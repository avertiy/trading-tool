using System;
using System.Linq;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.ExmoApi.Data.Services;
using AVS.ExmoApi.TradingTools;

namespace AVS.ExmoApi.Tasks
{
    /*
    public class ExmoLoadTradesSubtask : LoadTradesSubtaskBase
    {
        private readonly ExmoClient _client;
        private readonly ExmoTradeItemEntityService _entityService;
        private readonly ExmoTradingDataPreprocessor _dataPreprocessor;

        public ExmoLoadTradesSubtask(ExmoPairProvider pairProvider, ExmoTradingDataPreprocessor dataPreprocessor, ExmoTradeItemEntityService entityService, ExmoClient client) : base(pairProvider)
        {
            _dataPreprocessor = dataPreprocessor;
            _entityService = entityService;
            _client = client;
        }

        protected override int Execute(TaskLogWriter log, string pair, int timespan)
        {
            var allTrades = _client.Trading.GetAllTrades(0, 10000, pair);
            if (allTrades.Count == 0 || allTrades[pair].Count == 0)
            {
                return 0;
            }

            var lastTrade = _entityService.GetLastTrade(pair);
            if (lastTrade == null)
            {
                var trades = _dataPreprocessor.PreprocessTrades(allTrades);
                _entityService.BulkInsert(trades);
                return trades.Count;
            }
            else
            {
                var newTrades = allTrades[pair].Where(t => t.DateUtc > lastTrade.DateUtc);
                var trades = _dataPreprocessor.PreprocessTrades(newTrades, pair);
                if (trades.Count == 0)
                {
                    return 0;
                }

                var existingTrades = _entityService.Search(trades.Select(t => t.TradeId).ToArray())
                    .Select(t => t.TradeId);
                var tradesToBeInserted = trades.Where(t => !existingTrades.Contains(t.TradeId)).ToArray();
                if (tradesToBeInserted.Length == 0)
                {
                    return 0;
                }

                _entityService.BulkInsert(tradesToBeInserted);
                return tradesToBeInserted.Length;
            }
        }
    }*/
}