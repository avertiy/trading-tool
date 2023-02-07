using System;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Framework.Infrastructure;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class SelectMarketControl : UserControlEx
    {
        public object DataSource
        {
            get => comboMarket.DataSource;
            set => comboMarket.DataSource = value;
        }

        public string Market
        {
            get
            {
                string res = comboMarket.SelectedItem?.ToString() ?? comboMarket.SelectedText;
                if (string.IsNullOrEmpty(res))
                    res = comboMarket.Text;
                return res;
            }
            set => comboMarket.SelectedItem = value;
        }

        protected override void Initialize()
        {
            InitializeComponent();
        }
        
        private void comboMarket_MouseEnter(object sender, EventArgs e)
        {
            SetupMarkets();
        }

        private void comboMarket_Click(object sender, EventArgs e)
        {
            SetupMarkets();
        }

        private string _exchange;

        private void SetupMarkets()
        {
            var ctx = EngineContext.Current.Resolve<IWorkContext>();
            if (ctx.Exchange == _exchange)
                return;
            _exchange = ctx.Exchange;
            comboMarket.DataSource = ctx.Client.Pairs.GetAllPairs();
        }
    }
}
