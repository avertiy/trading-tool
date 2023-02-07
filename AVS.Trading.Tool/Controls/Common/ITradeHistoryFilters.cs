using System;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Tool.Controls.Common
{
    public interface ITradeHistoryFilters
    {
        TradeCategoryFilter Category { get; }
        TradeTypeFilter Type { get; }
        double? AmountMin { get; }
        double? AmountMax { get; }
        MinMaxTarget MinMaxTarget { get; }
        DateRange DateRange { get; }
        string Market { get; }
        double? ReduceKoef { get; }
    }

    public class TradeHistoryFilters : ITradeHistoryFilters
    {
        public TradeCategoryFilter Category { get; set; }
        public TradeTypeFilter Type { get; set; }
        public double? AmountMin { get; set; }
        public double? AmountMax { get; set; }
        public MinMaxTarget MinMaxTarget { get; set; }
        public DateRange DateRange { get; set; }
        public string Market { get; set; }
        public double? ReduceKoef { get; set; }
    }

    [Flags]
    public enum TradeCategoryFilter
    {
        None = 0,
        Exchange = 1,
        Margin = 2,
        Settlement = 4,
        LendingFees = 8,
    }

    [Flags]
    public enum TradeTypeFilter
    {
        None = 0,
        Buys = 1,
        Sells = 2,
    }

    public enum MinMaxTarget
    {
        Amount = 0,
        Price = 1,
        Total = 2
    }

    public enum ViewModeEnum
    {
        Normal =0,
        Mininal,
        Detailed
    }
}