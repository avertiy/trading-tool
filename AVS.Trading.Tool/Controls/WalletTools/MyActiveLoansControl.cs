using System;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.Controls;
using AVS.CoreLib.WinForms.Grid;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;

namespace AVS.Trading.Tool.Controls.WalletTools
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
            var ctx = EngineContext.Current.Resolve<IWorkContext>();
            if (ctx.Client.SupportWalletTools())
            {
                gridControl1.RunLoadDataAsync(Currency);
            }
            else
            {
                MessageBox.Show($"{ctx.Client.Exchange} exchange client does not support Wallet Tools");
            }
        }
    }
}
