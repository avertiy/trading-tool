using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using Jojatekok.PoloniexAPI.WalletTools;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.WalletTools.Models
{
    public class Withdrawal : IWithdrawal
    {
        [JsonProperty("withdrawalNumber")]
        public ulong Id { get; private set; }

        [JsonProperty("currency")]
        public string Currency { get; private set; }
        [JsonProperty("address")]
        public string Address { get; private set; }
        [JsonProperty("amount")]
        public double Amount { get; private set; }

        [JsonProperty("timestamp")]
        private ulong TimeInternal {
            set { Time = value.UnixTimeStampToDateTime(); }
        }
        public DateTime Time { get; private set; }
        [JsonProperty("ipAddress")]
        public string IpAddress { get; private set; }

        [JsonProperty("status")]
        public string Status { get; private set; }
    }
}
