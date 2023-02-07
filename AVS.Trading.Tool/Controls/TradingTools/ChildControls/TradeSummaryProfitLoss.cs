using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Tool.Models;
using AVS.Trading.Tool.Utils;
using AVS.Trading.Core;

namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    public partial class TradeSummaryProfitLoss : UserControlEx
    {
        protected override void Initialize()
        {
            InitializeComponent();
            WireAllControls(this);
        }

        public void Initialize(TradeTotals totals, CurrencyPair pair)
        {
            var helper = new TradeTotalsHelper();
            helper.Initialize(totals, pair);
            helper.BindBuyAndSellLabels(toolTip1, lblAvgBuyPrice, lblBuyVolume, lblBuyTotal, lblAvgSellPrice,
                lblSellVolume, lblSellTotal);
            helper.BindProfitLossLabels(lblPLTotal, lblPLAmount, lblPLSubTotal, lblDiff, lblAction);
            SwitchSettlements(false);
        }

        public void Initialize(MarginTradeTotals totals, CurrencyPair pair)
        {
            var helper = new TradeTotalsHelper();
            helper.Initialize(totals, pair);
            helper.BindBuyAndSellLabels(toolTip1, lblAvgBuyPrice, lblBuyVolume, lblBuyTotal, lblAvgSellPrice,
                lblSellVolume, lblSellTotal);
            helper.BindProfitLossLabels(lblPLTotal, lblPLAmount, lblPLSubTotal, lblDiff, lblAction);

            SwitchSettlements(totals.SettlementAmount > 0);
            if (totals.SettlementAmount > 0)
            {
                helper.BindSettlementLabels(lblSettlementAmount, lblSettlementTotal);
            }
        }

        private void SwitchSettlements(bool value)
        {
            lblSettlementLabel.Visible = value;
            lblSettlementTotal.Visible = value;
            lblSettlementAmount.Visible = value;
        }
    }
}