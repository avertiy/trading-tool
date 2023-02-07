using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.Trading.Tool.Utils;

namespace AVS.Trading.Tool.Controls.Common
{
    public partial class NumControl : UserControl
    {
        public NumControl()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Value
        {
            get => Helper.ParseDouble(textbox.Text);
            set
            {
                textbox.Text = value.ToString(Format, CultureInfo.InvariantCulture);
            }
        }

        public string Format { get; set; } = "0.00000000";

        public string Currency
        {
            get => lblCurrency.Text;
            set => lblCurrency.Text = value;
        }

        private void textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public event EventHandler<double> ValueChanged;

        private void textbox_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textbox.Text))
                return;

            var handler = ValueChanged;
            handler?.Invoke(this, Value);
        }
    }
}


