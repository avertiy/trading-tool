using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.WinForms;
using AVS.CoreLib.WinForms.Controls;
using AVS.CoreLib.WinForms.Grid;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Tool.Controls.TradingTools.Controllers;
using AVS.Trading.Tool.Forms.MarketTools;
using AVS.Trading.Tool.Utils;

namespace AVS.Trading.Tool.Controls.TradingTools
{
    public interface IMyTradeHistoryView : IGridView
    {
        ITradeHistoryFiltersView Filters { get; }
        IFormView ParentView { get; }
    }

    public partial class MyTradeHistoryControl : UserControlEx, IMyTradeHistoryView
    {
        protected MyTradeHistoryController Controller;
        public ITradeHistoryFiltersView Filters => this._tradeHistoryFiltersControl1;
        public IGridControl GridControl => gridControl;
        public ISummaryView SummaryView => tradeSummaryTabControl1;
        public ISelectedCellsSummaryView SelectedCellsSummaryView => tradeSummaryTabControl1;//selectedCellsTradeSummary;
        
        protected override void Initialize()
        {
            InitializeComponent();
            Controller = GetController<MyTradeHistoryController>();
            gridControl.Controller = Controller;
            gridControl.Hightlighter.WithTradeTypeColorScheme(ColumnType);
            gridControl.Hightlighter.WithTradeCategoryColorScheme(ColumnCategory);
            gridControl.CellFormatter = new PriceCellFormatter();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            btnLoad.Enabled = false;
            var filters = this.Filters.GetFilters();
            gridControl.RunLoadDataAsync(filters);
            marketPriceControl1.Visible = false;
            ParentView.FormTitle = this.Filters.Market + " trade history";
        }

        private void gridControl1_LoadDataCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                marketPriceControl1.LoadData(Filters.Market);
                myOpenOrdersControl1.LoadDataAsync(Filters);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
                btnLoad.Enabled = true;
            }
        }
        

        private void myOpenOrdersControl1_LoadDataCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnLoad.Enabled = true;
        }


        private void menuItemCalc_Click(object sender, EventArgs e)
        {
            
        }

        private void viewSummaryMenuItem_Click(object sender, EventArgs e)
        {
            Controller.DisplaySelectedCellsSummary();
        }

        private void MyTradeHistoryControl_Load(object sender, EventArgs e)
        {
            if(this.ParentForm !=null && ParentView != null)
                ParentView.FormTitle = this.Filters.Market+ " trade history";
        }

        private void menuItemOpenOrderBook_Click(object sender, EventArgs e)
        {
            var frm = new OrderBookForm();
            //don't use object initialization due to components initialization called inside of c-tor it might override the value with a default value
            frm.Market = Filters.Market;
            frm.Show();
            frm.LoadData();
        }
    }
}
