using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.ExmoApi.MarketTools.Models
{
    /// <summary>
    /// Represents MarketData Exmo response
    /// </summary>
    /// <example>
    /// {
    /// "BTC_USD": {
    ///     "buy_price": "589.06",
    ///     "sell_price": "592",
    ///     "avg": "591.14698808",
    ///     "last_trade": "591.221",
    ///     "high": "602.082",
    ///     "low": "584.51011695",
    ///     "vol": "167.59763535",
    ///     "vol_curr": "99095.17162071",
    ///     "updated": 1470250973
    ///  },
    ///}
    /// </example>
    class MarketData : IMarketData
    {
        [JsonProperty("last_trade")]
        public double PriceLast { get; set; }
        /// <summary>
        /// average deal price within the last 24 hours
        /// </summary>
        [JsonProperty("avg")]
        public double AvgPrice { get; set; }
        /// <summary>
        /// maximum deal price within the last 24 hours
        /// </summary>
        [JsonProperty("high")]
        public double High { get; }
        /// <summary>
        /// minimum deal price within the last 24 hours
        /// </summary>
        [JsonProperty("low")]
        public double Low { get; }


        [JsonProperty("vol_curr")]
        public double Volume24HourBase { get; }
        [JsonProperty("vol")]
        public double Volume24HourQuote { get; }

        /// <summary>
        ///  current maximum buy price
        /// </summary>
        [JsonProperty("buy_price")]
        public double HighestBid { get; }
        /// <summary>
        /// current minimum sell price
        /// </summary>
        [JsonProperty("sell_price")]
        public double LowestAsk { get; }
        
        public double OrderSpread => (LowestAsk - HighestBid).Normalize();
        public double OrderSpreadPercentage => (LowestAsk / HighestBid - 1).Normalize();

        public double PriceChange { get; set; }

        //public bool IsFrozen { get; set; }


        [JsonProperty("updated")]
        private string TimeInternal
        {
            set { DateUtc = ulong.Parse(value).UnixTimeStampToDateTime(); }
        }
        public DateTime DateUtc { get; set;}
    }
}
