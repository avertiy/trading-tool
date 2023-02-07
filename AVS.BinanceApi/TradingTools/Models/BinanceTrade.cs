using System;
using AVS.BinanceApi.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using Newtonsoft.Json;
using AVS.Trading.Core.Helpers;

namespace AVS.BinanceApi.TradingTools.Models
{
    //[
    //{
    //    "symbol": "BNBBTC",
    //    "id": 28457,
    //    "orderId": 100234,
    //    "orderListId": -1, //Unless OCO, the value will always be -1
    //    "price": "4.00000100",
    //    "qty": "12.00000000",
    //    "quoteQty": "48.000012",
    //    "commission": "10.10000000",
    //    "commissionAsset": "BNB",
    //    "time": 1499865549590,
    //    "isBuyer": true,
    //    "isMaker": false,
    //    "isBestMatch": true
    //}
    //]
    public class BinanceTrade : ITrade
    {
        public BinanceTrade()
        {
            Category = TradeCategory.Exchange;
        }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("id")]
        public string IdTrade { get; set; }

        [JsonProperty("orderId")]
        public string OrderNumber { get; set; }
        
        [JsonProperty("price")]
        public double Price { get; set; }
        
        [JsonProperty("qty")]
        public double AmountQuote { get; set; }

        [JsonProperty("quoteQty")]
        public double AmountBase { get; set; }

        [JsonProperty("commission")]
        public double TotalFee { get; set; }

        [JsonProperty("commissionAsset")]
        public string CommissionAsset { get; set; }
        
        [JsonProperty("isBuyer")]
        private bool TypeInternal
        {
            set => Type = value ? TradeType.Buy : TradeType.Sell;
        }
        public TradeType Type { get; set; }

        [JsonProperty("time")]
        private string TimeInternal
        {
            set => DateUtc = DateTimeHelper.ParseUtcDateTimeFromUnixTimestamp(value, true);
        }
        public DateTime DateUtc { get; set; }

        [JsonIgnore]
        public TradeCategory Category { get; set; }
        
        [JsonIgnore]
        public double Fee => TotalFee / AmountQuote;
        

        public string Exchange => BinanceConstants.ExchangeName;

        public override string ToString()
        {
            return $"{Type} {AmountQuote} x {Price} {DateUtc}";
        }
    }

}
