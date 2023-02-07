using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Data.Domain.WalletTools;

namespace AVS.Trading.Framework.Services.LendingTools
{
    public interface ILendingToolsDataPreprocessor
    {
        IList<ActiveLoan> PreprocessActiveLoans(IList<IActiveLoan> items);
    }
    public class LendingToolsDataPreprocessor : ILendingToolsDataPreprocessor
    {
        public IList<ActiveLoan> PreprocessActiveLoans(IList<IActiveLoan> items)
        {
            return items.Select(delegate(IActiveLoan i)
            {
                var loan = new ActiveLoan();
                i.Map(loan);
                return loan;
            }).ToList();
        }
    }
}