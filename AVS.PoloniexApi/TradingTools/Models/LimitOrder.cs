using System;
using AVS.PoloniexApi.General;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.TradingTools;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.TradingTools.Models
{
    /// <summary>
    /// Represents JSON 
    /// {
    ///     "orderNumber":"60481281191",
    ///     "type":"buy",
    ///     "rate":"0.00002230",
    ///     "startingAmount":
    ///     "300.00000000",
    ///     "amount":"300.00000000",
    ///     "total":"0.00669000",
    ///     "date":"2018-05-12 21:42:59",
    ///     "margin":1
    /// }
    /// </summary>
    public class LimitOrder : ILimitOrder
    {
        /// <summary>
        /// contains 11 digits (ulong)
        /// </summary>
        [JsonProperty("orderNumber")]
        public string IdOrder { get; set; }

        [JsonProperty("type")]
        private string TypeInternal {
            set => Type = value.ToOrderType();
        }
        public OrderType Type { get; set; }

        [JsonProperty("margin")]
        private int CategoryInternal
        {
            set => Account = value == 1 ? TradingAccount.Margin: TradingAccount.Exchange;
        }

        public TradingAccount Account { get; set; }
        /// <summary>
        /// Price per coin
        /// </summary>
        [JsonProperty("rate")]
        public double Price { get; set; }
        [JsonProperty("amount")]
        public double AmountQuote { get; set; }
        [JsonProperty("total")]
        public double AmountBase { get; set; }

        [JsonProperty("date")]
        private string TimeInternal
        {
            set { DateUtc = DateTimeHelper.ParseUtcDateTime(value); }
        }
        public DateTime DateUtc { get; set; }

        public string Exchange => PoloniexConstants.PoloniexExchange;


        public override string ToString()
        {
            return $"{Account} {Type}:  {AmountQuote} x {Price} = {AmountBase}";
        }
    }

    public class PostOrder : LimitOrder, IPostOrderResult
    {

    }

}
