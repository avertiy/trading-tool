using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Core.Enums;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Controls.Extensions
{
    public static class FiltersExtensions
    {
        public static IList<OpenOrder> ApplyFilters(this ITradeHistoryFilters filters, IList<OpenOrder> orders)
        {
            var query = orders.AsQueryable();

            if (!filters.Type.HasFlag(TradeTypeFilter.Buys))
            {
                query = query.Where(t => t.Type != OrderType.Buy);
            }

            if (!filters.Type.HasFlag(TradeTypeFilter.Sells))
            {
                query = query.Where(t => t.Type != OrderType.Sell);
            }

            if (!filters.Category.HasFlag(TradeCategoryFilter.Exchange))
            {
                query = query.Where(t => t.Account != TradingAccount.Exchange);
            }

            if (!filters.Category.HasFlag(TradeCategoryFilter.Margin))
            {
                query = query.Where(t => t.Account != TradingAccount.Margin);
            }

            orders = query.ToList();
            return orders;
        }

        public static IList<TradeItem> ApplyFilters(this ITradeHistoryFilters filters, IList<TradeItem> trades)
        {
            var query = trades.AsQueryable();

            if (!filters.Type.HasFlag(TradeTypeFilter.Buys))
            {
                query = query.Where(t => t.Type != TradeType.Buy);
            }

            if (!filters.Type.HasFlag(TradeTypeFilter.Sells))
            {
                query = query.Where(t => t.Type != TradeType.Sell);
            }

            if (!filters.Category.HasFlag(TradeCategoryFilter.Exchange))
            {
                query = query.Where(t => t.Category != TradeCategory.Exchange);
            }

            if (!filters.Category.HasFlag(TradeCategoryFilter.Margin))
            {
                query = query.Where(t => t.Category != TradeCategory.MarginTrade);
            }

            if (!filters.Category.HasFlag(TradeCategoryFilter.Settlement))
            {
                query = query.Where(t => t.Category != TradeCategory.Settlement);
            }

            if (!filters.Category.HasFlag(TradeCategoryFilter.LendingFees))
            {
                query = query.Where(t => t.Category != TradeCategory.LendingFees);
            }

            if (filters.AmountMin.HasValue && filters.AmountMin.Value > 0)
            {
                if(filters.MinMaxTarget == MinMaxTarget.Amount)
                    query = query.Where(t => t.AmountQuote >= filters.AmountMin.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Price)
                    query = query.Where(t => t.Price >= filters.AmountMin.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Total)
                    query = query.Where(t => t.AmountBase >= filters.AmountMin.Value);
            }

            if (filters.AmountMax.HasValue && filters.AmountMax.Value > 0)
            {
                if (filters.MinMaxTarget == MinMaxTarget.Amount)
                    query = query.Where(t => t.AmountQuote <= filters.AmountMax.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Price)
                    query = query.Where(t => t.Price <= filters.AmountMax.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Total)
                    query = query.Where(t => t.AmountBase <= filters.AmountMax.Value);
            }

            trades = query.ToList();
            return trades;
        }

        public static IList<MarketTradeItem> ApplyFilters(this ITradeHistoryFilters filters, IList<MarketTradeItem> trades)
        {
            var query = trades.AsQueryable();

            if (!filters.Type.HasFlag(TradeTypeFilter.Buys))
            {
                query = query.Where(t => t.Type != TradeType.Buy);
            }

            if (!filters.Type.HasFlag(TradeTypeFilter.Sells))
            {
                query = query.Where(t => t.Type != TradeType.Sell);
            }
            
            if (filters.AmountMin.HasValue && filters.AmountMin.Value > 0)
            {
                if (filters.MinMaxTarget == MinMaxTarget.Amount)
                    query = query.Where(t => t.AmountQuote >= filters.AmountMin.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Price)
                    query = query.Where(t => t.Price >= filters.AmountMin.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Total)
                    query = query.Where(t => t.AmountBase >= filters.AmountMin.Value);
            }

            if (filters.AmountMax.HasValue && filters.AmountMax.Value > 0)
            {
                if (filters.MinMaxTarget == MinMaxTarget.Amount)
                    query = query.Where(t => t.AmountQuote <= filters.AmountMax.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Price)
                    query = query.Where(t => t.Price <= filters.AmountMax.Value);
                if (filters.MinMaxTarget == MinMaxTarget.Total)
                    query = query.Where(t => t.AmountBase <= filters.AmountMax.Value);
            }

            trades = query.ToList();
            return trades;
        }
    }
}