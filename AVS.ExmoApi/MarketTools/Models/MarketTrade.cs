using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.ExmoApi.MarketTools.Models
{
    /// <summary>
    /// represents MarketTrade Exmo response for "trades" command
    /// </summary>
    /// <example>
    ///"trade_id": 3,
    ///"type": "sell",
    ///"price": "100",
    ///"quantity": "1",
    ///"amount": "100",
    ///"date": 1435488248
    /// </example>
    class MarketTrade: IMarketTrade
    {
        //public string Market { get; set; }

        [JsonProperty("trade_id")]
        public string IdTrade { get; set; }
        [JsonProperty("type")]
        private string TypeInternal
        {
            set { Type = value.ToTradeType(); }
        }
        public TradeType Type { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
        
        [JsonProperty("quantity")]
        public double AmountQuote { get; set; }

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
    }
}