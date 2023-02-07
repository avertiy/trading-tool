using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Controls;
using AVS.Trading.Tool.Controls.TradingTools.Controllers;
using AVS.Trading.Framework.Services.WalletTools;

namespace AVS.Trading.Tool.Forms.WalletTools
{
    public partial class MyLoansForm : FormEx
    {
        public MyLoansForm()
        {
            InitializeComponent();
        }

        public string Currency
        {
            get => this.myLoansControl1.Currency;
            set => this.myLoansControl1.Currency = value;
        }
    }

   
}
