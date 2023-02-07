using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.WinForms;

namespace AVS.Trading.Tool.Forms.MarketTools
{
    public partial class TradeHistoryForm : MyBaseForm, IFormView
    {
        public TradeHistoryForm()
        {
            InitializeComponent();
        }

        public string Market
        {
            get => this.marketTradeHistoryControl1.Market;
            set
            {
                this.marketTradeHistoryControl1.Market = value;
                FormTitle = "Trade history "+value;
            }
        }

        public void LoadData()
        {
            this.marketTradeHistoryControl1.LoadData();
        }
    }
}
