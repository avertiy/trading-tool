using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVS.CoreLib.Json;
using AVS.CoreLib.Utils;
using AVS.PoloniexApi.General;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using Newtonsoft.Json.Linq;

namespace AVS.PoloniexApi.LiveTools
{
    public sealed class PriceAggregatedBook
    {
        private readonly Comparer<double> ReverseComparer = Comparer<double>.Create((x, y) => y.CompareTo(x));
        private long SequenceNumber { get; set; }
        public double CutKoef = 1.0 + 0.16;//Constants.OrderBookPriceCutKoefficient;
        public double AmountBaseEventThreshold = 0.002;

        internal TickerSymbol Symbol { get; set; }
        public string Pair { get; set; }

        public SortedDictionary<double,double> Buys { get; set; }
        public SortedDictionary<double,double> Sells { get; set; }

        public int Length => Buys.Count > Sells.Count ? Buys.Count : Sells.Count;

        public event Action<TradeEventArgs> Trade;
        public event Action<OrderUpdateEventArgs> SellsUpdate;
        public event Action<OrderUpdateEventArgs> BuysUpdate;
        public event Action<PriceAggregatedBook> Ready;
        public event Action<PriceAggregatedBook> Update;

        public override string ToString()
        {
            var buysTotalAmount = Buys.Sum(b => b.Value).FormatAsAmount();
            var sellsTotalAmount = Sells.Sum(b => b.Value).FormatAsAmount();
            return $"{Symbol} buys: [#{Buys.Count} - {buysTotalAmount}] sells: [#{Sells.Count} - {sellsTotalAmount}]";
        }
        
        internal void OnTrade(double price, double amount, double oldAmount, TradeType type)
        {
            if (price * amount < AmountBaseEventThreshold)
                return;
            Trade?.Invoke(
                new TradeEventArgs()
                {
                    Book = this,
                    TradeAmount = amount,
                    Price = price,
                    Amount = oldAmount,
                    Type = type
                });
        }

        internal void OnReady(PriceAggregatedBook obj)
        {
            Ready?.Invoke(obj);
        }

        internal void OnSellsUpdate(double price, double amount, double oldAmount)
        {
            if(price * amount < AmountBaseEventThreshold && price * oldAmount < AmountBaseEventThreshold)
                return;
            SellsUpdate?.Invoke(new OrderUpdateEventArgs()
            {
                Book = this,
                Price = price,
                Amount = oldAmount,
                OrderAmount = amount,
                Type = TradeType.Sell
            });
        }

        internal void OnBuysUpdate(double price, double amount, double oldAmount)
        {
            if (price * amount < AmountBaseEventThreshold && price * oldAmount < AmountBaseEventThreshold)
                return;
            BuysUpdate?.Invoke(new OrderUpdateEventArgs()
            {
                Book = this,
                Price = price,
                Amount = oldAmount,
                OrderAmount = amount,
                Type = TradeType.Buy
            });
        }

        internal void OnUpdate(PriceAggregatedBook obj)
        {
            Update?.Invoke(obj);
        }

        internal void SetBuys(IDictionary<double, double> dict)
        {
            Buys = new SortedDictionary<double, double>(dict, ReverseComparer);
        }

        internal void SetSells(IDictionary<double, double> dict)
        {
            Sells = new SortedDictionary<double, double>(dict);
        }
    }

    public class TradeEventArgs
    {
        public double Price { get; set; }
        public double TradeAmount { get; set; }
        public double Amount { get; set; }
        public TradeType Type { get; set; }

        public PriceAggregatedBook Book { get; set; }
    }

    public class OrderUpdateEventArgs
    {
        public double Price { get; set; }
        public double OrderAmount { get; set; }
        public double Amount { get; set; }
        public TradeType Type { get; set; }
        public PriceAggregatedBook Book { get; set; }
    }

    public static class PriceAggregatedBookExtensions
    {
        public static int PeekIndex(this PriceAggregatedBook book, double price)
        {
            var i = 0;
            if (book.Buys.First().Key >= price)
            {
                foreach (var key in book.Buys.Keys)
                {
                    if (key <= price)
                        return i;
                    i++;
                }
            }

            if (book.Sells.First().Key <= price)
            {
                foreach (var key in book.Sells.Keys)
                {
                    if (key >= price)
                        return i;
                    i++;
                }
            }
            return -1;
        }

        public static void Print(this PriceAggregatedBook book, double minOrderAmount = 0.0, int printLength = 0)
        {
            if(printLength < 0)
                throw new ArgumentOutOfRangeException(nameof(printLength), printLength, "Expected depth value >= 0");
            //header
            using (var locker = ConsoleLocker.Create())
            {
                Console.WriteLine($"Order Book for {book.Symbol}");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("".PadRight(80, '-'));
                Console.WriteLine($"|{" ",15}{"BUYS",-16}{" ",6}|  |{" ",15}{"SELLS",-16}{" ",6}|");
                Console.WriteLine("".PadRight(80, '-'));
                Console.WriteLine($"|  {"Price",-12}{"Amount",11}{" ",6}TOTAL |  |  {"Price",-12}{"Amount",11}{" ",6}TOTAL |");
                Console.WriteLine("".PadRight(80, '-'));
                var length = printLength == 0 || printLength > book.Length ? book.Length : printLength;

                var avgAmount = (book.Buys.Values.Average() + book.Sells.Values.Average()) / 2;

                var totalBuys = 0.0;
                var totalSells = 0.0;

                var buys = book.Buys.Where(o => o.Value > minOrderAmount).ToList();
                var sells = book.Sells.Where(o => o.Value > minOrderAmount).ToList();

                for (var i = 0; i < length; i++)
                {
                    if (i < buys.Count)
                    {
                        var buy = buys.ElementAt(i);
                        totalBuys += buy.Value;
                        if (buy.Value >= avgAmount)
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"|{buy.Key.FormatAsPrice(),-14}{buy.Value.FormatAsQuantity(),11} ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"{totalBuys.FormatAsQuantity(),11}|");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.Write($"{" ",35}");
                    }

                    if (i < sells.Count)
                    {
                        var sell = sells.ElementAt(i);

                        if (sell.Value <= 0)
                            continue;

                        totalSells += sell.Value;

                        if (sell.Value >= avgAmount)
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($"  |{sell.Key.FormatAsPrice(),-14}{sell.Value.FormatAsQuantity(),11} ");
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write($"{totalSells.FormatAsQuantity(),11}|");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        
                    }

                    Console.WriteLine();
                }
            }
        }

        public static void Shrink(this PriceAggregatedBook book, double? cutKoef = null)
        {
            if (cutKoef.HasValue)
                book.CutKoef = cutKoef.Value;
            var thresholdSellPrice = book.Sells.Keys.First() * book.CutKoef;
            var thresholdBuyPrice = book.Buys.Keys.First() / book.CutKoef;

            var dict = book.Buys.Where(b => b.Key >= thresholdBuyPrice && b.Value > 0).ToDictionary(k => k.Key, k => k.Value);
            book.SetBuys(dict);

            dict = book.Sells.Where(b => b.Key <= thresholdSellPrice && b.Value > 0).ToDictionary(k => k.Key, k => k.Value);
            book.SetSells(dict);
        }

        internal static void ParseMessage(this PriceAggregatedBook book, JArray jArray)
        {
            var kind = jArray[2][0][0].Value<string>();
            if (kind == "i")
            {
                book.Init(jArray);
                book.OnReady(book);
            }
            else
            {
                foreach (JToken token in jArray[2])
                {
                    if (token is JArray arr)
                    {
                        kind = arr[0].Value<string>();
                        if (kind == "o")
                        {
                            book.ParseOrderInfo(arr);
                        }
                        else if (kind == "t")
                        {
                            book.ParseTradeInfo(arr);
                        }
                        //book.OnUpdate(book);
                    }
                    else
                    {
                        throw new Exception($"Unexpected token: {token}");
                    }
                }
            }
        }

        private static void ParseTradeInfo(this PriceAggregatedBook book, JArray arr)
        {
            double price = ParseDouble(arr[3]);
            double amount = ParseDouble(arr[4]);

            //trade
            //1 for sell, 0 for buy 
            if (arr[2].Value<int>() == 1)
            {
                //the book could be shrinked, so some keys might not exist
                if (book.Sells.TryGetValue(price, out double oldAmount))
                {
                    book.Sells[price] -= amount;
                    book.OnTrade(price, amount, book.Sells[price], TradeType.Sell);
                }
            }
            else
            {
                if (book.Buys.TryGetValue(price, out double oldAmount))
                {
                    book.Buys[price] -= amount;
                    book.OnTrade(price, amount, book.Buys[price], TradeType.Buy);
                }
            }
        }

        private static void ParseOrderInfo(this PriceAggregatedBook book, JArray arr)
        {
            //var str = arr.ToString();

            var price = NumericHelper.ParseDouble(arr[2].Value<string>());
            var amount = NumericHelper.ParseDouble(arr[3].Value<string>());

            //order
            //0 for sell, 1 for buy 
            if (arr[1].Value<int>() == 0)
            {
                book.Sells.TryGetValue(price, out double oldAmount);
                book.Sells[price] = amount;
                book.OnSellsUpdate(price, amount, oldAmount);
            }
            else
            {
                book.Buys.TryGetValue(price, out double oldAmount);
                book.Buys[price] = amount;
                book.OnBuysUpdate(price, amount, oldAmount);
            }
            
        }

        private static void Init(this PriceAggregatedBook book, JArray jArray)
        {
            book.Symbol = (TickerSymbol)jArray[0].Value<int>();
            book.Pair = (((JObject)jArray[2][0][1]).Property("currencyPair").Value).Value<string>();

            JArray bookJArray = ((JObject)jArray[2][0][1]).Property("orderBook").Value as JArray;


            var dict = JsonHelper.ParseDictionary<double, double>((JObject)bookJArray[0],
                price => NumericHelper.ParseDouble(price),
                amount => NumericHelper.ParseDouble(amount));

            book.SetSells(dict);

            dict = JsonHelper.ParseDictionary<double, double>((JObject)bookJArray[1],
                price => NumericHelper.ParseDouble(price),
                amount => NumericHelper.ParseDouble(amount));

            book.SetBuys(dict);
        }

        private static double ParseDouble(JToken token)
        {
            return NumericHelper.ParseDouble(token.Value<string>());
        }
    }
}