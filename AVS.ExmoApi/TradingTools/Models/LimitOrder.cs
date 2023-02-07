using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib._System.Net;
using AVS.ExmoApi.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.ExmoApi.TradingTools.Models
{
    public class LimitOrder : Response, ILimitOrder
    {
        [JsonProperty("order_id")]
        public string IdOrder { get; set; }
        [JsonProperty("pair")]
        public string Market { get; set; }

        [JsonProperty("type")]
        private string TypeInternal
        {
            set => Type = value.ToOrderType();
        }

        public OrderType Type { get; set; }

        public TradingAccount Account => TradingAccount.Exchange;

        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("quantity")]
        public double AmountQuote { get; set; }
        [JsonProperty("amount")]
        public double AmountBase { get; set; }

        [JsonProperty("date")]
        private string TimeInternal
        {
            set { DateUtc = ulong.Parse(value).UnixTimeStampToDateTime(); }
        }
        public DateTime DateUtc { get; set; }

        public string Exchange => ExmoConstants.ExmoExchange;
    }
}