using System;
using System.Collections.Generic;
using AVS.Trading.Core.Interfaces.WalletTools;
using Newtonsoft.Json;

namespace AVS.ExmoApi.Models
{
    public class ExmoUserInfo
    {
        [JsonProperty("uid")]
        public long Id { get; set; }
        [JsonIgnore]//server_date = in long format like 1435518576
        public DateTime ServerDate { get; set; }

        [JsonProperty("balances")]
        public Dictionary<string, double> Balances { get; set; }
        [JsonProperty("reserved")]
        public Dictionary<string, double> Reserved { get; set; }
    }


    public class ExmoBalance:IBalance
    {

        public double QuoteAvailable { get; set; }
        public double QuoteOnOrders { get; set; }
        public double BitcoinValue { get; set; }

        [JsonIgnore]
        public bool IsEmpty => Math.Abs(QuoteAvailable) < 0.0000001;
        [JsonIgnore]
        public string Currency { get; set; }
    }
}