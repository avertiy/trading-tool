using System;
using AVS.Trading.Core;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Data.Services.TradingTools;
using AVS.Trading.Pipeline.Models;

namespace AVS.Trading.Engine.Emulator.Services
{
    public interface ITradeSessionService
    {
        TradeSession GetOpenSession(string exchange, CurrencyPair pair);
        TradeSession GetSession(string exchange, CurrencyPair pair, string algorithm);
        void SaveSession(TradeSession session);
        TradeSession OpenSession(string exchange, CurrencyPair pair, string algorithm);
    }

    public class TradeSessionService : ITradeSessionService
    {
        private readonly ITradeSessionEntityService _entityService;

        public TradeSessionService(ITradeSessionEntityService entityService)
        {
            _entityService = entityService;
        }

        public TradeSession GetOpenSession(string exchange, CurrencyPair pair)
        {
            return _entityService.GetOpenTradeSession(exchange, pair.ToString());
        }

        public TradeSession GetSession(string exchange, CurrencyPair pair, string algorithm)
        {
            return _entityService.GetTradeSession(exchange, pair.ToString(), algorithm);
        }

        public TradeSession OpenSession(string exchange, CurrencyPair pair, string algorithm)
        {
            var tradeSession = new TradeSession
            {
                Exchange = exchange,
                Pair = pair.ToString(),
                Algorithm = algorithm,
                Opened = DateTime.UtcNow,
            };

            return tradeSession;
        }

        public void SaveSession(TradeSession session)
        {
            var entity = _entityService.GetOpenTradeSession(session.Exchange, session.Pair);
            if (entity != null)
            {
                if(entity.Id != session.Id)
                    throw new ArgumentException($"There is another open trade session at {session.Exchange} on {session.Pair} with different id");
                _entityService.Update(session);
            }
            else
            {
                _entityService.Insert(session);
            }
        }
    }
}