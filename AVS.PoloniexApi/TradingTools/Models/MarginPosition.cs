using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.TradingTools.Models
{
    /// <summary>
    /// Represents JSON
    /// {"amount": "40.94717831","total": "-0.09671314","basePrice": "0.00236190"," +
    /// "liquidationPrice": -1,"pl": "-0.00058655","lendingFees": "-0.00000038","type": "long"}
    /// </summary>
    public class MarginPosition:  IMarginPosition, IMarket
    {
        public string Market { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("total")]
        public double Total { get; set; }
        [JsonProperty("basePrice")]
        public double BasePrice { get; set; }
        [JsonProperty("liquidationPrice")]
        public double LiquidationPrice { get; set; }
        [JsonProperty("pl")]
        public double ProfitLoss { get; set; }
        [JsonProperty("lendingFees")]
        public double LendingFees { get; set; }
        
        [JsonProperty("type")]
        private string TypeInternal
        {
            set => Type = value.ToMarginPosition();
        }
        public PositionType Type { get; set; }

    }
}