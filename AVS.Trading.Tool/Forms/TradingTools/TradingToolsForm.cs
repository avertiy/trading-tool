using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms;
using AVS.Trading.Tool;
using AVS.Trading.Framework.Infrastructure;

namespace AVS.Poloniex
{
    public partial class TradingToolsForm : Form, IFormView, IStatusText
    {
        public TradingToolsForm()
        {
            InitializeComponent();
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

        public string FormTitle
        {
            get => this.Text;
            set => this.Text = value;
        }

        private void toolStripMenuItemExmo_Click(object sender, EventArgs e)
        {
            SwitchExchange(Constants.Exchanges.Exmo, ((ToolStripMenuItem)sender).Image);
        }

        private void toolStripMenuItemPoloniex_Click(object sender, EventArgs e)
        {
            SwitchExchange(Constants.Exchanges.Poloniex, ((ToolStripMenuItem)sender).Image);
        }

        private void SwitchExchange(string exchange, Image image)
        {
            var ctx = EngineContext.Current.Resolve<IWorkContext>();
            ctx.Exchange = exchange;
            toolStripSwitchExchangeBtn.ToolTipText = $"Current exchange is {exchange.ToString()}";
            toolStripSwitchExchangeBtn.Image = image;
        }
    }
}
