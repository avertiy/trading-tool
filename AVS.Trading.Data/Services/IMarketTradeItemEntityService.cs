using System;
using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Poloniex.Data.Domain.Market;

namespace AVS.Poloniex.Data.Services
{
    public interface IMarketTradeItemEntityService : IEntityServiceBase<MarketTradeItem>
    {
    }

    public class MarketTradeItemEntityService : EntityServiceBase<MarketTradeItem>, IMarketTradeItemEntityService
    {
        public MarketTradeItemEntityService(IRepository<MarketTradeItem> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }
    }
}