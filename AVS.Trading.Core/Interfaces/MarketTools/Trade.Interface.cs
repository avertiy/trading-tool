using System;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IMarketTrade 
    {
        DateTime DateUtc { get; }
        TradeType Type { get; }
        double Price { get; }

        double AmountQuote { get; }
        double AmountBase { get; }
    }
}
