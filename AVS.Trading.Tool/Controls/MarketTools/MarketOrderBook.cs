using System;
using System.Collections.Generic;
using AVS.CoreLib.WinForms;
using AVS.CoreLib.WinForms.Utils;
using AVS.Poloniex;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Tool.Controls.MarketTools.Controllers;
using AVS.Trading.Framework.Extensions;

using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.MarketTools
{
    public interface IOrderBookView: IStatusText
    {
        string Market { get; set; }
        double? AmountBaseThreshold { get; }
        double PriceRangeKoef { get; }
        IFormView ParentView { get; }
        void LoadData();
    }
    
    public partial class MarketOrderBook : MyUserControl, IOrderBookView
    {
        private OrderBookController _controller;
        protected OrderBookController Controller => _controller ?? (_controller = GetController<OrderBookController>());
        private readonly GridHightlighter _hightlighter = new GridHightlighter();
        
        #region Prop-s
        public string Market
        {
            get => marketTickerControl1.Market;
            set => marketTickerControl1.Market = value;
        }

        public double? AmountBaseThreshold
        {
            get
            {
                if (!cbFilter.Checked)
                    return null;
                string text = comboFilterAmount.SelectedText;
                if (comboFilterAmount.SelectedItem != null)
                    text = comboFilterAmount.SelectedItem.ToString();
                if (string.IsNullOrEmpty(text))
                    return null;
                return double.Parse(text);
            }
        }

        private void SetSellOrdersDataSource(IEnumerable<SellOrder> orders)
        {
            var items = orders.ForEach(delegate(SellOrder o)
            {
                return new
                {
                    AmountQuote = o.AmountQuote.FormatAsQuantity(),
                    Price = o.Price.FormatAsPrice(),
                    AmountBase = o.AmountBase.FormatAsQuantity(),
                    Sum = o.Sum.FormatAsQuantity(),
                    SumQuote = o.SumQuoteAmount.FormatAsQuantity()
                };
            });
            bindingSourceSellOrders.DataSource = items;
        }

        private void SetBuyOrdersDataSource(IEnumerable<BuyOrder> orders)
        {
            var items = orders.ForEach(o => new
            {
                AmountQuote = o.AmountQuote.FormatAsQuantity(),
                Price = o.Price.FormatAsPrice(),
                AmountBase = o.AmountBase.FormatAsQuantity(),
                Sum = o.Sum.FormatAsQuantity(),
                SumQuote = o.SumQuoteAmount.FormatAsQuantity()
            });
            bindingSourceBuyOrders.DataSource = items;
        }

        public double PriceRangeKoef => ((double)trackBar1.Value / trackBar1.Maximum)/2;

        public int ThrottleUpdates => cbThrottleUpdates.Checked ? (int)numericUpDown1.Value : -1;
        #endregion

        protected override void Initialize()
        {
            InitializeComponent();
            this.StatusLabel = toolStripStatusLabel1;
            if (cbThrottleUpdates.Checked)
                timer1.Start();
        }

        #region event handlers
        private void btnLoadOpenOrders_Click(object sender, EventArgs e)
        {
            btnLoadOpenOrders.Enabled = false;
            SafeExecute(LoadData, true);
            btnLoadOpenOrders.Enabled = true;
        }

        private void cbFilter_CheckedChanged(object sender, EventArgs e)
        {
            comboFilterAmount.Enabled = cbFilter.Checked;
        }

        private void cbThrottleUpdates_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = cbThrottleUpdates.Checked;
            timer1.Enabled = cbThrottleUpdates.Checked;
            if (timer1.Enabled)
                timer1.Start();
            else
                timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ThrottleUpdates <= 0)
                return;
            timer1.Interval = ThrottleUpdates * 1000;
            LoadData();
        } 
        #endregion
        
        public void LoadData()
        {
            StatusText = "Loading open orders";
            Controller.LoadOrderBook();
            //Controller.BookReady+=OnBookReady

            OrderBook orderBook = Controller.LoadOrderBook();
            
            SetSellOrdersDataSource(orderBook.SellOrders);
            SetBuyOrdersDataSource(orderBook.BuyOrders);

            var pair = CurrencyPair.Parse(orderBook.Pair);

            orderBookSummary1.Initialize(orderBook, pair);
            orderBookSummary1.Visible = true;
            
            
            if (cbHighlightWalls.Checked)
            {
                StatusText = "Hightlighting grids";
                _hightlighter.SetupScaleColorScheme(SellBaseColumn, pair);
                _hightlighter.Execute(gridSellOrders, ValueConverter);
                _hightlighter.Execute(gridBuyOrders, ValueConverter);
            }

            StatusText = "Ready";
            
            ParentView.FormTitle = pair + " order book";
            SellQuoteColumn.HeaderText = pair.QuoteCurrency;
            SellBaseColumn.HeaderText = pair.BaseCurrency;
            BuyQuoteColumn.HeaderText = pair.QuoteCurrency;
            BuyBaseColumn.HeaderText = pair.BaseCurrency;
        }

        private IComparable ValueConverter(object value)
        {
            //value might be a string of formatted double due to K (means 1000) or M (means million) letters
            //the default double.Parse does not work
            //so in this was we do conversion from the string e.g 10K  or 1.5M into double
            if (value is string str)
            {
                if (NumericHelper.TryParseDouble(str, out double d))
                {
                    return d;
                }
            }
            return value as IComparable;
        }

        private void panel3_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void marketTickerControl1_MarketChanged(object sender, MarketData e)
        {
            var cp = CurrencyPair.Parse(e.Pair);
            switch (cp.BaseCurrency)
            {
                case "UAH":
                case "USDT":
                case "USDC":
                {
                    this.comboFilterAmount.Items.Clear();
                    this.comboFilterAmount.Items.AddRange(new object[] {
                        "100",
                        "1000",
                        "5000",
                        "10000",
                        "50000"});
                    break;
                }
                default:
                {
                    this.comboFilterAmount.Items.Clear();
                    this.comboFilterAmount.Items.AddRange(new object[] {
                        "0.1",
                        "0.5",
                        "1",
                        "5",
                        "10",
                        "25",
                        "50",
                        "100"});
                    break;
                }
            }
        }
    }
}
