using System.Collections.Generic;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Grid;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Framework.Extensions;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Tool.Utils
{
    public class MyGridSelectionHelper: GridSelectionHelper{
        
        public MyGridSelectionHelper()
        {
            Columns = new Dictionary<string, string>
            {
                {"Amount", "Amount"},
                {"Type", "Type"},
                {"Total", "Total"},
                {"Market", "Market"}
            };
        }
        
        public string GetTradeSummary(DataGridView grid, string market = null)
        {
            if (grid.SelectedCells.Count == 0)
                return "";

            DataGridViewColumn column = grid.SelectedCells[0].OwningColumn;
            
            if (column.Name.Either(Columns["Amount"], Columns["Total"], Columns["Price"]))
            {
                var summary = grid.SelectedCells.ToTradeSummary();
                if(summary == null)//return cells sum
                    return GetSelectedCellsSum(grid);
                if (column.Name == Columns["Price"])
                    return summary.GetAvgPriceInfo();
                return summary.GetInfo(column.Name == Columns["Amount"]);
                
            }

            if (market == null && grid.Columns.Contains(Columns["Market"]))
            {
                market = (string)grid.SelectedCells[0].OwningRow.Cells[Columns["Market"]].Value;
            }

            double bought = 0.0;
            double sold = 0.0;
            foreach (DataGridViewCell cell in grid.SelectedCells)
            {
                var row = grid.Rows[cell.RowIndex];
                
                var cellTradeType = row.Cells[Columns["Type"]].Value;

                if (cellTradeType?.ToString() == TradeType.Buy.ToString())
                {
                    bought += (double)row.Cells[Columns["Amount"]].Value;
                }
                else if (cellTradeType?.ToString() == TradeType.Sell.ToString())
                {
                    sold += (double)row.Cells[Columns["Amount"]].Value;
                }
            }
            var pair = CurrencyPair.Parse(market);
            var total = bought - sold;
            var boughtStr = bought > 0 ? "Buys: " + bought.FormatNumber(pair.QuoteCurrency) : "";
            var soldStr = sold > 0 ? "Sells: " + sold.FormatNumber(pair.QuoteCurrency) : "";
            return $"{boughtStr}   {soldStr}   Total: {total.FormatNumber(pair.QuoteCurrency)}";
        }

        protected override string SelectedCellsSumFormat(int rows, int rowCount, double sum)
        {
            return $"Rows {rows} from {rowCount}   Sum: {sum.FormatNumber()}";
        }
    }
}