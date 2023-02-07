using System;
using System.Windows.Forms;
using AVS.CoreLib.WinForms;
using AVS.CoreLib.WinForms.Controls;
using AVS.CoreLib.WinForms.Grid;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Tool.Controls.MarketTools.Controllers;

namespace AVS.Trading.Tool.Controls.MarketTools
{
    
    public interface IMarketTradeHistoryView : IGridView
    {
        ITradeHistoryFiltersView Filters { get; }
        IFormView ParentView { get; }
    }

    /// <summary>
    /// Маркет трейдов происходит до фига поэтому выгребать данные за период в неделю или месяц с биржи не получится 
    /// т.к. биржа возвращает до 50 000 записей, поэтому нужно дата лоадером качать данные с биржи в локальную базу
    /// в т.ч. сразу делает преданализ данных и записывать в табличку 5 мин свертки (трейд саммари за малый таймфрейм)
    /// </summary>
    public partial class MarketTradeHistoryControl : UserControlEx, IMarketTradeHistoryView
    {
        #region prop-s
        protected MarketTradeHistoryController Controller;
        public ITradeHistoryFiltersView Filters => this.tradeHistoryFiltersControl1;
        public IGridControl GridControl => gridControl1;
        public ISummaryView SummaryView => tradeTotals;
        public ISelectedCellsSummaryView SelectedCellsSummaryView => selectedCellsTradeTotals;

        public string Market
        {
            get => tradeHistoryFiltersControl1.Market;
            set => tradeHistoryFiltersControl1.Market = value;
        }
        #endregion

        protected override void Initialize()
        {
            InitializeComponent();
            
            Controller = GetController<MarketTradeHistoryController>();
            gridControl1.Controller = Controller;
            gridControl1.Hightlighter.WithTradeTypeColorScheme(TypeColumn);
            gridControl1.Hightlighter.WithLowScaleColorScheme(TotalColumn);
        }

        private void btnLoadTradeHistory_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            btnLoadTradeHistory.Enabled = false;
            gridControl1.RunLoadDataAsync(this.Filters.GetFilters());
            groupBox2.Text = @"Trade history " + tradeHistoryFiltersControl1.Market;
            ParentView.FormTitle = groupBox2.Text;
        }

        private void gridControl1_LoadDataCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnLoadTradeHistory.Enabled = true;
        }

        private void viewSummaryMenuItem_Click(object sender, EventArgs e)
        {
            Controller.DisplaySelectedCellsSummary();
        }
    }

}
