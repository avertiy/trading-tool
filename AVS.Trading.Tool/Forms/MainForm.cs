using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.Poloniex;
using AVS.PoloniexApi;
using AVS.Trading.Tool.Forms.Dialogs;
using AVS.Trading.Tool.Forms.MarketTools;
using AVS.Trading.Tool.Forms.TradingTools;
using AVS.Trading.Tool.Forms.WalletTools;
using AVS.Trading.Framework.Infrastructure;
using AVS.PoloniexApi.PoloniexCommands;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Tool.Forms
{
    public partial class MainForm : MyBaseForm, IMainFormView
    {
        protected MainFormController Controller;
        public MainForm()
        {
            InitializeComponent();
            Controller = new MainFormController();
        }

        public string StatusText
        {
            get => toolStripStatusLabel1.Text;
            set
            {
                toolStripStatusLabel1.Text = value;
                toolStripStatusLabel1.GetCurrentParent().Refresh();
            }
        }

        #region Menu item event handlers
        private void menuItemPing_Click(object sender, EventArgs e)
        {
            try
            {
                StatusText = Controller.PingPoloniex();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.InnerException?.ToString() ?? "", ex.Message);
            }
        }

        private void menuItemOpenOrders_Click(object sender, EventArgs e)
        {
            var frm = new OrderBookForm
            {
                Market = myTradeHistoryControl1.Filters.Market
            };
            frm.Show();
        }

        private void menuItemMarketTradeHistory_Click(object sender, EventArgs e)
        {
            var frm = new TradeHistoryForm()
            {
                Market = myTradeHistoryControl1.Filters.Market
            };
            frm.LoadData();
            frm.Show();
        }

        private void menuItemTradeHistory_Click(object sender, EventArgs e)
        {
            var frm = new TradingToolsForm();
            frm.Show();
        }

        private void menuItemExecuteCommand_Click(object sender, EventArgs e)
        {
            var client = EngineContext.Current.Resolve<PoloniexClient>();
            var command = TradingCommands.ReturnBalances;
            var result = client.ExecuteCommand(command);
            MessageBox.Show(result.JsonText, $@"Poloniex Trading API {command} result");
        }

        private void myOrdersMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new MyOrdersForm();
            frm.Initialize(myTradeHistoryControl1.Filters.Market);
            frm.Show();
        }

        private void loanOffersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new MyLoansForm
            {
                Currency = "BTC"
            };
            frm.Show();
        }

        private void menuItemCandles_Click(object sender, EventArgs e)
        {
            var frm = new CandlesForm();
            frm.Initialize(myTradeHistoryControl1.Filters.Market);
            frm.Show();
        }

        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //if (WindowState == FormWindowState.Minimized)
              //  Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show();
            //WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItemExmo_Click(object sender, EventArgs e)
        {
            SwitchExchange(Constants.Exchanges.Exmo, ((ToolStripMenuItem)sender).Image);
        }

        private void toolStripMenuItemPoloniex_Click(object sender, EventArgs e)
        {
            var config = EngineContext.Current.Resolve<TradingAppConfig>();
            var poloniexConfig = config.Exchanges.GetExchange(Constants.Exchanges.Poloniex);
            if (poloniexConfig == null)
                return;

            SwitchExchange(Constants.Exchanges.Poloniex, ((ToolStripMenuItem)sender).Image, poloniexConfig.Keys.FirstOrDefault());
        }

        private void ToolStripMenuItemKuna_Click(object sender, EventArgs e)
        {
            SwitchExchange(Constants.Exchanges.Kuna, ((ToolStripMenuItem)sender).Image);
        }

        private void ToolStripMenuItemPoloniex2_Click(object sender, EventArgs e)
        {
            var config = EngineContext.Current.Resolve<TradingAppConfig>();
            var poloniexConfig = config.Exchanges.GetExchange(Constants.Exchanges.Poloniex);
            if (poloniexConfig == null)
                return;
            
            SwitchExchange(Constants.Exchanges.Poloniex, ((ToolStripMenuItem)sender).Image, 
                poloniexConfig.Keys.Last());
        }

        private void toolStripMenuItemBinance_Click(object sender, EventArgs e)
        {
            var config = EngineContext.Current.Resolve<TradingAppConfig>();
            var binanceConfig = config.Exchanges.GetExchange(Constants.Exchanges.Binance);
            if (binanceConfig == null)
                return;

            SwitchExchange(Constants.Exchanges.Binance, ((ToolStripMenuItem)sender).Image,
                binanceConfig.Keys.Last());
        }

        private void SwitchExchange(string exchange, Image image, ApiKey apiKey = null)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            workContext.Exchange = exchange;
            toolStripSwitchExchange.ToolTipText = $@"Current exchange is {exchange.ToString()} {apiKey?.Name}";
            toolStripSwitchExchange.Image = image;
            if (apiKey != null)
            {
                workContext.SwitchAccount(apiKey);
            }
            LoadCurrentMargin();
        }

        private void apiKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new SelectApiKeyDialog();
            dlg.Initialize();
            dlg.ShowDialog();
        }

        private void LoadCurrentMargin()
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();

            var marginTools = workContext.Client.MarginTools;
            if (marginTools == null)
            {
                statusLabel.Text = string.Empty;
            }
            else
            {
                var response = marginTools.GetMarginAccountSummary();
                if (response.Success)
                {
                    var summary = response.Data;
                    statusLabel.Text = $@"Current margin: {summary.CurrentMargin.Round(4) * 100}%";
                    statusLabel.ForeColor = summary.CurrentMargin >= 0.4 ? Color.Green : Color.DarkRed;
                    statusLabel.Font = summary.CurrentMargin <= 0.32 ? new Font(statusLabel.Font.Name, statusLabel.Font.Size, FontStyle.Bold) : new Font(statusLabel.Font.Name, statusLabel.Font.Size, FontStyle.Regular);
                }
                else
                {
                    statusLabel.Text = response.Error;
                    statusLabel.ForeColor = Color.Red;
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCurrentMargin();
        }

        
    }
}
