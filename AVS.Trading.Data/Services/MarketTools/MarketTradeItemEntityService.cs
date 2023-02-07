using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;

namespace AVS.Trading.Data.Services.MarketTools
{
    public interface IMarketTradeItemEntityService : IEntityServiceBase<MarketTradeItem>
    {
        MarketTradeItem GetLastTrade(string pair);
        MarketTradeItem GetFirstTrade(string pair);
        IQueryable<MarketTradeItem> GetTrades(string pair);
        int Count(string pair, DateTime start, DateTime end, double? threshold);
        IList<MarketTradeItem> Search(string pair, DateTime start, DateTime end, double? amountbase);
        
    }
    public class MarketTradeItemEntityService : EntityServiceBase<MarketTradeItem>,
        IMarketTradeItemEntityService
    {
        public MarketTradeItemEntityService(IRepository<MarketTradeItem> repository, 
            IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public MarketTradeItem GetLastTrade(string pair)
        {
            return Repository.Table.Where(t => t.Pair == pair).OrderByDescending(t => t.DateUtc).FirstOrDefault();
        }

        public MarketTradeItem GetFirstTrade(string pair)
        {
            return Repository.Table.Where(t => t.Pair == pair).OrderBy(t => t.DateUtc).FirstOrDefault();
        }

        public IQueryable<MarketTradeItem> GetTrades(string pair)
        {
            return Repository.Table.Where(t => t.Pair == pair);
        }

        public int Count(string pair, DateTime start, DateTime end, double? threshold)
        {
            var query = Repository.Table.Where(t => t.Pair == pair && t.DateUtc >= start && t.DateUtc <= end);
            if (threshold.HasValue)
                query = query.Where(t => t.AmountBase <= threshold);
            return query.Count();
        }


        public IList<MarketTradeItem> Search(string pair, DateTime start, DateTime end, double? threshold)
        {
            var query = Repository.Table.Where(t => t.Pair == pair && t.DateUtc >=start && t.DateUtc<=end);
            if (threshold.HasValue)
                query = query.Where(t => t.AmountBase <= threshold);
            return query.OrderByDescending(t => t.DateUtc).ToList();
        }
    }
}