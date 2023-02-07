using System.Collections.Generic;
using System.Linq;
using AVS.ExmoApi.Data.Domain;
using AVS.ExmoApi.TradingTools.Models;
using AVS.Trading.Core.Interfaces.TradingTools;

namespace AVS.ExmoApi.TradingTools
{
    public class ExmoTradingDataPreprocessor
    {
        public List<ExmoTradeItem> PreprocessTrades(IDictionary<string, IList<ITrade>> alltrades)
        {
            var items = new List<ExmoTradeItem>();
            foreach (var kp in alltrades)
            {
                items.AddRange(PreprocessTrades(kp.Value, kp.Key));
            }
            return items;
        }

        public List<ExmoTradeItem> PreprocessTrades(IEnumerable<ITrade> trades, string pair)
        {
            return trades.Select(trade => new ExmoTradeItem
                {
                    TradeId = trade.IdTrade,
                    OrderId = trade.OrderNumber,
                    Type = trade.Type,
                    AmountBase = trade.AmountBase,
                    AmountQuote = trade.AmountQuote,
                    Price = trade.Price,
                    DateUtc = trade.DateUtc,
                    Pair = pair
            }).ToList();
        }

        public ExmoWalletTransaction[] PreprocessWalletHistory(WalletHistory data)
        {
            if (data.Success && data.Items.Count > 0)
            {
                return data.Items.Select(i => new ExmoWalletTransaction()
                {
                    DateUtc = i.DateUtc,
                    Type = i.Type,
                    Currency = i.Currency,
                    Status = i.Status,
                    Provider = i.Provider,
                    Account = i.Account,
                    Amount = i.Amount
                }).ToArray();

            }
            return new ExmoWalletTransaction[] { };
        }
    }
}
