using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core.Domain;

namespace AVS.Trading.Tool.Forms.Dialogs
{
    public partial class SelectApiKeyDialog : Form
    {
        private IWorkContext _workContext;
        public SelectApiKeyDialog()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            var config = EngineContext.Current.Resolve<TradingAppConfig>();
            _workContext = EngineContext.Current.Resolve<IWorkContext>();

            var exchange = config.Exchanges.GetExchange(_workContext.Exchange);
            if(exchange == null)
                return;

            comboBox.DataSource = exchange.Keys.Select(k=>k.Name).ToList();
            
            tbExchange.Text = exchange.Name;
            comboBox.SelectedItem = _workContext.Client.Account.Name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (comboBox.SelectedItem == null)
            {
                return;
            }

            var config = EngineContext.Current.Resolve<TradingAppConfig>();
            ApiKey key = config.Exchanges.GetApiKey(_workContext.Exchange, comboBox.SelectedItem.ToString());
            _workContext.SwitchAccount(key);

            tbPublicKey.Text = key.PublicKey;
            tbPrivateKey.Text = key.PrivateKey;
        }
    }
}
