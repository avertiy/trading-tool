using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.MVC;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Framework.Services.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.TradingTools.ChildControls
{
    public interface IPostOrdersView
    {
        void SetStatus(string message, bool success);
    }

    public partial class PostOrdersControl : UserControl, IPostOrdersView
    {
        protected PostOrdersController Controller;
        public string Pair { get; set; }

        public PostOrdersControl()
        {
            InitializeComponent();
        }

        public void Initialize(CurrencyPair pair)
        {
            if (pair == null || pair.IsAll)
                throw new ArgumentException("pair is null or does not refer to a certain market");

            buyControl.Initialize(pair);
            sellControl.Initialize(pair);
            marginBuyControl.Initialize(pair);
            marginSellControl.Initialize(pair);
            Pair = pair.ToString();
            if (Controller == null)
            {
                Controller = EngineContext.Current.Resolve<PostOrdersController>();
                Controller.SetView(this);
            }
            SetStatus("");
            this.OnLoad(EventArgs.Empty);
        }

        public void Setup(MarketData marketData)
        {
            if(marketData == null)
                return;
            Initialize(CurrencyPair.ParsePair(marketData.Pair));
            buyControl.Price = marketData.LowestAsk;
            marginBuyControl.Price = marketData.LowestAsk;
            sellControl.Price = marketData.HighestBid;
            marginSellControl.Price = marketData.HighestBid;
        }

        public void SetStatus(string message, bool success = true)
        {
            var lbl = tabControl1.SelectedIndex == 0 ? lblStatus : lblMarginStatus;
            lbl.Text = message;
            lbl.ForeColor = success ? Color.DarkGreen : Color.DarkRed;
        }

        private void buyControl_ButtonClick(object sender, EventArgs e)
        {
            Controller.SubmitOrderAsync(Pair, OrderType.Buy, buyControl.Price, buyControl.Amount);
        }

        private void sellControl_ButtonClick(object sender, EventArgs e)
        {
            Controller.SubmitOrderAsync(Pair, OrderType.Sell, sellControl.Price, sellControl.Amount);
        }

        private void maginBuyControl_ButtonClick(object sender, EventArgs e)
        {
            if (marginBuyControl.LoanRate > 0.5)
            {
                MessageBox.Show(@"Be careful big loan rate might damage your profit and even cause losses. The rate > 0.5% is prohibited");
                return;
            }

            Controller.SubmitMarginOrderAsync(Pair, OrderType.Buy, 
                marginBuyControl.Price, marginBuyControl.Amount, marginBuyControl.LoanRate);
        }

        private void marginSellControl_ButtonClick(object sender, EventArgs e)
        {
            if (marginBuyControl.LoanRate > 0.5)
            {
                MessageBox.Show(@"Be careful big loan rate might damage your profit and even cause losses. The rate > 0.5% is prohibited");
                return;
            }

            Controller.SubmitMarginOrderAsync(Pair, OrderType.Sell, marginSellControl.Price, 
                marginSellControl.Amount, marginSellControl.LoanRate);
        }

        private async void PostOrdersControl_Load(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(Pair))
                return;

            var info = await Controller.LoadBalanceInfo(Pair);
            lblBalanceQuote.Text = info.QuoteAmount.FormatNumber(info.QuoteCurrency);
            lblBalanceBase.Text = info.BaseAmount.FormatNumber(info.BaseCurrency);
            lblTradableBalanceQuote.Text = info.QuoteTradableAmount.FormatNumber(info.QuoteCurrency);
            lblTradableBalanceBase.Text = info.BaseTradableAmount.FormatNumber(info.BaseCurrency);
        }

        
    }

    public class PostOrdersController : ControllerBase<IPostOrdersView>
    {
        private readonly ITradingToolsService _tradingToolsService;
        private readonly IMarginToolsService _marginToolsService;
        private readonly IWalletToolsService _walletToolsService;
        private readonly IWalletDataPreprocessor _walletDataPreprocessor;

        public PostOrdersController(ITradingToolsService tradingToolsService, IMarginToolsService marginToolsService, IWalletToolsService walletToolsService, IWalletDataPreprocessor walletDataPreprocessor)
        {
            _tradingToolsService = tradingToolsService;
            _marginToolsService = marginToolsService;
            _walletToolsService = walletToolsService;
            _walletDataPreprocessor = walletDataPreprocessor;
        }

        public async void SubmitOrderAsync(string market, OrderType type, double price, double amount)
        {
            View.SetStatus("", false);
            Response<IPostOrderResult> response =  await _tradingToolsService.SubmitOrderAsync(market, type, price, amount);
            if (!response.Success)
            {
                View.SetStatus(response.Error, false);
                return;
            }

            View.SetStatus($"Order #{response.Data.IdOrder}", true);
        }

        public async void SubmitMarginOrderAsync(string market, OrderType type, double price, double amount, double loanRate)
        {
            View.SetStatus("", false);
            Response<IPlaceOrderResult> response = await _marginToolsService.SubmitMarginOrderAsync(market, type, price, amount, loanRate);

            if (!response.Success)
            {
                View.SetStatus(response.Error, false);
                return;
            }

            View.SetStatus($"{response.Data.Message} [order #{response.Data.OrderNumber}]", true);
        }

        public async Task<BalanceInfo> LoadBalanceInfo(string pair)
        {
            var response = await _walletToolsService.GetCompleteBalancesAsync().ConfigureAwait(false);
            if (!response.Success)
            {
                View.SetStatus(response.Error, false);
                return new BalanceInfo();
            }
            CurrencyPair cp = CurrencyPair.Parse(pair);
            var info = _walletDataPreprocessor.GetBalanceInfo(cp, response.Data, null);
            return info;
        }
    }
}
