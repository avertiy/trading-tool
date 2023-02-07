using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.Utils;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Tool.Controls.Extensions
{
    public static class GridHighlighterExtensions
    {
        public static GridHightlighter WithTradeTypeColorScheme(this GridHightlighter hightlighter, DataGridViewColumn column)
        {
            var scheme = new GridColorScheme()
            {
                ColumnIndex = column.Index,
                Scope = ColorSchemeScope.CellForeColor,
                Values = new[] { TradeType.Buy, TradeType.Sell },
                Colors = new[] { Color.DarkGreen, Color.DarkRed }
            };
            hightlighter.AddScheme(scheme.WithEnumEquityCondition());
            return hightlighter;
        }

        public static GridHightlighter WithOrderTypeColorScheme(this GridHightlighter hightlighter, DataGridViewColumn column)
        {
            var scheme = new GridColorScheme()
            {
                ColumnIndex = column.Index,
                Scope = ColorSchemeScope.CellForeColor,
                Values = new[] { OrderType.Buy, OrderType.Sell },
                Colors = new[] { Color.DarkGreen, Color.DarkRed }
            };
            hightlighter.AddScheme(scheme.WithEnumEquityCondition());
            return hightlighter;
        }

        public static GridHightlighter WithTradeCategoryColorScheme(this GridHightlighter hightlighter, DataGridViewColumn column)
        {
            var scheme = new GridColorScheme()
            {
                ColumnIndex = column.Index,
                Scope = ColorSchemeScope.RowBackColor,
                Colors = new[]
                {
                    System.Drawing.SystemColors.Window,
                    Color.LightYellow,
                    //Color.Gold,
                    Color.MintCream,
                    Color.LightGreen
                },
                Values = new[] { TradeCategory.Settlement, TradeCategory.Exchange, TradeCategory.MarginTrade }
            };
            hightlighter.AddScheme(scheme.WithEnumEquityCondition());
            return hightlighter;
        }

        public static GridHightlighter WithAccountTypeColorScheme(this GridHightlighter hightlighter, DataGridViewColumn column)
        {
            var scheme = new GridColorScheme()
            {
                ColumnIndex = column.Index,
                Scope = ColorSchemeScope.RowBackColor,
                Colors = new[]
                {
                    Color.LightYellow,
                    Color.MintCream,
                },
                Values = new[] { AccountType.Exchange, AccountType.Margin }
            };
            hightlighter.AddScheme(scheme.WithEnumEquityCondition());
            return hightlighter;
        }


        public static void SetupScaleColorScheme(this GridHightlighter hightlighter, DataGridViewColumn column, CurrencyPair pair)
        {
            hightlighter.Schemes.Clear();
            double[] values = new[] { 1.0, 2.0, 5.0, 10, 15, 25, 50, 100 };
            if (pair.BaseCurrency.Either("USD", "USDT","USDC", "EUR"))
                values = new[] { 1000.0, 2000, 5000, 10000, 15000, 25000, 50000, 100000 };
            if (pair.BaseCurrency == "UAH")
                values = new[] { 10000.0, 20000, 50000, 100000, 200000, 250000, 500000 };
            if (pair.BaseCurrency.Either("ETH","XMR"))
                values = new[] { 10.0, 20, 50, 100, 150, 250, 500, 1000 };
            hightlighter.WithScaleColorScheme(column, values);
        }
    }
}