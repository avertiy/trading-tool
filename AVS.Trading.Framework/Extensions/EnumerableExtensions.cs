using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;

namespace AVS.Trading.Framework.Extensions
{
    public static class EnumerableExtensions
    {
        public static IList<T> Shrink<T>(this IList<T> items, Func<T,double> selector, double threshold = 0.0)
        {
            var avg = items.Average(selector);
            if (threshold <= 0)
                threshold = avg;
            return items.Where(i => selector(i) >= threshold).ToList();
        }
        /// <summary>
        /// Reduces items count by combining items together summing up key value up to threshold value 
        /// </summary>
        /// <param name="tradeItems"></param>
        /// <param name="avgKoef">koefficient used to calculate average that is used to shrink the sequense</param>
        /// <param name="threshold">if the value is 0 the threshold is determined by average x avgKoef. Resulting combined item will not exceed the threshold value</param>
        public static IList<MarketTradeItem> Reduce(this IList<MarketTradeItem> tradeItems, double avgKoef = 1.1, double threshold = 0.0)
        {
            if (tradeItems.Count == 0)
                return tradeItems;

            var avg = tradeItems.Average(t => t.AmountBase) * avgKoef;//avg +10%
            if (threshold >= 0 && threshold < avg)
                threshold = avg;
            var list = tradeItems.Where(t => t.AmountBase >= threshold).ToList();

            var buy = new MarketTradeItem() { Type = TradeType.Buy, Pair = tradeItems[0].Pair };
            var sell = new MarketTradeItem() { Type = TradeType.Sell, Pair = tradeItems[0].Pair };

            int buys = 0, sells = 0;

            foreach (var item in tradeItems.Where(t => t.AmountBase < threshold))
            {
                if (item.Type == TradeType.Buy)
                {
                    buy.DateUtc = item.DateUtc;
                    buy.Price += item.Price;
                    buys++;
                    buy.AmountQuote = (buy.AmountQuote + item.AmountQuote).Normalize();
                    buy.AmountBase = (buy.AmountBase + item.AmountBase).Normalize();
                    if (buy.AmountBase > threshold)
                    {
                        buy.Price = buy.Price / buys;
                        buys = 0;
                        list.Add(buy);
                        buy = new MarketTradeItem() { Type = TradeType.Buy, Pair = buy.Pair };
                    }

                }
                else
                {
                    sell.DateUtc = item.DateUtc;
                    sell.Price += item.Price;
                    sells++;
                    sell.AmountQuote = (sell.AmountQuote + item.AmountQuote).Normalize();
                    sell.AmountBase = (sell.AmountBase + item.AmountBase).Normalize();
                    if (sell.AmountBase > threshold)
                    {
                        sell.Price = sell.Price / sells;
                        sells = 0;
                        list.Add(sell);
                        sell = new MarketTradeItem() { Type = TradeType.Sell, Pair = sell.Pair };
                    }
                }
            }

            if (buy.AmountBase > 0)
            {
                buy.Price = buy.Price / buys;
                list.Add(buy);
            }

            if (sell.AmountBase > 0)
            {
                sell.Price = sell.Price / sells;
                list.Add(sell);
            }

            return list.OrderBy(i=>i.DateUtc).ToList();
        }
        
        public static IEnumerable<TResult> ForEach<T,TResult>(this IEnumerable<T> items, Func<T, TResult> apply)
        {
            var res = new List<TResult>();
            foreach (var item in items)
            {
                res.Add(apply(item));
            }
            return res;
        }

        public static bool Contains<T>(this IList<T> source, params T[] items)
        {
            if (source.Count < items.Length)
                return false;

            var result = true;
            
            foreach (var item in items)
            {
                if (!source.Contains(item))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static void Merge<T, TKey>(this IList<T> target, IList<T> source, Func<T, TKey> selector, Func<T, bool> predicate)
        {
            var knownKeys = new HashSet<TKey>(target.Select(selector));

            foreach (var item in source)
            {
                if (predicate(item) && knownKeys.Add(selector(item)))
                    target.Add(item);
            }
        }
    }

    public static class RunningTotalExtensions
    {
        public static IEnumerable<TResult> Rollup<TSource, TResult>(
            this IEnumerable<TSource> source,
            TResult seed,
            Func<TSource, TResult, TResult> projection)
        {
            TResult nextSeed = seed;
            foreach (TSource src in source)
            {
                TResult projectedValue = projection(src, nextSeed);
                nextSeed = projectedValue;
                yield return projectedValue;
            }
        }

        public static IEnumerable<Tuple<TSource, TSeed>> AccumulativeTotal<TSource, TSeed>(
            this IEnumerable<TSource> source,
            TSeed seed,
            Func<TSource, TSeed, TSeed> projection)
        {
            TSeed nextSeed = seed;
            foreach (TSource src in source)
            {
                TSeed projectedValue = projection(src, nextSeed);
                nextSeed = projectedValue;
                yield return new Tuple<TSource, TSeed>(src, projectedValue);
            }
        }

        public static IEnumerable<Aggregation<TSource, TSeed>> RunningTotal<TSource, TSeed>(
            this IEnumerable<TSource> source,
            TSeed seed,
            Func<TSource, TSeed, TSeed> projection)
        {
            TSeed nextSeed = seed;
            foreach (TSource src in source)
            {
                TSeed projectedValue = projection(src, nextSeed);
                nextSeed = projectedValue;
                yield return new Aggregation<TSource, TSeed>(src, projectedValue);
            }
        }
        
        public class Aggregation<TData, TResult>
        {
            public TData Data { get; set; }
            public TResult RunningTotal { get; set; }

            public Aggregation(TData data, TResult total)
            {
                Data = data;
                RunningTotal = total;
            }
        }
    }

    

}
