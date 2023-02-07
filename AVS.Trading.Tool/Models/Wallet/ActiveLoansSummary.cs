using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Data.Domain.WalletTools;

namespace AVS.Trading.Tool.Models.Wallet
{
    public class ActiveLoansSummary
    {
        public double AvgRate { get; private set; }
        public double Fees { get; private set; }
        public double Amount { get; private set; }

        public void Initialize(IList<ActiveLoan> items)
        {
            if (items == null || items.Count == 0)
                return;
            Fees = items.Sum(i => i.Fees);
            Amount = items.Sum(i => i.Amount);
            AvgRate = items.Sum(i => i.Rate * i.Amount) / Amount;
        }
    }
}