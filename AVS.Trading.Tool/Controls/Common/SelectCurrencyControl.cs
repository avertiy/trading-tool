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
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class SelectCurrencyControl : UserControlEx
    {
        protected override void Initialize()
        {
            InitializeComponent();
            comboMarket.DataSource = DefaultCurrencies;
            comboMarket.SelectedIndex = 0;
        }
        
        public object DataSource
        {
            get => comboMarket.DataSource;
            set => comboMarket.DataSource = value;
        }

        public string Currency
        {
            get => comboMarket.SelectedItem?.ToString() ?? comboMarket.SelectedText;
            set => comboMarket.SelectedItem = value;
        }

        private void SetupCurrencies()
        {
            var ctx = EngineContext.Current.Resolve<IWorkContext>();
            try
            {
                comboMarket.DataSource = ctx.Client.Pairs.GetCoinsFor(AccountType.Lending);
            }
            catch (Exception)
            {
                comboMarket.DataSource = DefaultCurrencies;
            }
        }


        private string[] DefaultCurrencies => new[]
        {
            "BTC",
            "BTS",
            "DASH",
            "DOGE",
            "ETH",
            "FCT",
            "LTC",
            "MAID",
            "STR",
            "XMR",
            "XRP"
        };

        private void comboMarket_MouseEnter(object sender, EventArgs e)
        {
            SetupCurrencies();
        }

        private void comboMarket_Click(object sender, EventArgs e)
        {
            SetupCurrencies();
        }
    }
}
