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
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Services;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class LimitOrderControl : UserControl
    {
        #region prop-s

        public TradingAccount Type
        {
            get => numLoanRate.Visible ? TradingAccount.Margin : TradingAccount.Exchange;
            set
            {
                numLoanRate.Visible = value == TradingAccount.Margin;
                lblLoanRate.Visible = numLoanRate.Visible;
            }
        }

        public OrderType OrderType { get; set; }

        public string Header
        {
            get => groupBox1.Text;
            set => groupBox1.Text = value;
        }

        public string ButtonText
        {
            get => button.Text;
            set => button.Text = value;
        }

        public double Price
        {
            get => numPrice.Value;
            set => numPrice.Value = value;
        }

        public double Amount
        {
            get => numAmount.Value;
            set => numAmount.Value = value;
        }

        public double LoanRate
        {
            get => numLoanRate.Value/100;
            set => numLoanRate.Value = value;
        }

        public double Total
        {
            get => numTotal.Value;
            set => numTotal.Value = value;
        }
        #endregion

        public LimitOrderControl()
        {
            InitializeComponent();
        }
        
        public void Initialize(CurrencyPair pair)
        {
            numPrice.Currency = pair.BaseCurrency;
            numTotal.Currency = pair.BaseCurrency;
            numAmount.Currency = pair.QuoteCurrency;
            Header = $"{OrderType.ToString().ToUpper()} {pair.QuoteCurrency}";

        }

        public event EventHandler ButtonClick;

        

        private void button_Click(object sender, EventArgs e)
        {
            var handler = ButtonClick;
            handler?.Invoke(this, e);
        }

        private void numAmount_ValueChanged(object sender, double e)
        {
            Total = Amount * Price;
        }

        private void numPrice_ValueChanged(object sender, double e)
        {
            Total = Amount * Price;
        }
    }
}
