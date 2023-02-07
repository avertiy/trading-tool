using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;

namespace AVS.Trading.Tool.Forms.TradingTools
{
    public partial class MyOrdersForm : MyBaseForm
    {
        private IWorkContext _workContext;
        protected string Market;
        public MyOrdersForm()
        {
            InitializeComponent();
        }

        public void Initialize(string market)
        {
            if (!string.IsNullOrEmpty(market))
                marketTickerControl1.Market = market;
            
            postOrdersControl1.Initialize(CurrencyPair.Parse(marketTickerControl1.Market));
        }

        private void marketTickerControl1_MarketChanged(object sender, Trading.Data.Domain.MarketTools.MarketData e)
        {
            postOrdersControl1.Setup(e);
        }
    }
}
