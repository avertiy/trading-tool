using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class SelectMarketPeriodControl : UserControl
    {
        readonly Type _type = typeof(MarketPeriod);


        public SelectMarketPeriodControl()
        {
            InitializeComponent();

            var periods = Enum.GetNames(_type);
            combo.DataSource = periods;
            combo.SelectedIndex = 0;
        }

        public MarketPeriod SelectedPeriod
        {
            get
            {
                if(combo.SelectedItem == null)
                    return MarketPeriod.M5;
                return (MarketPeriod)Enum.Parse(_type, combo.SelectedItem.ToString());
            }
            set => combo.SelectedItem = Enum.GetName(_type,value);
        }
    }
}
