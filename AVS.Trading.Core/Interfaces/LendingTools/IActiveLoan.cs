using System;

namespace AVS.Trading.Core.Interfaces.LendingTools
{
    public interface IActiveLoan 
    {
        long Id { get; }
        double Rate { get; }
        double Amount { get; }
        int Duration { get; }
        DateTime DateUtc { get; }
        string Currency { get; set; }
        double Fees { get; }
        string Exchange { get; }
    }
}