using System;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.Interfaces.WalletTools;

namespace AVS.Trading.Data.Domain.WalletTools
{
    public class ActiveLoan: BaseEntity<long>, IActiveLoan
    {
        public double Rate { get; set; }
        public double Amount { get; set; }
        public int Duration { get; set; }
        public DateTime DateUtc { get; set; }
        public string Currency { get; set; }
        public double Fees { get; set; }
        public string Exchange { get; set; }
    }
}
