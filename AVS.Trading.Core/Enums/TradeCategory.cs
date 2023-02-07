using System;

namespace AVS.Trading.Core.Enums
{
    [Flags]
    public enum TradeCategory
    {
        Exchange =1,
        MarginTrade =2,
        Settlement =4,
        LendingFees =8
    }
}