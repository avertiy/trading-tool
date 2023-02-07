using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.Extensions
{
    public static class CandleExtensions
    {
        public static double[] GetBollingerBandsWithSimpleMovingAverage(this IList<ICandlestick> value, int index, int period = 20)
        {
            var closes = new List<double>(period);
            for (var i = index; i > Math.Max(index - period, -1); i--)
            {
                closes.Add(value[i].Close);
            }

            var simpleMovingAverage = closes.Average();
            var stDevMultiplied = Math.Sqrt(closes.Average(x => Math.Pow(x - simpleMovingAverage, 2))) * 2;

            return new[] {
                simpleMovingAverage,
                simpleMovingAverage + stDevMultiplied,
                simpleMovingAverage - stDevMultiplied
            };
        }
    }
}