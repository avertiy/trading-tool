using System.Collections.Generic;
using System.Windows.Forms;
using AVS.Trading.Tool.Models;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Tool.Controls.Extensions
{
    public static class DataGridViewExtensions
    {
        public static List<IMarketTradeItem> GetTradeItems(this DataGridViewSelectedCellCollection selectedCells)
        {
            if (selectedCells.Count == 0)
                return null;

            if (!(selectedCells[0].OwningRow.DataBoundItem is IMarketTradeItem))
                return null;

            var items = new List<IMarketTradeItem>();

            var column = selectedCells[0].OwningColumn;

            foreach (DataGridViewCell cell in selectedCells)
            {
                //ignore other selected columns
                if (cell.ColumnIndex != column.Index)
                    continue;
                var tradeItem = (IMarketTradeItem)cell.OwningRow.DataBoundItem;
                items.Add(tradeItem);
            }

            return items;
        }

        public static TradeSummary ToTradeSummary(this DataGridViewSelectedCellCollection selectedCells)
        {
            var summary = new TradeSummary();
            summary.Initialize(selectedCells.GetTradeItems());
            return summary;
        }


    }
}