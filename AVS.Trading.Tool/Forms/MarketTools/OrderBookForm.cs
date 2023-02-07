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
    public partial class OrderBookForm : Form, IFormView
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FormTitle
        {
            get => this.Text;
            set => this.Text = value;
        }

        public OrderBookForm()
        {
            InitializeComponent();
            this.FormTitle = "Order book";
        }

        public string Market
        {
            get => this.marketOrderBook1.Market;
            set
            {
                this.marketOrderBook1.Market = value;
                FormTitle = value + " order book";
            }
        }

        public void LoadData()
        {
            this.marketOrderBook1.LoadData();
        }
    }
}
