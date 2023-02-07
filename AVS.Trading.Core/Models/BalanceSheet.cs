using System;
using System.Collections.Generic;
using System.Text;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Core.Interfaces;
using Newtonsoft.Json;

namespace AVS.Trading.Core.Models
{
    //Debit -2 LTC  => Order #123
    //Credit +2 LTC  => Order #123 canceled
    //or 
    //Credit 0.014 BTC Order #123 executed
    //or 
    //Credit 0.01 BTC Order #123 partially executed
    //Credit 0.5 LTC Order #123 canceled
    
    public sealed class BalanceSheet
    {
        //public readonly TradingFormatInfo Formatter = new TradingFormatInfo();

        private static void UsageExample()
        {
            var pair = new CurrencyPair("BTC","XMR");
            var sheet = new BalanceSheet();
            sheet["XMR"].Credit(1, "initial balance");
            sheet["BTC"].Credit(0.02, "initial balance");
            sheet["MAID"].Credit(200, "initial balance");
            sheet.Sold("XMR", 0.5, 0.012, "BTC");
            sheet.Bought(pair, 1, 1*0.08/100, 0.011, DateTime.Now);
            //sheet.Bought("MAID", 100, 0.0000233, "BTC");

            var log = sheet.GetTransactionsLog();
        }

        [JsonProperty]
        private Dictionary<string, CurrencyBalance> _items = new Dictionary<string, CurrencyBalance>();

        public BalanceSheet()
        {
        }

        public void Debit(double amount, string currency, string notes)
        {
            if (!_items.ContainsKey(currency))
                _items[currency] = new CurrencyBalance(){Currency = currency };
            _items[currency].Debit(amount, notes);
        }
        
        public void Credit(double amount, string currency, string notes)
        {
            if (!_items.ContainsKey(currency))
                _items[currency] = new CurrencyBalance() { Currency = currency };
            _items[currency].Credit(amount, notes);
        }

        public void Sold(string quoteCurrency, double amount, double price, string baseCurrency="BTC", double fees=0.08)
        {
            //when sell transaction
            //0.00736910 BTC [5 LTC x 0.001475 - 0.0000059]
            //    total       amount     price      fees     

            this[quoteCurrency].Debit(amount, $"-{amount.FormatAsQuantity()}{quoteCurrency} [sell at {price.FormatAsPrice()} {baseCurrency}]");
            var total = amount * price * (1 - fees / 100);
            this[baseCurrency].Credit(total, $"+{total.FormatAsQuantity()}{baseCurrency} [sold {amount.FormatAsQuantity()} {quoteCurrency} x {price.FormatAsPrice()}]");
        }

        public void Bought(CurrencyPair pair, double amount, double fees, double price, DateTime date)
        {
            var quoteCurrency = pair.QuoteCurrency;
            var baseCurrency = pair.BaseCurrency;
            var priceStr = price.FormatAsPrice();
            var amountStr = amount.FormatAsQuantity();
            var creditAmountStr = (amount-fees).FormatAsQuantity();

            //e.g. +0.9992 LTC [buy 1 LTC x 0.011]
            string comment = $"+{creditAmountStr} {quoteCurrency} [buy {amountStr} {quoteCurrency} x {priceStr} {date:g}]";
            this[pair.QuoteCurrency].Credit(amount-fees, comment);

            var total = amount * price;
            //e.g. -0.0032 BTC [buy 4FCT x 0.0008]
            comment = $"-{total.FormatAsQuantity()} {baseCurrency} [buy {amountStr} {quoteCurrency} x {priceStr} {date:g}]";
            this[pair.BaseCurrency].Debit(total, comment);
        }

        public void Bought(string quoteCurrency, double amount, double price, string baseCurrency="BTC", double fees = 0.08)
        {
            this[quoteCurrency].Credit(amount, $"+{amount.FormatAsQuantity()}{quoteCurrency} [buy at {price.FormatAsPrice()} {baseCurrency}]");
            var total = amount * price * (1 + fees / 100);
            this[baseCurrency].Debit(total, $"-{total.FormatAsQuantity()} [bought {amount.FormatAsQuantity()} {quoteCurrency} x {price.FormatAsPrice()}]");
        }

        //public void 

        public CurrencyBalance this[string currency]
        {
            get => _items[currency];
            set => _items[currency] = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kp in _items)
            {
                var currency = kp.Key;
                sb.Append($"{kp.Value.Balance.FormatAsQuantity()} {currency} ");
            }

            if(sb.Length>0)
                sb.Length--;

            return sb.ToString();
        }

        public string GetTransactionsLog()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kp in _items)
            {
                sb.AppendLine($"{kp.Key} balance: {kp.Value.Balance:0.0000}\r\n");
                
                foreach (var record in kp.Value.GetAllRecords())
                    sb.AppendLine(record);

                sb.AppendLine();
            }
            return sb.ToString();
        }

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static BalanceSheet FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return null;
            var balance = JsonConvert.DeserializeObject<BalanceSheet>(json);
            return balance;
        }

        public void Execute(ITradeItem trade, CurrencyPair pair)
        {
            var date = trade.DateUtc;
            var total = trade.AmountBase;
            var price = trade.Price;
            var amount = trade.AmountQuote;
            if (trade.Type == TradeType.Buy)
            {
                var creditQuoteAmount = (trade.AmountQuote - trade.Fee);

                //e.g. +0.9992 LTC [buy 1 LTC x 0.011]
                var comment = TradingFormatter.Format($"{creditQuoteAmount} {pair:quote} [buy {amount} {pair:quote} x {price:price} {date:g}]");
                this[pair.QuoteCurrency].Credit(creditQuoteAmount, comment);

                //e.g. -0.0032 BTC [buy 4FCT x 0.0008]
                comment = TradingFormatter.StringFormat("-{0} {3:base} [buy {1} {3:quote} x {2:price} {4:g}]", 
                    total,
                    amount,
                    price, 
                    pair,
                    date);

                this[pair.BaseCurrency].Debit(total, comment);
            }
            else
            {
                //e.g. -1 LTC [sell x 0.011]
                var comment = TradingFormatter.StringFormat("+{0} {2:quote} [sell x {1:price} {3:g}]",
                    amount,
                    price,
                    pair,
                    date);

                this[pair.QuoteCurrency].Debit(trade.AmountQuote, comment);

                //e.g. +0.00319744 [sell 4FCT x 0.0008 - fees]
                comment = TradingFormatter.StringFormat("+{0} {3:base} [sell {1} {3:quote} x {2:price} {4:g}]",
                    total,
                    amount,
                    price,
                    pair,
                    date);

                this[pair.BaseCurrency].Credit(total, comment);
            }
        }
    }

    public class CurrencyBalance
    {
        [JsonProperty]
        public List<string> Notes { get; protected set; }
        [JsonProperty]
        public string Currency { get; set; }
        [JsonProperty]
        public double Balance { get; set; }

        public CurrencyBalance()
        {
            Notes = new List<string>();
        }

        public void Debit(double amount, string notes)
        {
            Balance -= amount;
            Notes.Add(notes);
        }

        public void Credit(double amount, string notes)
        {
            Balance += amount;
            Notes.Add(notes);
        }

        public string GetLog()
        {
            return string.Join("\r\n", Notes);
        }

        public string[] GetAllRecords()
        {
            return Notes.ToArray();
        }

        public string GetRecordsAsJson()
        {
            return JsonConvert.SerializeObject(Notes);
        }

        public override string ToString()
        {
            return $"{Balance.FormatAsQuantity()} {Currency}";
        }
    }
}