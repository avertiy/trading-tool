using System;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace Jojatekok.PoloniexAPI.WalletTools
{
    public class PoloniexBalance : Response, IBalance
    {
        [JsonProperty("available")]
        public double QuoteAvailable { get; private set; }
        [JsonProperty("onOrders")]
        public double QuoteOnOrders { get; private set; }
        [JsonProperty("btcValue")]
        public double BitcoinValue { get; private set; }
        [JsonIgnore]
        public bool IsEmpty => Math.Abs(BitcoinValue) < 0.0000001;
        [JsonIgnore]
        public string Currency { get; set; }

        public override string ToString()
        {
            return $"{BitcoinValue} BTC; available: {QuoteAvailable.FormatNumber(Currency)}; on orders: {QuoteOnOrders.FormatNumber(Currency)}";
        }
    }

   
}
