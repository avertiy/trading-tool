using System;
using AVS.CoreLib._System.Net;
using AVS.PoloniexApi.General;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.WalletTools.Models
{
    public abstract class LoanOffer : Response, ILoanOffer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("date")]
        private string _date
        {
            set { DateUtc = DateTimeHelper.ParseUtcDateTime(value); }
        }

        public DateTime DateUtc { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        public string Exchange => PoloniexConstants.PoloniexExchange;

        public override string ToString()
        {
            var rate = Rate * 100;
            return $"loan {rate:0.00}% {Duration} days {Amount.FormatNumber()}";
        }
    }

    public class OpenLoanOffer : LoanOffer, IOpenLoanOffer
    {
        [JsonProperty("autoRenew")]
        public int _autoRenew
        {
            set { AutoRenew = value==1; }
        }

        public bool AutoRenew { get; set; }
        
        public override string ToString()
        {
            var rate = Rate * 100;
            return $"loan {rate:0.00}% {Duration} days {Amount.FormatNumber(Currency)}";
        }
    }

    public class ActiveLoan : LoanOffer, IActiveLoan
    {
        [JsonProperty("fees")]
        public double Fees { get; private set; }

        public override string ToString()
        {
            var rate = Rate * 100;
            return $"loan {rate:0.00}% {Duration} days {Amount.FormatNumber(Currency)}";
        }
    }
}