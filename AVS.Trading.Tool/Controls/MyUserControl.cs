using System;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.Controls;
using AVS.Poloniex;

namespace AVS.Trading.Tool.Controls
{
    public class MyUserControl : UserControlEx, IStatusText
    {
        protected ToolStripStatusLabel StatusLabel;

        /// <summary>
        /// requires StatusLabel to be initialized
        /// </summary>
        public string StatusText
        {
            get => StatusLabel?.Text;
            set
            {
                if (StatusLabel != null) { 
                    StatusLabel.Text = value;
                    StatusLabel.Visible = !string.IsNullOrWhiteSpace(value);
                }
            }
        }
    }
}