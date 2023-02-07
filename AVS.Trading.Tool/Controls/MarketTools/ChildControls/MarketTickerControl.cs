using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Tool.Controls.MarketTools.Controllers;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.MarketTools.ChildControls
{
    public interface IMarketTickerView
    {
        void SetMarketData(MarketData data);
        void DisplayError(string message);
    }

    public partial class MarketTickerControl : UserControlEx, IMarketTickerView
    {
        #region properties

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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object DataSource
        {
            get => comboMarket.DataSource;
            set => comboMarket.DataSource = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Market
        {
            get => comboMarket.SelectedItem.ToString();
            set
            {
                InitializePairs();
                comboMarket.SelectedItem = value;
            }
        }

        [Category("Behavior")]
        [Description("The Price text initial value.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Price
        {
            get => lblPrice.Text;
            set => lblPrice.Text = value;
        }
        [Category("Behavior")]
        [Description("The Change text initial value.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Change
        {
            get => lblChange.Text;
            set => lblChange.Text = value;
        }
        [Category("Behavior")]
        [Description("The Volume text initial value.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string Volume
        {
            get => lblVolume.Text;
            set => lblVolume.Text = value;
        }
        [Category("Behavior")]
        [Description("The LowestAsk text initial value.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string LowestAsk
        {
            get => lblLowestAsk.Text;
            set => lblLowestAsk.Text = value;
        }
        [Category("Behavior")]
        [Description("The HighestBid text initial value.")]
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string HighestBid
        {
            get => lblHighestBid.Text;
            set => lblHighestBid.Text = value;
        }
        #endregion
        
        protected override void Initialize()
        {
            InitializeComponent();
            InitializePairs();
        }

        protected void InitializePairs()
        {
            try
            {
                var ctx = EngineContext.Current.Resolve<IWorkContext>();
                var pairs = ctx.Client.Pairs.GetAllPairs();
                comboMarket.DataSource = pairs;
                comboMarket.SelectedIndex = 0;
            }
            catch (NotInitializedEngineContextException ex) { }
        }
        
        public void SetMarketData(MarketData data)
        {
            var pair = CurrencyPair.ParsePair(data.Pair);
            SetPrice(data.PriceLast, pair.BaseCurrency);
            SetChange(data.PriceChange*100);
            SetVolume(data.Volume24HourBase, data.Volume24HourQuote, pair);
            SetHighestBid(data.HighestBid, pair.BaseCurrency);
            SetLowestAsk(data.LowestAsk, pair.BaseCurrency);
        }

        public void DisplayError(string message)
        {
            lblPrice.Text = message;
        }
        
        #region private setXXX methods

        private void SetPrice(double price, string currency)
        {
            lblPrice.Text = price.FormatNumber(currency);
        }

        private void SetChange(double change)
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

        private void SetVolume(double volumeBase, double volumeQuote, CurrencyPair pair)
        {
            lblVolume.Text = volumeBase.FormatNumber(pair.BaseCurrency);
            lblVolumeQuote.Text = volumeQuote.FormatNumber(pair.QuoteCurrency);
        }

        private void SetLowestAsk(double value, string currency)
        {
            lblLowestAsk.Text = value.FormatNumber(currency);
        }

        private void SetHighestBid(double value, string currency)
        {
            lblHighestBid.Text = value.FormatNumber(currency);
        }
        #endregion

        private async void comboMarket_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var data = await Controller.LoadMarketDataAsync(Market);

            if (data != null)
            {
                SetMarketData(data);
                var handler = MarketChanged;
                handler?.Invoke(this, data);
            }
        }

        public event EventHandler<MarketData> MarketChanged;
    }
}
