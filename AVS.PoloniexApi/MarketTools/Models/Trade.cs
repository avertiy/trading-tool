using System;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.MarketTools.Models
{
    public class Trade : Response, IMarketTrade
    {
        [JsonProperty("date")]
        private string TimeInternal {
            set { DateUtc = DateTimeHelper.ParseUtcDateTime(value); }
        }
        
        public DateTime DateUtc { get; private set; }

        [JsonProperty("type")]
        private string TypeInternal {
            set { Type = value.ToTradeType(); }
        }
        public TradeType Type { get; private set; }
        /// <summary>
        /// Price per coin
        /// </summary>
        [JsonProperty("rate")]
        public double Price { get; private set; }

        [JsonProperty("amount")]
        public double AmountQuote { get; private set; }
        [JsonProperty("total")]
        public double AmountBase { get; private set; }



        public override string ToString()
        {
            return $"{AmountQuote.FormatNumber("")} x {Price} = {AmountBase.FormatNumber("")}";
        }
    }
}
