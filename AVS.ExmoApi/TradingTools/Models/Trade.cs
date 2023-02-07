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
    //trade_id - deal identifier
    //order_id - user’s order identifier
    //type - type of the deal
    //price - deal price
    //amount - total sum of the deal
    //date – date and time of the deal
    //quantity - currency quantity
    //pair - currency pair
    public class Trade :Response, ITrade
    {
        public Trade()
        {
            Category = TradeCategory.Exchange;
        }

        public Trade(string aOrderNumber, TradeType aType, double aPrice, double aAmountBase, DateTime aDateUtc)
        {
            this.DateUtc = aDateUtc;
            this.OrderNumber = aOrderNumber;
            this.Type = aType;
            this.Price = aPrice;
            this.AmountBase = aAmountBase;
            Category = TradeCategory.Exchange;
        }


        [JsonProperty("trade_id")]
        public string IdTrade { get; set; }
        [JsonProperty("order_id")]
        public string OrderNumber { get; set; }
        [JsonProperty("type")]
        private string TypeInternal
        {
            set { Type = value.ToTradeType(); }
        }
        public TradeType Type { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
        
        [JsonProperty("pair")]
        public string Market { get; set; }
        
        [JsonProperty("amount")]
        public double AmountBase { get; set; }
        
        [JsonProperty("date")]
        private string TimeInternal
        {
            set
            {
                DateUtc = ulong.Parse(value).UnixTimeStampToDateTime();
            }
        }
        public DateTime DateUtc { get; set; }

        [JsonIgnore]
        public TradeCategory Category { get; set; }

        [JsonIgnore]
        public double AmountQuote => AmountBase / Price;
        [JsonIgnore]
        public double Fee => 0;
        [JsonIgnore]
        public double TotalFee => Fee * AmountQuote;

        public string Exchange => ExmoConstants.ExmoExchange;

        public override string ToString()
        {
            return $"{Type} {AmountQuote} x {Price} {DateUtc}";
        }
    }

}
