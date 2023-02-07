using System.Collections.Generic;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Core.Interfaces.TradingTools
{
    public interface IMarginPosition
    {
        string Market { get; set; }
        double Amount { get; set; }
        double Total { get; set; }
        double BasePrice { get; set; }
        double LiquidationPrice { get; set; }
        double ProfitLoss { get; set; }
        double LendingFees { get; set; }
        PositionType Type { get; set; }
    }

    public interface IPlaceOrderResult
    {
        string OrderNumber { get; set; }
        string Market { get; set; }
        IList<ITrade> Trades { get; set; }
        bool Success { get; }
        string Message { get; }
    }


}