namespace AVS.Trading.Core.Interfaces.TradingTools
{
    public interface IMarginAccountSummary
    {
        double TotalValue { get; }
        double ProfitLoss { get; }
        double LendingFees { get; }
        double NetValue { get; }
        double TotalBorrowedValue { get; }
        double CurrentMargin { get; }
    }
}