using System;
using AVS.CoreLib._System.Net;
using AVS.PoloniexApi.General;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using Jojatekok.PoloniexAPI;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.TradingTools.Models
{
    /// <summary>
    /// Represents JSON object
    /// { 
    ///    "globalTradeID": 25129732, 
    ///    "tradeID": "6325758", 
    ///    "date": "2016-04-05 08:08:40",     
    ///    "amount": "0.10000000", 
    ///    "total": "0.00256549", 
    ///    "fee": "0.00200000", 
    ///    "orderNumber": "34225313575", 
    ///    "type": "sell", 
    ///    "category": "exchange" 
    /// }
    /// </summary>
    public class Trade : Response, ITrade
    {
        [JsonProperty("tradeID")]
        public string IdTrade { get; set; }

        /// <summary>
        /// orderNumber
        /// </summary>
        [JsonProperty("orderNumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("type")]
        private string TypeInternal
        {
            set { Type = value.ToTradeType(); }
        }
        public TradeType Type { get; set; }

        [JsonProperty("rate")]
        public double Price { get; set; }
        [JsonProperty("amount")]
        public double AmountQuote { get; set; }
        [JsonProperty("total")]
        public double AmountBase { get; set; }

        [JsonProperty("fee")]
        public double Fee { get; set; }

        public double TotalFee => AmountQuote * Fee;


        [JsonProperty("category")]
        private string TradeCategory
        {
            set => Category = value.ToTradeCategory();
        }

        public TradeCategory Category { get; set; } 

        [JsonProperty("date")]
        private string TimeInternal {
            set { DateUtc = DateTime.SpecifyKind(DateTime.Parse(value), DateTimeKind.Utc); }
        }
        public DateTime DateUtc { get; set; }

        public string Exchange => PoloniexConstants.PoloniexExchange;

        public override string ToString()
        {
            return $"{Category} {Type} {AmountQuote} x {Price} {DateUtc:g}";
        }
    }
}
