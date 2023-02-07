using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Tool.Controls.MarketTools.ChildControls;
using AVS.Trading.Tool.Controls.MarketTools.Controllers;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    public partial class MarketPriceControl : UserControl, IMarketTickerView
    {
        protected MarketTickerController Controller
        {
            get
            {
                var ctrl = EngineContext.Current
                    .Resolve<MarketTickerController>();
                ctrl.SetView(this);
                return ctrl;
            }
        }

        public MarketPriceControl()
        {
            InitializeComponent();
        }

        [Category("Behavior")]
        [Description("The Market initial value.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Market { get; set; }

        public void SetChange(double change)
        {
            if (change > 0)
            {
                lblChange.Text = $@"+{change.Abs():N2}%";
                lblChange.ForeColor = Color.DarkGreen;
            }
            else
            {
                lblChange.Text = $@"-{change.Abs():N2}%";
                lblChange.ForeColor = Color.DarkRed;
            }
        }

        public void SetPriceToolip(double lowestAsk, double highestBid, double priceavg, string currency)
        {
            string ask = lowestAsk.FormatNumber(currency);
            string bid = highestBid.FormatNumber(currency);
            string avg = priceavg.FormatNumber(currency);
            
            var text =
                $"Lowest ask: {ask}   Highest bid: {bid}   Price avg: {avg}";
            this.toolTip1.SetToolTip(this.lblPrice, text);
            this.toolTip1.SetToolTip(this.lblVolume, text);
        }
       
        public void SetMarketData(MarketData data)
        {
            Market = data.Pair;
            var pair = CurrencyPair.Parse(data.Pair)?? CurrencyPair.Any;
            lblPrice.Text = data.PriceLast.FormatNumber(pair.BaseCurrency);
            var volumeQuote = data.Volume24HourQuote.FormatNumber(pair.QuoteCurrency);
            var volumeBase = data.Volume24HourBase.FormatNumber(pair.BaseCurrency);
            lblVolume.Text = $@"{volumeQuote} / {volumeBase}";
            SetChange(data.PriceChange);
            SetPriceToolip(data.LowestAsk, data.HighestBid, data.PriceAvg, pair.BaseCurrency);
            this.Visible = true;
        }

        public void DisplayError(string message)
        {
            lblVolume.Text = message;
        }

        public void LoadData(string market)
        {
            Controller.LoadMarketData(market);
        }
    }
}
