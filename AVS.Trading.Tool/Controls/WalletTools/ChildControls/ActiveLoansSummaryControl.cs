using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Models.Wallet;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.WalletTools;

namespace AVS.Trading.Tool.Controls.WalletTools.ChildControls
{
    public partial class ActiveLoansSummaryControl : BaseSummaryControl
    {
        public ActiveLoansSummaryControl()
        {
            InitializeComponent();
        }

        public string Currency { get; set; }

        public double Amount
        {
            set => lblAmount.Text = value.FormatNumber(Currency);
        }

        public double Fees
        {
            set => lblTotalFees.Text = value.FormatNumber(Currency);
        }

        public double AvgRate
        {
            set => lblAvgRate.Text = $"{value:P3}";
        }

        protected override void DataBound(object dataSource)
        {
            if (!(dataSource is ActiveLoansSummary summary))
            {
                Amount = 0;
                Fees = 0;
                AvgRate = 0;
                //this.Visible = false;
            }
            else
            {
                Amount = summary.Amount;
                Fees = summary.Fees;
                AvgRate = summary.AvgRate;
                this.Visible = true;
            }
        }
    }
}
