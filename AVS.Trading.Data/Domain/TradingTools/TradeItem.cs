using System;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces;

namespace AVS.Trading.Data.Domain.TradingTools
{
    /// <summary>
    /// Represents trades on crypto exchange
    /// Trade items loaded from exchange only
    /// don't confuse they are not based on MyOrders
    /// </summary>
    public class TradeItem : BaseEntity, ITradeItem, ITradeCategory
    {
        private string _pair;
        public string Pair
        {
            get => _pair;
            set
            {
                _pair = value;
                if(!_pair.Contains("_"))
                    throw new ArgumentException($"Invalid pair: {value} [pair must contain '_' character that separates currencies, e.g. BTC_LTC]");
            }
        }

        public string Exchange { get; set; }
        public TradeCategory Category { get; set; }
        public TradeType Type { get; set; }
        public double Price { get; set; }
        public double AmountQuote { get; set; }
        public double AmountBase { get; set; }
        public DateTime DateUtc { get; set; }

        public string OrderId { get; set; }
        public string TradeId { get; set; }
        
        
        /// <summary>
        /// fee percentage
        /// </summary>
        public double Fee { get; set; }
        /// <summary>
        /// in quote currency
        /// </summary>
        public double TotalFee { get; set; }
        
        public override string ToString()
        {
            return $"#{TradeId,-6} [OrderRef#{OrderId,-6}] {Type,-4} {Category,-11} {AmountQuote.FormatNumber(),-10} x {Price.FormatAsPrice(),-10} = {AmountBase.FormatNumber()} {DateUtc:g}";
        }
    }
}