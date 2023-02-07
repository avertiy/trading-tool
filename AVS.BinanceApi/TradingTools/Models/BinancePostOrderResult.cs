using AVS.Trading.Core.Interfaces.TradingTools;
using Newtonsoft.Json;

namespace AVS.BinanceApi.TradingTools.Models
{
    public class BinancePostOrderResult: IPostOrderResult
    {
        [JsonProperty("id")]
        public string IdOrder { get; set; }
        [JsonProperty("side")]
        public string Side { get; set; }

        [JsonProperty("ord_type")]
        public string Type { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("executed_volume")]
        public double ExecutedVolume { get; set; }

        [JsonProperty("market")]
        public string Pair { get; set; }
    }
}