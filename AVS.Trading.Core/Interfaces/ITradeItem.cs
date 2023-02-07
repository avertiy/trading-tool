using System;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.Interfaces
{
    public interface ITradeItem: IMarketTradeItem
    {
        double Fee { get; set; }
    }

    public interface ITradeCategory
    {
        TradeCategory Category { get; set; }
    }
}