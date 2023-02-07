using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib.Json.Converters;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.KunaApi.MarketTools.Models
{
    /// <summary>
    /// Market Data
    /// </summary>
    /// <code>
    /// json: [["btcuah",
    /// 208001,     # цена BID
    /// 11200693,   # объем ордербука BID
    /// 208499,     # цена ASK
    /// 29.255569,  # объем ордербука ASK
    /// 5999,       # изменение цены за 24 часа в котируемой валюте 
    /// -2.8,       # изменение цены за 24 часа в процентах
    /// 208001,     # последняя цена
    /// 11.3878,    # объем торгов за 24 часа в базовой валюте
    /// 215301,     # максимальная цена за 24 часа
    /// 208001      # минимальная цена за 24 часа ]]
    /// </code>
    [JsonConverter(typeof(ArrayConverter))]
    public class KunaMarketData : IMarketData
    {
        /// <summary>
        /// The symbol e.g. BTCUSD
        /// </summary>
        [ArrayProperty(0)]
        public string Symbol { get; set; }
        /// <summary>
        /// The best bid price
        /// </summary>
        [ArrayProperty(1)]
        public double HighestBid { get; set; }
        /// <summary>
        /// The best bid size
        /// </summary>
        [ArrayProperty(2)]
        public double BidSize { get; set; }
        /// <summary>
        /// The best ask price
        /// </summary>
        [ArrayProperty(3)]
        public double LowestAsk { get; set; }
        /// <summary>
        /// The best ask size
        /// </summary>
        [ArrayProperty(4)]
        public double AskSize { get; set; }
        /// <summary>
        /// Change versus 24 hours ago
        /// </summary>
        [ArrayProperty(5)]
        public double PriceChange { get; set; }
        /// <summary>
        /// Change percentage versus 24 hours ago
        /// </summary>
        [ArrayProperty(6)]
        public double PriceChangePercentage { get; set; }
        /// <summary>
        /// The last trade price
        /// </summary>
        [ArrayProperty(7)]
        public double PriceLast { get; set; }

        /// <summary>
        /// The 24 hour volume
        /// </summary>
        [ArrayProperty(8)]
        public double Volume24HourBase { get; set; }
        /// <summary>
        /// The 24 hour high price
        /// </summary>
        [ArrayProperty(9)]
        public double High { get; set; }
        /// <summary>
        /// The 24 hour low price
        /// </summary>
        [ArrayProperty(10)]
        public double Low { get; set; }

        public override string ToString()
        {
            return $"{Symbol} bid: {HighestBid.FormatAsPrice()} ask: {LowestAsk.FormatAsPrice()}";
        }
    }
}
