using System;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;

namespace AVS.Trading.Tool.Models.Trading
{
    public class TradeItemModel: ITradeItem, ITradeCategory
    {
        public TradeCategory Category { get; set; }
        public string CategoryDisplayText => (Category.ToString()).Substring(0, 1);

        public TradeType Type { get; set; }
        public double Price { get; set; }
        public double AmountQuote { get; set; }
        public double AmountBase { get; set; }
        public double Fee { get; set; }
        public string Pair { get; set; }
        public DateTime DateUtc { get; set; }
        public string Market { get; set; }

        public string Exchange { get; set; }
        //public string OrderId { get; set; }
        //public string TradeId { get; set; }
    }
}
