using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.BinanceApi.MarketTools.Models
{
    /// <summary>
    /// binance ticker book data 
    /// </summary>
    public class BinanceMarketData : IMarketData
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        
        /// <summary>
        /// The best bid price
        /// </summary>
        [JsonProperty("bidPrice")]
        public double HighestBid { get; set; }
        /// <summary>
        /// The best bid size
        /// </summary>
        [JsonProperty("bidQty")]
        public decimal BidSize { get; set; }
        /// <summary>
        /// The best ask price
        /// </summary>
        [JsonProperty("askPrice")]
        public double LowestAsk { get; set; }

        public double Volume24HourBase { get; set; }

        /// <summary>
        /// The best ask size
        /// </summary>
        [JsonProperty("askQty")]
        public decimal AskSize { get; set; }

        [JsonIgnore]
        public double PriceLast { get; set; }

        public double PriceChange { get; set; }
        
        public override string ToString()
        {
            return $"{Symbol} bid: {HighestBid.FormatAsPrice()} ask: {LowestAsk.FormatAsPrice()}";
        }
    }
}
