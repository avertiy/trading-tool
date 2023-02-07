using System;
using System.Collections;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Grid;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Tool.Models;
using AVS.Trading.Core;

namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    public partial class TradeSummaryTabControl : BaseSummaryControl, ISelectedCellsSummaryView
    {
        public TradeSummaryTabControl()
        {
            InitializeComponent();
        }

        protected override void DataBound(object dataSource)
        {
            var summary = new TradeSummary();
            if (dataSource != null)
            {
                if (!(dataSource is IEnumerable))
                    throw new ArgumentException("IEnumerable collection is expected as dataSource");

                summary.Initialize((IEnumerable)dataSource);
            }

            Initialize(summary, false);
        }

        public void Initialize(DataGridViewSelectedCellCollection selectedCells)
        {
            Initialize(selectedCells.ToTradeSummary(), true);
        }

        protected void Initialize(TradeSummary summary, bool selectedCells)
        {
            var pair = CurrencyPair.Parse(summary.Pair) ?? new CurrencyPair("BTC", "*");

            plSummaryExSelectedCells.Visible = selectedCells;
            plSummaryMarginSelectedCells.Visible = selectedCells;
            if (selectedCells)
            {
                plSummaryExSelectedCells.Initialize(summary.Exchange, pair );
                plSummaryMarginSelectedCells.Initialize(summary.Margin, pair);
            }
            else
            {
                plSummaryEx.Initialize(summary.Exchange, pair);
                plSummaryMargin.Initialize(summary.Margin, pair);
            }

            if (summary.Exchange.IsEmpty)
            {
                tabControl1.SelectedTab = tabPageMargin;
                tabPageMargin.Visible = true;
                tabPageEx.Visible = false;
            }

            if (summary.Margin.IsEmpty)
            {
                tabControl1.SelectedTab = tabPageEx;
                tabPageMargin.Visible = false;
                tabPageEx.Visible = true;
            }

            this.Visible = true;
        }
    }
}
