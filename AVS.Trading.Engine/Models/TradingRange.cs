using System;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Engine.Models
{
    public class TradingRange : PriceRange
    {
        public TradingRange(
            double min,
            double max,
            double buyRangePriceMin,
            double buyRangePriceMax,
            double sellRangePriceMin,
            double sellRangePriceMax, double deviation = 0.005) : base(min, max)
        {
            BuyRange = new PriceRange(buyRangePriceMin, buyRangePriceMax);
            SellRange = new PriceRange(sellRangePriceMin, sellRangePriceMax);
            
            if (!this.Contains(BuyRange) || !this.Contains(SellRange))
                throw new ArgumentException("range must contain buyRange and sellRange");
            if (!(BuyRange < SellRange))
                throw new ArgumentException("buyRange must be less sellRange");
            
            AllowedDeviation = deviation;
        }

        public TradingRange(PriceRange range, PriceRange buyRange, PriceRange sellRange, double deviation = 0.005) : base(range.Min, range.Max)
        {
            if(!range.Contains(buyRange) || !range.Contains(sellRange))
                throw new ArgumentException("range must contain buyRange and sellRange");
            if(!(buyRange < sellRange))
                throw new ArgumentException("buyRange must be less sellRange");

            BuyRange = buyRange;
            SellRange = sellRange;

            AllowedDeviation = deviation;
        }

        public TradingRange(double min,double max, PriceRange buyRange, PriceRange sellRange, double deviation = 0.005) : base(min,max)
        {
            if (!this.Contains(buyRange) || !this.Contains(sellRange))
                throw new ArgumentException("range must contain buyRange and sellRange");
            if (!(buyRange < sellRange))
                throw new ArgumentException("buyRange must be less sellRange");

            BuyRange = buyRange;
            SellRange = sellRange;

            AllowedDeviation = deviation;
        }

        public PriceRange BuyRange { get; set; }
        public PriceRange SellRange { get; set; }

        /// <summary>
        /// допустимое отклонение от диапозона
        /// выход за диапозон означает стоп трейдинга
        /// </summary>
        public double AllowedDeviation { get; set; }

    }
}