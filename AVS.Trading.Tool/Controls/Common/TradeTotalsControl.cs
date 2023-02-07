using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.Grid;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Tool.Models;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class TradeTotalsControl : BaseSummaryControl, ISelectedCellsSummaryView
    {
        #region Properties
        
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
        public string Total
        {
            get => lblTotal.Text;
            set => lblTotal.Text = value;
        }
        
        #endregion

        public TradeTotalsControl()
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

                summary.Initialize((IEnumerable) dataSource);
            }

            Initialize(summary);
        }

        public void Initialize(DataGridViewSelectedCellCollection selectedCells)
        {
            var summary = selectedCells.ToTradeSummary();
            Initialize(summary);
        }
        

        protected void Initialize(TradeSummary summary)
        {
            if (!summary.Exchange.IsEmpty)
            {
                IPairProvider pairProvider = EngineContext.Current.Resolve<IWorkContext>().Client.Pairs;
                var pair = CurrencyPair.Parse(summary.Pair) ?? new CurrencyPair("*", "*");
                Initialize(summary.Exchange, pair);
                Visible = true;
            }
        }

        protected void Initialize(TradeTotals totals, CurrencyPair pair)
        {
            SetAvgBuyPrice(totals.AvgBuyPrice, pair.BaseCurrency);
            SetAvgSellPrice(totals.AvgSellPrice, pair.BaseCurrency);
            SetBuyVolume(totals.VolumeBought, pair.QuoteCurrency);
            SetSellVolume(totals.VolumeSold, pair.QuoteCurrency);
            SetBuyTotal(totals.Buys, pair.BaseCurrency);
            SetSellTotal(totals.Sells, pair.BaseCurrency);
            SetTotal(totals.Buys - totals.Sells, pair.BaseCurrency);
        }

        #region Set methods
        public void SetAvgBuyPrice(double value, string currency)
        {
            lblAvgBuyPrice.Text = value.FormatNumber(currency);
        }

        public void SetAvgSellPrice(double value, string currency)
        {
            lblAvgSellPrice.Text = value.FormatNumber(currency);
        }

        public void SetBuyVolume(double value, string currency)
        {
            lblBuyVolume.Text = value.FormatNumber(currency);
        }

        public void SetSellVolume(double value, string currency)
        {
            lblSellVolume.Text = value.FormatNumber(currency);
        }

        public void SetBuyTotal(double value, string currency)
        {
            lblBuyTotal.Text = value.FormatNumber(currency);
        }

        public void SetSellTotal(double value, string currency)
        {
            lblSellTotal.Text = value.FormatNumber(currency);
        }

        public void SetTotal(double value, string currency)
        {
            if (value > 0)
            {
                lblTotal.Text = $@"+{value.FormatNumber(currency)}";
                lblTotal.ForeColor = Color.DarkGreen;
            }
            else if (value < 0)
            {
                lblTotal.Text = $@"-{value.Abs().FormatNumber(currency)}";
                lblTotal.ForeColor = Color.DarkRed;
            }
            else
            {
                lblTotal.Text = $@"0 {currency}";
                lblTotal.ForeColor = Color.Gray;
            }
        } 
        #endregion

        
    }
}
