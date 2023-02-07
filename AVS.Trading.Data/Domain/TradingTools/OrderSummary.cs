using System.Text;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Data.Domain.TradingTools
{
    /// <summary>
    /// just a summary model not an entity
    /// </summary>
    public class OrderSummary
    {
        public string Market { get; set; }
        public OrderSummary()
        {
            Exchange = new OrderTotals();
            Margin = new OrderTotals();
        }

        public OrderTotals Exchange { get; set; }
        public OrderTotals Margin { get; set; }

        public void Add(TradingAccount category, OrderType type, double amountQuote, double amountBase)
        {
            if (category == TradingAccount.Exchange)
            {
                Exchange.Add(type, amountQuote, amountBase);
            }
            else
            {
                Margin.Add(type, amountQuote, amountBase);
            }
        }

        public string GetInfo(bool quote = true)
        {
            var pair = CurrencyPair.Parse(Market);
            var baseCurrency = pair.BaseCurrency;
            var quoteCurrency = pair.QuoteCurrency;
            var sb = new StringBuilder();
            if (!Exchange.IsEmpty)
            {
                sb.Append("Exchange: ");
                sb.Append(quote
                    ? Exchange.TotalAmountQuote.FormatNumber(quoteCurrency)
                    : Exchange.TotalAmountBase.FormatNumber(baseCurrency));
                sb.Append("    ");
            }
            if (!Margin.IsEmpty)
            {
                sb.Append("Margin: ");
                sb.Append(quote
                    ? Margin.TotalAmountQuote.FormatNumber(quoteCurrency)
                    : Margin.TotalAmountBase.FormatNumber(baseCurrency));
            }
            return sb.ToString();
        }
    }

    public class OrderTotals
    {
        public double BuysAmountQuote { get; set; }
        public double BuysAmountBase { get; set; }

        public double SellsAmountQuote { get; set; }
        public double SellsAmountBase { get; set; }

        public double TotalAmountQuote => BuysAmountQuote - SellsAmountQuote;
        public double TotalAmountBase => BuysAmountBase - SellsAmountBase;

        public bool IsEmpty => (BuysAmountQuote + SellsAmountQuote) == 0;

        public void Add(OrderType type, double amountQuote, double amountBase)
        {
            if (type == OrderType.Buy)
            {
                BuysAmountQuote += amountQuote;
                BuysAmountBase += amountBase;
            }
            else
            {
                SellsAmountQuote += amountQuote;
                SellsAmountBase += amountBase;
            }
        }
    }
}