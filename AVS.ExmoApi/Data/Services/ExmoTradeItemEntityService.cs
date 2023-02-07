using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.ExmoApi.Data.Domain;

namespace AVS.ExmoApi.Data.Services
{
    public class ExmoTradeItemEntityService : EntityServiceBase<ExmoTradeItem>
    {
        public ExmoTradeItemEntityService(IRepository<ExmoTradeItem> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public ExmoTradeItem GetLastTrade(string pair)
        {
            var items = Repository.Table.Where(t => t.Pair == pair).OrderByDescending(t => t.DateUtc).ToList();
            return items.FirstOrDefault();
        }

        public IList<ExmoTradeItem> Search(string pair, DateTime from, DateTime to, string[] tradeIds = null)
        {
            var query = Repository.Table.Where(t => t.Pair == pair &&
                                                    t.DateUtc >= from && t.DateUtc <= to);
            if (tradeIds != null && tradeIds.Length > 0)
                query = query.Where(t => tradeIds.Contains(t.TradeId));

            return query.OrderByDescending(t => t.DateUtc).ToList();
        }

        public IList<ExmoTradeItem> Search(string[] tradeIds)
        {
            var query = Repository.Table.Where(t => tradeIds.Contains(t.TradeId));
            return query.OrderByDescending(t => t.DateUtc).ToList();
        }

       
    }
}