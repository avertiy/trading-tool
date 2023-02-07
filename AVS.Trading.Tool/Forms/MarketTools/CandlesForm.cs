using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVS.Trading.Tool.Forms.MarketTools
{
    public partial class CandlesForm : Form
    {
        public CandlesForm()
        {
            InitializeComponent();
        }

        public void Initialize(string market)
        {
            //selectMarketControl1.Market = market;
            //_selectDateRangeControl1.From = DateTime.Today.AddDays(-180);
            //_selectDateRangeControl1.To = DateTime.Today.AddDays(-180);
        }
    }
}
