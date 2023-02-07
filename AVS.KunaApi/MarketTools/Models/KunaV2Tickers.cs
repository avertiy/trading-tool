using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.KunaApi.MarketTools.Models
{
    public class KunaV2Tickers
    {
        [JsonProperty("ticker")]
        public KunaMarketData Ticker { get; set; }

        public class KunaMarketData : IMarketData
        {
            [JsonProperty("buy")]
            public double LowestAsk { get; set; }
            [JsonProperty("sell")]
            public double HighestBid { get; set; }
            [JsonProperty("low")]
            public double Low { get; set; }
            [JsonProperty("high")]
            public double High { get; set; }
            [JsonProperty("vol")]
            public double Volume24HourBase { get; set; }
            [JsonProperty("price")]
            public double PriceLast { get; set; }
            [JsonIgnore]
            public double PriceChange => 0;
        }
    }
}