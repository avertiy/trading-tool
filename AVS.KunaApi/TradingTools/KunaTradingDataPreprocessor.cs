using System.Collections.Generic;
using System.Linq;
using AVS.KunaApi.TradingTools.Models;
using AVS.Trading.Core.Interfaces.TradingTools;

namespace AVS.KunaApi.TradingTools
{
    public class KunaTradingDataPreprocessor
    {
        /*
        public List<KunaTradeItem> PreprocessTrades(IDictionary<string, IList<ITrade>> alltrades)
        {
            var items = new List<KunaTradeItem>();
            foreach (var kp in alltrades)
            {
                items.AddRange(PreprocessTrades(kp.Value, kp.Key));
            }
            return items;
        }

        public List<KunaTradeItem> PreprocessTrades(IEnumerable<ITrade> trades, string pair)
        {
            return trades.Select(trade => new KunaTradeItem
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

        public KunaWalletTransaction[] PreprocessWalletHistory(WalletHistory data)
        {
            if (data.Success && data.Items.Count > 0)
            {
                return data.Items.Select(i => new KunaWalletTransaction()
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
            return new KunaWalletTransaction[] { };
        }
        */
    }
}
