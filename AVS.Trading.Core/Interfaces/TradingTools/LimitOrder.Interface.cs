using System;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Core.Interfaces.TradingTools
{
    public interface ILimitOrder 
    {
        string IdOrder { get; set; }

        OrderType Type { get;  }
        TradingAccount Account { get;  }
        DateTime DateUtc { get; }
        string Exchange { get; }

        double Price { get; }
        double AmountQuote { get; }
        double AmountBase { get; }
    }

    public interface IPostOrderResult 
    {
        string IdOrder { get; set; }
    }
}
