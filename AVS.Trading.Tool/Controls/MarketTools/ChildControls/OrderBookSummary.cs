using System.Windows.Forms;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Tool.Controls.MarketTools.ChildControls
{
    public partial class OrderBookSummary : UserControl
    {
        public OrderBookSummary()
        {
            InitializeComponent();
        }

        public void Initialize(OrderBook book, CurrencyPair pair)
        {
            lblBuyCount.Text = book.BuyOrdersCount.ToString();
            lblSellCount.Text = book.SellOrdersCount.ToString();

            lblBuyTotal.Text = book.TotalBuys.FormatNumber(pair.BaseCurrency);
            lblSellTotal.Text = book.TotalSells.FormatNumber(pair.BaseCurrency);

            lblSupportPrice.Text = book.SupportingWall.Price.FormatAsPrice();
            lblResistancePrice.Text = book.ResistanceWall.Price.FormatAsPrice();
            
            lblSupportAmountBase.Text = book.SupportingWall.AmountBase.FormatNumber(pair.BaseCurrency);
            lblResistanceAmountBase.Text = book.ResistanceWall.AmountBase.FormatNumber(pair.BaseCurrency);

            lblSupportAmountQuote.Text = book.SupportingWall.AmountQuote.FormatNumber(pair.QuoteCurrency);
            lblResistanceAmountQuote.Text = book.ResistanceWall.AmountQuote.FormatNumber(pair.QuoteCurrency);

            lblSupportTotalVolume.Text = book.SupportingWall.Sum.FormatNumber(pair.BaseCurrency);
            lblResistanceTotalVolume.Text = book.ResistanceWall.Sum.FormatNumber(pair.BaseCurrency);
        }
    }
}
