using System;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IMarketTradeItem
    {
        string Pair { get; set; }
        DateTime DateUtc { get; set; }
        TradeType Type { get; set; }
        double Price { get; set; }
        double AmountQuote { get; set; }
        double AmountBase { get; set; }
    }
}