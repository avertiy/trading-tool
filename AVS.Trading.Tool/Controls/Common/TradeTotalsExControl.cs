using System.ComponentModel;
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Tool.Models;
using AVS.Trading.Tool.Utils;
using AVS.Trading.Core;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class TradeTotalsExControl : UserControlEx//, ITradeTotalsView
    {
        #region Properties
        [Category("Appearance")]
        [Description("Trade totals title text (Exchange or Margin)")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string AvgBuyPrice
        {
            get => lblAvgBuyPrice.Text;
            set => lblAvgBuyPrice.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string AvgSellPrice
        {
            get => lblAvgSellPrice.Text;
            set => lblAvgSellPrice.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string BuyVolume
        {
            get => lblBuyVolume.Text;
            set => lblBuyVolume.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string SellVolume
        {
            get => lblSellVolume.Text;
            set => lblSellVolume.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string BuyTotal
        {
            get => lblBuyTotal.Text;
            set => lblBuyTotal.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string SellTotal
        {
            get => lblSellTotal.Text;
            set => lblSellTotal.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string ProfitLoss
        {
            get => lblProfitLoss.Text;
            set => lblProfitLoss.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string SettlementAmount
        {
            get => lblSettlementAmount.Text;
            set => lblSettlementAmount.Text = value;
        }
        [Category("Appearance")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string SettlementTotal
        {
            get => lblSettlementTotal.Text;
            set => lblSettlementTotal.Text = value;
        }

        public int VisiblePanels { get; set; }


        #endregion

        protected override void Initialize()
        {
            InitializeComponent();
            WireAllControls(this);
        }
        
        public void Initialize(TradeTotals totals, CurrencyPair pair)
        {
            var helper = new TradeTotalsHelper();
            helper.Initialize(totals,pair);
            helper.BindBuyAndSellLabels(toolTip1,lblAvgBuyPrice,lblBuyVolume,lblBuyTotal,lblAvgSellPrice,lblSellVolume,lblSellTotal);
            helper.BindProfitLossLabels(lblProfitLoss);
            SetupBoxPanels(totals.VolumeBought > 0, totals.VolumeSold > 0, false);
        }

        public void Initialize(MarginTradeTotals totals, CurrencyPair pair)
        {
            var helper = new TradeTotalsHelper();
            helper.Initialize(totals, pair);
            helper.BindBuyAndSellLabels(toolTip1, lblAvgBuyPrice, lblBuyVolume, lblBuyTotal, lblAvgSellPrice, lblSellVolume, lblSellTotal);
            helper.BindProfitLossLabels(lblProfitLoss);
            
            if (totals.SettlementAmount > 0)
            {
                helper.BindSettlementLabels(lblSettlementAmount, lblSettlementTotal);
            }

            SetupBoxPanels(totals.VolumeBought > 0, totals.VolumeSold > 0, totals.SettlementAmount > 0);
        }

        private void SetupBoxPanels(bool hasBuys, bool hasSells, bool hasSettlement)
        {
            pnlBuy.Visible = hasBuys;
            pnlSell.Visible = hasSells;
            pnlSettlement.Visible = hasSettlement;
            pnlLabels.Visible = hasBuys || hasSells || hasSettlement;
            VisiblePanels = (hasBuys ? 1 : 0) + (hasSells ? 1 : 0) + (hasSettlement ? 1 : 0);

            //panels were not refreshed so we can't use here pnlBuy.Visible
            if (hasBuys)
            {
                pnlBuy.Left = 100;
                if (hasSells)
                {
                    pnlSell.Left = 275;
                    pnlSettlement.Left = 450;
                }
                else
                {
                    pnlSettlement.Left = 275;
                }
            }
            else
            {
                if (hasSells)
                {
                    pnlSell.Left = 100;
                    pnlSettlement.Left = 275;
                }
                else
                {
                    pnlSell.Left = 100;
                }
            }
        }
    }
}
