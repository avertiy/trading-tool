using System.Collections.Generic;
using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Framework.Services.LendingTools;
using AVS.Trading.Tool.Models.Wallet;
using AVS.Trading.Data.Domain.WalletTools;

namespace AVS.Trading.Tool.Controls.WalletTools
{
    public class MyLoansController : GridViewController<IMyLoansView, ActiveLoan>
    {
        private readonly ILendingToolsService _lendingTools;
        private readonly ILendingToolsDataPreprocessor _dataPreprocessor;

        public MyLoansController(ILendingToolsService lendingTools, ILendingToolsDataPreprocessor dataPreprocessor)
        {
            _lendingTools = lendingTools;
            _dataPreprocessor = dataPreprocessor;
        }

        public override object LoadData(object argument)
        {
            return LoadOpenOffers((string)argument);
        }

        protected IList<ActiveLoan> LoadOpenOffers(string currency)
        {
            return SafeExecute(() =>
            {
                View.GridControl.ReportProgress("Loading offers..", 1);
                var iactiveloans = _lendingTools.GetUsedActiveLoans(currency);
                //convert IActiveLoan to domain entity ActiveLoan
                var activeloans = _dataPreprocessor.PreprocessActiveLoans(iactiveloans);
                return activeloans;
            });
        }

        protected override void BindData_SummaryView(object source)
        {
            var summary = new ActiveLoansSummary();
            summary.Initialize((IList<ActiveLoan>)source);
            base.BindData_SummaryView(summary);
        }
    }
}