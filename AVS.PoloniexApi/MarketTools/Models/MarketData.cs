using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.MarketTools.Models
{
    public class MarketData : IMarketData
    {
        public double PriceLast { get; internal set; }

        [JsonProperty("last")]
        private string PriceLastStringValue
        {
            set => PriceLast = NumericHelper.ParseDouble(value);
        }

        [JsonProperty("percentChange")]
        public double PriceChange { get; internal set; }

        [JsonProperty("baseVolume")]
        public double Volume24HourBase { get; internal set; }
        [JsonProperty("quoteVolume")]
        public double Volume24HourQuote { get; internal set; }

        
        public double HighestBid { get; internal set; }

        [JsonProperty("highestBid")]
        private string OrderTopBuyStringValue
        {
            set => HighestBid = NumericHelper.ParseDouble(value);
        }

        
        public double LowestAsk { get; internal set; }

        [JsonProperty("lowestAsk")]
        private string OrderTopSellStringValue
        {
            set => LowestAsk = NumericHelper.ParseDouble(value);
        }

        public double OrderSpread => (LowestAsk - HighestBid).Normalize();

        public double OrderSpreadPercentage => (LowestAsk / HighestBid - 1).Normalize();////trick with -1 is the same as OrderSpread/HighestBid

        [JsonProperty("isFrozen")]
        internal byte IsFrozenInternal {
            set { IsFrozen = value != 0; }
        }
        public bool IsFrozen { get; private set; }
    }


   
}
