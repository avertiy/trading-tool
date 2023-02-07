using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    /// <summary>
    /// {"totalValue": "0.00346561", "pl": "-0.00001220","lendingFees": "0.00000000",
    /// "netValue": "0.00345341","totalBorrowedValue": "0.00123220","currentMargin": "2.80263755"}
    /// </summary>
    public class MarginAccountSummary : Response, IMarginAccountSummary
    {
        [JsonProperty("totalValue")]
        public double TotalValue { get; set; }
        [JsonProperty("pl")]
        public double ProfitLoss { get; set; }
        [JsonProperty("lendingFees")]
        public double LendingFees { get; set; }
        [JsonProperty("netValue")]
        public double NetValue { get; set; }
        [JsonProperty("totalBorrowedValue")]
        public double TotalBorrowedValue { get; set; }
        [JsonProperty("currentMargin")]
        public double CurrentMargin { get; set; }
    }
}