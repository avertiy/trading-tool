using System;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Utils;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Tool.Utils
{
    /// <summary>
    /// adds cell tooltip to price cell with scale +1%,+2%,+3% and -1%,-2%,-3% 
    /// </summary>
    public class PriceCellFormatter : IGridCellFormatter
    {
        private int? _tradeTypeColIndex;

        protected int GetTradeTypeColIndex(DataGridView grid)
        {
            if (!_tradeTypeColIndex.HasValue)
            {
                _tradeTypeColIndex = -1;
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (col.HeaderText == @"Type")
                    {
                        _tradeTypeColIndex = col.Index;
                        break;
                    }
                }
            }
            return _tradeTypeColIndex.Value;
        }

        public void FormatCell(DataGridView grid, DataGridViewCellFormattingEventArgs args)
        {
            var typeColumnIndex = GetTradeTypeColIndex(grid);

            if (grid.Columns[args.ColumnIndex].HeaderText == @"Price")
            {
                var priceCell = grid.Rows[args.RowIndex].Cells[args.ColumnIndex];
                var price = Convert.ToDouble(priceCell.Value);
                var isBuy = IsTradeTypeBuy(grid, args.RowIndex, typeColumnIndex);
                var tooltip = isBuy ? 
                    $"+1% - {(price * 1.01).FormatAsPrice()}; +2% - {(price * 1.02).FormatAsPrice()}; +3% - {(price * 1.03).FormatAsPrice()}" 
                    : $"-1% - {(price * 0.99).FormatAsPrice()}; -2% - {(price * 0.98).FormatAsPrice()}; -3% - {(price * 0.97).FormatAsPrice()}";
                priceCell.ToolTipText = tooltip;
            }
        }

        private bool IsTradeTypeBuy(DataGridView grid, int row, int column)
        {
            if (column == -1)
                return false;
            var typeCell = grid.Rows[row].Cells[column];
            return typeCell.Value.ToString() == "Buy";
        }
    }
}