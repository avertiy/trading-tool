using System.ComponentModel;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Grid;

namespace AVS.Trading.Tool.Controls.Common
{
    public class BaseSummaryControl : UserControl, ISummaryView
    {
        private object _dataSource;
        [Browsable(false)]
        public object DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                DataBound(value);
            }
        }

        protected virtual void DataBound(object dataSource)
        {

        }
    }


}