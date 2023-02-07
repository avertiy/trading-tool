using System;
using System.Collections.Generic;
using AVS.CoreLib.Utils;
using AVS.CoreLib.WinForms.Controls;
using AVS.CoreLib.WinForms.Grid;
using AVS.CoreLib.WinForms.MVC;
using AVS.Poloniex.Framework.Services.WalletTools;
using Jojatekok.PoloniexAPI.WalletTools;

namespace AVS.Poloniex.Controls.WalletTools
{
    public interface IMyLoansView : IGridView
    {
        string Currency { get; }
    }

    public partial class MyActiveLoansControl : UserControlEx, IMyLoansView
    {
        protected MyLoansController Controller;
        public IGridControl GridControl => gridControl1;
        public ISummaryView SummaryView => activeLoansSummaryControl1;
        public ISelectedCellsSummaryView SelectedCellsSummaryView => null;
        public string Currency
        {
            get => selectCurrencyControl2.Currency;
            set => selectCurrencyControl2.Currency = value;
        }

        protected override void Initialize()
        {
            InitializeComponent();
            Controller = GetController<MyLoansController>();
            gridControl1.Controller = Controller;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            gridControl1.RunLoadDataAsync(Currency);
        }
    }

    public class MyLoansController : GridViewController<IMyLoansView, ActiveLoan>
    {
        private readonly IWalletToolsService _walletTools;

        public MyLoansController(IWalletToolsService walletTools)
        {
            _walletTools = walletTools;
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
                List<ActiveLoan> offers = _walletTools.GetUsedLoans(currency);
                return offers;
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
