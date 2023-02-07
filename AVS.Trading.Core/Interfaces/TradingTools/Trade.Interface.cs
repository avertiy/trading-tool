using System;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Core.Interfaces.TradingTools
{
    public interface ITrade 
    {
        string OrderNumber { get; }
        string IdTrade { get; }

        TradeType Type { get;  }
        TradeCategory Category { get;  }

        double Price { get;  }
        double AmountQuote { get; }
        double AmountBase { get; }
        /// <summary>
        /// fee percentage 0.0015 or 0.0025
        /// </summary>
        double Fee { get; }
        double TotalFee { get; }

        DateTime DateUtc { get; }

        string Exchange { get; }
    }
}
