using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms;
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Tool.Controls.TradingTools.Controllers;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces;

namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    public interface IMyTradeBalancesView
    {
        IFormView ParentView { get; }
        string AmountQuote { get; set; }
        string AmountBase { get; set; }
        string AmountOnOrdersQuote { get; set; }
        string AmountOnOrdersBase { get; set; }
        bool Visible { get; set; }
    }

    public partial class MyTradeBalancesControl : LoadDataUserControl, IMyTradeBalancesView
    {
        protected MyTradeBalancesController Controller
        {
            get
            {
                var ctrl = EngineContext.Current
                    .Resolve<MyTradeBalancesController>();
                ctrl.SetView(this);
                return ctrl;
            }
        }

        protected string _market;

        #region prop-s
        public string AmountQuote
        {
            get => lblQuoteValue.Text;
            set => lblQuoteValue.Text = value;
        }

        public string AmountOnOrdersQuote
        {
            get => this.toolTip1.GetToolTip(this.lblQuoteValue);
            set => this.toolTip1.SetToolTip(this.lblQuoteValue, value);
        }

        public string AmountBase
        {
            get => lblBaseValue.Text;
            set => lblBaseValue.Text = value;
        }

        public string AmountOnOrdersBase
        {
            get => this.toolTip1.GetToolTip(this.lblBaseValue);
            set => this.toolTip1.SetToolTip(this.lblBaseValue, value);
        }

        public string AmountQuoteMargin
        {
            get => lblQuoteValueMargin.Text;
            set => lblQuoteValueMargin.Text = value;
        }
        public string AmountBaseMargin
        {
            get => lblBaseValueMargin.Text;
            set => lblBaseValueMargin.Text = value;
        }
        #endregion

        public MyTradeBalancesControl()
        {
            InitializeComponent();
        }
        
        public void LoadDataAsync(string market)
        {
            if(string.IsNullOrEmpty(market))
                return;
            _market = market;
            RunWorkerAsync(market);
        }

        protected override object DoWork(object argument)
        {
            return Controller.LoadData((string)argument); 
        }

        protected override void WorkCompleted(object result)
        {
            var pair = CurrencyPair.Parse(_market);
            var info = (BalanceInfo)result;
            AmountQuote = info.QuoteAmount.FormatNumber(pair.QuoteCurrency);
            AmountOnOrdersQuote = info.QuoteAmountOnOrders.FormatNumber(pair.QuoteCurrency);
            AmountQuoteMargin = info.QuoteTradableAmount.FormatNumber(pair.QuoteCurrency);

            AmountBase = info.BaseAmount.FormatNumber(pair.BaseCurrency);
            AmountOnOrdersBase = info.BaseAmountOnOrders.FormatNumber(pair.BaseCurrency);
            AmountBaseMargin = info.BaseTradableAmount.FormatNumber(pair.BaseCurrency);
            Visible = true;
        }
    }
}
