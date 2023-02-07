using System;
using System.ComponentModel.DataAnnotations.Schema;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Data.Domain.MarketTools
{
    public class MarketData: BaseEntity
    {
        public string Pair { get; set; }
        public DateTime DateUtc { get; set; }

        public double PriceLast { get; set; }
        public double PriceChange { get; set; }
        [NotMapped]
        public double PriceChangePercentage => PriceChange * 100;
        public double Volume24HourBase { get; set; }
        public double Volume24HourQuote { get; set; }

        public double PriceAvg => Volume24HourBase/Volume24HourQuote;
        /// <summary>
        /// highest (top) buy order
        /// </summary>
        public double HighestBid { get; set; }
        /// <summary>
        /// lowest (top) sell order
        /// </summary>
        public double LowestAsk { get; set; }
        public double OrderSpread { get; set; }
        public double OrderSpreadPercentage { get; set; }
        public bool IsFrozen { get; set; }
        
        public override string ToString()
        {
            return $"{Pair} - {PriceLast.FormatAsPrice()} ({PriceChangePercentage:#.####}%)\t bid/ask {HighestBid}/{LowestAsk} [spread {OrderSpreadPercentage:#.####}%]";
        }
    }
}