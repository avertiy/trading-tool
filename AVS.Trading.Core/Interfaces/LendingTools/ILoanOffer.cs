using System;

namespace AVS.Trading.Core.Interfaces.LendingTools
{
    public interface ILoanOffer
    {
        long Id { get; }
        double Rate { get; }
        double Amount { get; }
        int Duration { get; }
        DateTime DateUtc { get; }
        string Currency { get; }

        string Exchange { get; }
    }

    public interface IOpenLoanOffer : ILoanOffer
    {
        bool AutoRenew { get; }
    }
}