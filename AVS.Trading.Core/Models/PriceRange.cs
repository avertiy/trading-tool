using System;
using System.Collections.Generic;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Core.Models
{
    public class PriceRange
    {
        public PriceRange(double min, double max)
        {
            Min = min;
            Max = max;
            if(Max > min)
                throw new ArgumentException("priceMax must be greater than priceMin");
        }

        public double Min { get; protected set; }
        public double Max { get; protected set; }

        public double Avg => (Max + Min) / 2;

        public bool Match(double price)
        {
            return Min <= price && price <= Max;
        }

        public double GetDiff(double price)
        {
            if (price > Max)
                return Math.Round((price - Max) / Max, 3);
            if (price < Min)
                return Math.Round((Min - price) / Min, 3);
            return 0.0;
        }

        public bool Contains(PriceRange range)
        {
            return Min <= range.Min && Max >= range.Max;
        }

        public double GetLength() => Math.Round((Max - Min) / Min*100, 2);

        #region comparison operators
        public static bool operator <(PriceRange range1, PriceRange range2)
        {
            return range1.Avg < range2.Avg;
        }

        public static bool operator >(PriceRange range1, PriceRange range2)
        {
            return range1.Avg > range2.Avg;
        }

        public static bool operator <=(double price, PriceRange range)
        {
            return price <= range.Max;
        }

        public static bool operator >=(double price, PriceRange range)
        {
            return price >= range.Min;
        } 
        #endregion

        public override string ToString()
        {
            return $"[{Min.FormatAsPrice()};{Max.FormatAsPrice()}]";
        }

        public PriceRange[] Split(double step)
        {
            double price = Min;
            int n = (int)(GetLength() / step);
            var list = new List<PriceRange>(n);
            do
            {
                var range = new PriceRange(price, price * (1 + step));
                list.Add(range);
                price = range.Max;
            } while (price <= Max);

            return list.ToArray();
        }
    }
}