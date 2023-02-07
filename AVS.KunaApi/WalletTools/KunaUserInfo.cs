using System;
using System.Collections.Generic;
using AVS.Trading.Core.Interfaces.WalletTools;
using Newtonsoft.Json;

namespace AVS.KunaApi.WalletTools
{
//    "email": email,
//    "activated": whether an account is activated,
//    "accounts": an array of assets
//[{
//    "currency": currency,
//    "balance": the available amount,
//    "locked": the locked amount,
//}]

    public class KunaUserInfo
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("activated")]
        public bool Activated { get; set; }

        [JsonProperty("accounts")]
        public List<KunaBalance> Balances { get; set; }
    }

    public class KunaBalance : IBalance
    {
        private string _currency;

        [JsonProperty("balance")]
        public double QuoteAvailable { get; set; }
        [JsonProperty("locked")]
        public double QuoteOnOrders { get; set; }
        public double BitcoinValue { get; set; }

        [JsonIgnore]
        public bool IsEmpty => Math.Abs(QuoteAvailable + QuoteOnOrders) < 0.0000001;

        [JsonProperty("currency")]
        public string Currency
        {
            get => _currency;
            set => _currency = value.ToUpper();
        }

        public override string ToString()
        {
            return $"available: {QuoteOnOrders}{Currency}; on orders: {QuoteOnOrders}{Currency}";
        }
    }
}