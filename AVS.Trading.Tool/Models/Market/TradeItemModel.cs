using System;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Tool.Models.Market
{
    public class MarketTradeItemModel: IMarketTradeItem
    {
        
        public TradeType Type { get; set; }
        public double Price { get; set; }
        public double AmountQuote { get; set; }
        public double AmountBase { get; set; }
        public string Pair { get; set; }
        public DateTime DateUtc { get; set; }
        public string Market { get; set; }

        public string Exchange { get; set; }
        //public string OrderId { get; set; }
        //public string TradeId { get; set; }
    }
}
