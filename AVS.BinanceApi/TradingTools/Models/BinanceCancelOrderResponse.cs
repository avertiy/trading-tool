using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.BinanceApi.TradingTools.Models
{
    public class BinanceCancelOrderResponse : SimpleResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("executed_volume")]
        public double ExecutedVolume { get; set; }

        [JsonProperty("market")]
        public string Pair { get; set; }

        public override bool Success => Price > 0 && string.IsNullOrEmpty(Error);

        public void SetMessage(string message)
        {
            Message = message;
        }
    }
}