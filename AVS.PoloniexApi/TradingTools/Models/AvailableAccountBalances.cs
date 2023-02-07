using System.Collections.Generic;
using AVS.Trading.Core.ResponseModels.TradingTools;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.TradingTools.Models
{
    public class AvailableAccountBalances: IAvailableAccountBalances
    {
        public AvailableAccountBalances()
        {
            Exchange = new Dictionary<string, double>();
            Margin = new Dictionary<string, double>(); 
            Lending = new Dictionary<string, double>(); 
        }

        [JsonProperty("exchange")]
        public IDictionary<string, double> Exchange { get; set; }

        [JsonProperty("margin")]
        public IDictionary<string, double> Margin { get; set; }

        [JsonProperty("lending")]
        public IDictionary<string, double> Lending { get; set; }
    }
}