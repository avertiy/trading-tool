using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib._System.Net;
using AVS.KunaApi.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.KunaApi.TradingTools.Models
{
    public class LimitOrder : Response, ILimitOrder
    {
        [JsonProperty("id")]
        public string IdOrder { get; set; }
        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("side")]
        private string TypeInternal
        {
            set => Type = value.ToOrderType();
        }
        public OrderType Type { get; set; }

        [JsonProperty("ord_type")]
        private string KindInternal
        {
            set => Kind = value.ToOrderKind();
        }
        public OrderKind Kind { get; set; }
        
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("volume")]
        public double AmountQuote { get; set; }

        [JsonProperty("remaining_volume")]
        public double RemainingVolume { get; set; }

        [JsonProperty("executed_volume")]
        public double ExecutedVolume { get; set; }
        
        [JsonProperty("trades_count")]
        public int TradesCount { get; set; }

        //value comes as UTC 
        [JsonProperty("created_at")]
        private string TimeInternal
        {
            set { DateUtc = DateTime.Parse(value); }
        }
        public DateTime DateUtc { get; set; }


        public double AmountBase => AmountQuote * Price;
        public TradingAccount Account => TradingAccount.Exchange;
        public string Exchange => KunaConstants.ExchangeName;
    }
}