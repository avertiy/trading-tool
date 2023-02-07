using System;
using System.Collections.Generic;
using System.Linq;

namespace AVS.Trading.Core.Extensions
{
    public static class RankCalculationExtensions
    {
        /// <summary>
        /// imagine you have scale [1,5,10,50,100] rank for 20 => 3 
        /// </summary>
        public static int GetRankByScale(this double number, double[] scale)
        {
            for (var i = 0; i < scale.Length; i++)
            {
                if (number < scale[i])
                    return i;
            }
            return scale.Length;
        }

        public static int GetRank(this double number, double @base)
        {
            var n = number / @base;
            if (n <= 1)
                return 0;
            if (n < 3)
                return 1;
            if (n < 5)
                return 2;
            if (n < 8)
                return 3;
            if (n < 11)
                return 4;
            if (n < 14)
                return 5;
            if (n < 17)
                return 6;
            if (n < 20)
                return 7;
            if (n < 25)
                return 8;
            if (n < 35)
                return 9;
            return 10;
        }

        public static void CalcRank<T>(this IList<T> list, Func<T, double> selector, Action<T, int> projection)
        {
            var rankBase = list.GetRankBase(selector);

            foreach (var item in list)
            {
                var value = selector(item);
                int rank = GetRank(value, rankBase);
                projection(item, rank);
            }
        }

        public static IEnumerable<TResult> CalcRank<T, TResult>(this IList<T> list, Func<T, double> selector, Func<T, int, TResult> projection)
        {
            var rankBase = list.GetRankBase(selector);

            foreach (var item in list)
            {
                var value = selector(item);
                int rank = GetRank(value, rankBase);
                yield return projection(item, rank);
            }
        }

        public static int GetIndexOfBestValue<T>(this IList<T> list, Func<T, double> selector, int bestFromRank =5)
        {
            double result = -1.0;
            int index = 0;
            int rank = 0;

            var rankBase = list.GetRankBase(selector);

            foreach (var item in list)
            {
                var value = selector(item);
                int currenRank = GetRank(value, rankBase);
                if (result < 0 || rank < currenRank)
                {
                    rank = currenRank;
                    result = value;
                    if (rank >= bestFromRank)
                        break;
                }

                index++;
            }
            return index;
        }

        public static double GetBestValue<T>(this IList<T> list, Func<T, double> selector, int bestFromRank = 5)
        {
            double result = -1.0;
            int rank = 0;

            var rankBase = list.GetRankBase(selector);

            foreach (var item in list)
            {
                var value = selector(item);
                int currenRank = GetRank(value, rankBase);
                if (result < 0 || rank < currenRank)
                {
                    rank = currenRank;
                    result = value;
                    if (rank >= bestFromRank)
                        break;
                }
            }
            return result;
        }
        
        private static double GetRankBase<T>(this IList<T> list, Func<T, double> selector)
        {
            double max = 0.0;
            double avg = 0.0;
            double sum = 0.0;

            long count = 0;

            foreach (T item in list)
            {
                var value = selector(item);
                if (value > max)
                    max = value;
                sum += value;
                checked { ++count; }
            }

            if (count > 0L)
                avg = sum / count;

            var rankBase = avg;
            if (GetRank(max, avg) <= 2)
                rankBase = avg / 2;

            return rankBase;
        }

        public static int GetPriceRank(this double price)
        {
            if (price < 0.00000001)
                return 0;
            if (price < 0.0000001)
                return 1;
            if (price < 0.000001)
                return 2;
            if (price < 0.00001)
                return 3;
            if (price < 0.0001)
                return 4;
            if (price < 0.001)
                return 5;
            if (price < 0.01)
                return 6;
            if (price < 0.1)
                return 7;
            if (price < 1)
                return 8;
            if (price < 10)
                return 9;
            if (price < 100)
                return 10;
            if (price < 1000)
                return 11;
            if (price < 10000)
                return 12;
            if (price < 100000)
                return 13;
            return 14;
            //for (var i = -8; i < 6; i++)
            //{
            //    if(number < Math.Pow(10.0, i))
            //}
        }

        public static bool AllSameRank(this double number, params double[] values)
        {
            var rank = number.GetPriceRank();
            return values.All(p => p.GetPriceRank() == rank);
        }

        public static bool IsSameRank(this double value1, double value2)
        {
            return value1.GetPriceRank() == value2.GetPriceRank();
        }
    }
}