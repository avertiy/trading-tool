using AVS.CoreLib.Data.Services;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;

namespace AVS.Trading.Data.Services.MarketTools
{
    public interface IMarketTradeHistoryEntityService : IEntityServiceBase<MarketTradeHistory, MarketTradeItem>
    {
        MarketTradeHistory GetLastTrade(string pair);
    }
    
    /*public class MarketTradeHistoryEntityService : EntityServiceBase<MarketTradeHistory, MarketTradeItem>, IMarketTradeHistoryEntityService
    {
        public MarketTradeHistoryEntityService(IRepository<MarketTradeHistory> repository, IEventPublisher eventPublisher, IRepository<MarketTradeItem> itemRepository) : base(repository, eventPublisher, itemRepository)
        {
        }

        public MarketTradeHistory GetLastTrade(string pair)
        {
            return Repository.Table.Where(t=>t.Market == pair).OrderByDescending(t => t.From).FirstOrDefault();
        }
        public MarketTradeHistory GetTradeByDate(string pair, DateTime date)
        {
            return Repository.Table.Where(t => t.Market == pair && t.From >= date && t.To <= date).OrderByDescending(t => t.From).FirstOrDefault();
        }
    }*/


    
}