using System;
using System.Windows.Forms;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Tool.Controls.Common
{
    public interface ITradeHistoryFiltersView: ITradeHistoryFilters
    {
        /// <summary>
        /// Returns Filters model (data transfer object) 
        /// in async operations we can't access the view properties 
        /// so the DTO model is required
        /// </summary>
        /// <returns></returns>
        ITradeHistoryFilters GetFilters();
    }

    public partial class TradeHistoryFiltersControl : UserControl, ITradeHistoryFiltersView
    {
        public double? AmountMin => double.TryParse(txtAmountMin.Text, out double res) ? (double?)res : null;
        public double? AmountMax => double.TryParse(txtAmountMax.Text, out double res) ? (double?)res : null;
        public double? ReduceKoef
        {
            get
            {
                if (!cbReduce.Checked)
                    return null;
                return double.TryParse(txtReduce.Text, out double res) ? (double?)res : null;
            }
        }
        public MinMaxTarget MinMaxTarget
        {
            get
            {
                if (rbPrice.Checked)
                    return MinMaxTarget.Price;
                return MinMaxTarget.Amount;
            }
        }
        
        public DateRange DateRange => _selectDateRangeControl1.Range;

        //public DateTime? From => _selectDateRangeControl1.From;
        //public DateTime? To => _selectDateRangeControl1.To;

        public string Market
        {
            get => selectMarketControl1.Market;
            set => selectMarketControl1.Market = value;
        }

        

        public TradeCategoryFilter Category
        {
            get
            {
                var res = TradeCategoryFilter.None;
                if (DisplayTradeCategoryBox)
                {
                    if (cbExchange.Checked)
                        res = res | TradeCategoryFilter.Exchange;
                    if (cbMargin.Checked)
                        res = res | TradeCategoryFilter.Margin;
                    if (cbSettlement.Checked)
                        res = res | TradeCategoryFilter.Settlement;
                }
                return res;
            }
        }

        public TradeTypeFilter Type
        {
            get
            {
                TradeTypeFilter res = TradeTypeFilter.None;
                if (cbBuys.Checked)
                    res = res | TradeTypeFilter.Buys;
                if (cbSells.Checked)
                    res = res | TradeTypeFilter.Sells;
                return res;
            }
        }

        public TradeHistoryFiltersControl()
        {
            InitializeComponent();
        }

        public ITradeHistoryFilters GetFilters()
        {
            var res = new TradeHistoryFilters()
            {
                Category = this.Category,
                Type = this.Type,
                AmountMin = AmountMin,
                AmountMax = AmountMax,
                DateRange = DateRange,
                Market = Market,
                MinMaxTarget = MinMaxTarget,
                ReduceKoef = ReduceKoef
            };
            return res;
        }

        public bool DisplayTradeCategoryBox
        {
            get => tradeCategoryGroupBox.Visible;
            set => tradeCategoryGroupBox.Visible = value;
        }

        private void cbReduce_CheckedChanged(object sender, EventArgs e)
        {
            txtReduce.Enabled = cbReduce.Checked;
        }
    }

    
}
