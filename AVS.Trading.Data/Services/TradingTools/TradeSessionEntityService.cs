using System.Linq;
using AVS.CoreLib.Data;
using AVS.CoreLib.Data.Events;
using AVS.CoreLib.Data.Services;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Data.Services.TradingTools
{
    public interface ITradeSessionEntityService : IEntityServiceBase<TradeSession>
    {
        TradeSession GetOpenTradeSession(string exchange, string pair);
        TradeSession GetTradeSession(string exchange, string pair, string algorithm);
    }

    public class TradeSessionEntityService: EntityServiceBase<TradeSession>, ITradeSessionEntityService
    {
        public TradeSessionEntityService(IRepository<TradeSession> repository, IEventPublisher eventPublisher) : base(repository, eventPublisher)
        {
        }

        public TradeSession GetOpenTradeSession(string exchange, string pair)
        {
            return Repository.Table.FirstOrDefault(t => t.Exchange == exchange && t.Pair == pair && t.Closed == null);
        }

        public TradeSession GetTradeSession(string exchange, string pair, string algorithm)
        {
            return Repository.Table.FirstOrDefault(t => t.Exchange == exchange && t.Pair == pair && t.Algorithm == algorithm);
        }
    }
}
