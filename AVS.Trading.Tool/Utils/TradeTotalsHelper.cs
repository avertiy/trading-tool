using System;
using System.Drawing;
using System.Windows.Forms;
using AVS.Trading.Tool.Models;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Tool.Utils
{
    public class TradeTotalsHelper
    {
        #region Properties
        private string AvgBuyPrice { get; set; }
        private string AvgSellPrice { get; set; }
        private string BuyVolume { get; set; }
        private string SellVolume { get; set; }
        private string BuyTotal { get; set; }
        private string SellTotal { get; set; }
        private string ProfitLossTotal { get; set; }
        private Color ProfitLossForeColor { get; set; }
        private Color ProfitLossSubtotalForeColor { get; set; }
        private string AvgBuyPriceTooltip { get; set; }
        private string AvgSellPriceTooltip { get; set; }
        private string Diff { get; set; }
        private string DiffAction { get; set; }
        private string ProfitLossAmount { get; set; }
        private string ProfitLossSubtotal { get; set; }
        private string SettlementAmount { get; set; }
        private string SettlementTotal { get; set; }
        #endregion

        public void BindBuyAndSellLabels(ToolTip toolTip, Label lblAvgBuyPrice, 
            Label lblBuyVolume,
            Label lblBuyTotal,
            Label lblAvgSellPrice,
            Label lblSellVolume,
            Label lblSellTotal)
        {
            lblAvgBuyPrice.Text = AvgBuyPrice;
            lblBuyVolume.Text = BuyVolume;
            lblBuyTotal.Text = BuyTotal;
            lblAvgSellPrice.Text = AvgSellPrice;
            lblSellVolume.Text = SellVolume;
            lblSellTotal.Text = SellTotal;

            toolTip.SetToolTip(lblAvgBuyPrice, AvgBuyPriceTooltip);
            toolTip.SetToolTip(lblAvgSellPrice, AvgSellPriceTooltip);

            toolTip.SetToolTip(lblBuyVolume, "Bought amount");
            toolTip.SetToolTip(lblSellVolume, "Sold amount");
            toolTip.SetToolTip(lblBuyTotal, "Total");
            toolTip.SetToolTip(lblSellTotal, "Total");
        }

        public void BindSettlementLabels(Label lblSettlementAmount, Label lblSettlementTotal)
        {
            lblSettlementAmount.Text = SettlementAmount;
            lblSettlementTotal.Text = SettlementTotal;
        }

        public void BindProfitLossLabels(
            Label lblProfitLossTotal,
            Label lblProfitLossAmount = null,
            Label lblProfitLossSubtotal = null,
            Label lblDiff = null,
            Label lblDiffAction = null)
        {
            lblProfitLossTotal.Text = ProfitLossTotal;
            lblProfitLossTotal.ForeColor = ProfitLossForeColor;
            if (lblDiff != null && lblDiffAction != null)
            {
                lblDiff.Text = Diff;
                lblDiffAction.Text = DiffAction;

            }

            if (lblProfitLossAmount != null && lblProfitLossSubtotal != null)
            {
                lblProfitLossAmount.Text = ProfitLossAmount;
                lblProfitLossSubtotal.Text = ProfitLossSubtotal;
                lblProfitLossSubtotal.ForeColor = ProfitLossSubtotalForeColor;
            }
        }

        public void Initialize(TradeTotals totals, CurrencyPair pair)
        {
            SetAvgBuyPrice(totals.AvgBuyPrice, pair.BaseCurrency);
            SetAvgSellPrice(totals.AvgSellPrice, pair.BaseCurrency);
            SetVolumes(totals.VolumeBought, totals.VolumeSold, pair.QuoteCurrency);
            SetTotals(totals.Buys, totals.Sells, pair.BaseCurrency);
            SetProfitLoss(totals.ProfitLossTotal, pair.BaseCurrency);
            SetPofitLossAmount(totals.ProfitLossAmount, pair.QuoteCurrency);
            SetPofitLossSubtotal(totals.ProfitLossSubtotal, pair.BaseCurrency);
        }
        
        public void Initialize(MarginTradeTotals totals, CurrencyPair pair)
        {
            this.Initialize((TradeTotals)totals, pair);
            if (totals.SettlementAmount > 0)
            {
                SettlementAmount = totals.SettlementAmount.FormatNumber(pair.QuoteCurrency);
                SettlementTotal = totals.SettlementTotal.FormatNumber(pair.BaseCurrency);
            }
        }

        #region Private methods
        private void SetAvgBuyPrice(double value, string currency)
        {
            AvgBuyPrice = value.FormatNumber(currency);
            AvgBuyPriceTooltip = $"Avg Buy Price [+1% - {(value * 1.01).FormatNumber(currency)}; +2% - {(value * 1.02).FormatNumber(currency)}; +4% - {(value * 1.04).FormatNumber(currency)}]";
        }

        private void SetAvgSellPrice(double value, string currency)
        {
            AvgSellPrice = value.FormatNumber(currency);
            AvgSellPriceTooltip = $"Avg Sell Price [1% - {(value / 1.01).FormatNumber(currency)}; 2% - {(value / 1.02).FormatNumber(currency)}; 4% - {(value / 1.04).FormatNumber(currency)}]";
        }

        private void SetVolumes(double bought, double sold, string currency)
        {
            BuyVolume = bought.FormatNumber(currency);
            SellVolume = sold.FormatNumber(currency);
            DiffAction = "to be sold";
            var diff = bought - sold;
            if (bought < sold)
            {
                DiffAction = "to be bought";
                diff = sold - bought;
            }
            Diff = $"{diff.FormatNumber(currency)}";
        }

        private void SetTotals(double totalBuys, double totalSells, string currency)
        {
            BuyTotal = totalBuys.FormatNumber(currency);
            SellTotal = totalSells.FormatNumber(currency);
        }

        private void SetProfitLoss(double value, string currency)
        {
            if (value > 0)
            {
                ProfitLossTotal = $@"+{value.FormatNumber(currency)}";
                ProfitLossForeColor = Color.DarkGreen;
            }
            else if (value < 0)
            {
                ProfitLossTotal = $@"-{value.Abs().FormatNumber(currency)}";
                ProfitLossForeColor = Color.DarkRed;
            }
            else
            {
                ProfitLossTotal = $@"0 {currency}";
                ProfitLossForeColor = Color.Gray;
            }
        }

        private void SetPofitLossAmount(double value, string currency)
        {
            ProfitLossAmount = value.FormatNumber(currency);
        }
        private void SetPofitLossSubtotal(double value, string currency)
        {

            if (value < 0)
            {
                ProfitLossSubtotal = $@"-{value.Abs().FormatNumber(currency)}";
                ProfitLossSubtotalForeColor = Color.DarkRed;
            }
            else if (value > 0)
            {
                ProfitLossSubtotal = $@"+{value.Abs().FormatNumber(currency)}";
                ProfitLossSubtotalForeColor = Color.DarkGreen;
            }
            else
            {
                ProfitLossTotal = $@"0 {currency}";
                ProfitLossSubtotalForeColor = Color.Gray;
            }
        } 
        #endregion
    }
}