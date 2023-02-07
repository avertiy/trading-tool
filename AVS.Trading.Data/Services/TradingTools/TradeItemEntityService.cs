using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Data.Services.TradingTools
{
    public interface ITradeItemEntityService : IEntityServiceBase<TradeItem>
    {
        TradeItem GetLastTrade(string exchange, string pair, TradeCategory category);
        IList<TradeItem> Search(string pair, DateTime from, DateTime to, string[] tradeIds = null);
        IList<TradeItem> Search(string[] tradeIds);
        void ImportTrades(IList<TradeItem> tradeItems);
    }

    public class TradeItemEntityService : EntityServiceBase<TradeItem>, ITradeItemEntityService
    {
        public TradeItemEntityService(IRepository<TradeItem> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public TradeItem GetLastTrade(string exchange, string pair, TradeCategory category)
        {
            var items = Repository.Table.Where(t => t.Exchange == exchange && t.Pair == pair).OrderByDescending(t => t.DateUtc).ToList();
            return items.FirstOrDefault(t=>category.CheckFlag(t.Category));
        }

        public IList<TradeItem> Search(string pair, DateTime from, DateTime to, string[] tradeIds = null)
        {
            var query = Repository.Table.Where(t => t.Pair == pair && 
                                               t.DateUtc >= from &&t.DateUtc <= to);
            if(tradeIds != null && tradeIds.Length > 0)    
              query = query.Where(t=> tradeIds.Contains(t.TradeId));

            return query.OrderByDescending(t => t.DateUtc).ToList();
        }

        public IList<TradeItem> Search(string[] tradeIds)
        {
            var query = Repository.Table.Where(t => tradeIds.Contains(t.TradeId));
            return query.OrderByDescending(t => t.DateUtc).ToList();
        }

        public void ImportTrades(IList<TradeItem> tradeItems)
        {
            if (tradeItems.Count <= 500)
            {
                foreach (var tradeItem in tradeItems)
                {
                    if (string.IsNullOrEmpty(tradeItem.OrderId))
                        throw new ArgumentException("OrderId is expected");
                    if (string.IsNullOrEmpty(tradeItem.TradeId))
                        throw new ArgumentException("TradeId is expected");

                    var item = FirstOrDefault(t => t.TradeId == tradeItem.TradeId);

                    if (item == null)
                    {
                        Insert(tradeItem);
                    }
                    else
                    {
                        item.OrderId = tradeItem.OrderId;
                        item.Pair = tradeItem.Pair;
                        item.Category = tradeItem.Category;
                        item.Fee = tradeItem.Fee;
                        item.TotalFee = tradeItem.TotalFee;
                        item.AmountBase = tradeItem.AmountBase;
                        item.AmountQuote = tradeItem.AmountQuote;
                        item.Price = tradeItem.Price;
                        item.DateUtc = tradeItem.DateUtc;
                        item.Type = tradeItem.Type;
                        Update(item);
                    }
                }
            }
            else
            {
                //if many items let's do bulk insert, but before filter out existing items
                var tradeIds = tradeItems.Select(t => t.TradeId).ToArray();
                var existingItems = Search(tradeIds);
                var newTradeItems = tradeItems.Where(t => existingItems.All(e => e.TradeId != t.TradeId));
                BulkInsert(newTradeItems);
            }
        }
    }
}