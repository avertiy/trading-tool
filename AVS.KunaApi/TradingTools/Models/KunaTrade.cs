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
    // [{
    //    "id": trade ID,
    //    "price": price for 1 BTC,
    //    "volume": volume in BTC,
    //    "funds": amount in UAH,
    //    "market": market ID,
    //    "created_at": the time of trade,
    //    "side": bid or ask
    //}]
    public class KunaTrade : ITrade
    {
        public KunaTrade()
        {
            Category = TradeCategory.Exchange;
        }

        //public Trade(string aOrderNumber, TradeType aType, double aPrice, double aAmountBase, DateTime aDateUtc)
        //{
        //    this.DateUtc = aDateUtc;
        //    this.OrderNumber = aOrderNumber;
        //    this.Type = aType;
        //    this.Price = aPrice;
        //    this.AmountBase = aAmountBase;
        //    Category = TradeCategory.Exchange;
        //}
        
        public string OrderNumber { get; set; }

        [JsonProperty("id")]
        public string IdTrade { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
        
        [JsonProperty("volume")]
        public double AmountQuote { get; set; }

        [JsonProperty("funds")]
        public double AmountBase { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }
        
        [JsonProperty("side")]
        private string TypeInternal
        {
            set { Type = value.ToTradeType(); }
        }
        public TradeType Type { get; set; }
        
        [JsonProperty("created_at")]
        private string TimeInternal
        {
            set
            {
                DateUtc = DateTime.Parse(value);
            }
        }
        public DateTime DateUtc { get; set; }

        [JsonIgnore]
        public TradeCategory Category { get; set; }

        
        
        [JsonIgnore]
        public double Fee => 0.0025;
        [JsonIgnore]
        public double TotalFee => Fee * AmountQuote;

        public string Exchange => KunaConstants.ExchangeName;

        public override string ToString()
        {
            return $"{Type} {AmountQuote} x {Price} {DateUtc}";
        }
    }

}
