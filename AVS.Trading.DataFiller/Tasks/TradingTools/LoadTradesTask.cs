using System;
using System.Collections.Generic;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Utils;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.DataFiller.Tasks.TradingTools
{
    /// <summary>
    /// loads user trades task
    /// </summary>
    public class LoadTradesTask : ITask
    {
        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Load User Trades",
                    Description = "Trading tools. Load user transactions.",
                    Group = "Trading tools",
                    Seconds = 30,//720
                    Enabled = true,
                    StopOnError = false,
                    Type = typeof(LoadTradesTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        private readonly ExchangeDirectory _exchangeDirectory;
        private readonly TradingToolsDataAdapter _tradingToolsDataAdapter;
        
        public LoadTradesTask(TradingToolsDataAdapter tradingToolsDataAdapter, ExchangeDirectory exchangeDirectory)
        {
            _tradingToolsDataAdapter = tradingToolsDataAdapter;
            _exchangeDirectory = exchangeDirectory;
        }

        public void Execute(TaskLogWriter log)
        {
            var exchanges = _exchangeDirectory.GetAllExchanges();
            foreach (var exchange in exchanges)
            {
                var client = _exchangeDirectory.GetClient(exchange);
                var pairs = client.Pairs.GetAllPairs();
                _tradingToolsDataAdapter.Client = client;

                log.Write($"{exchange} loading trades..\r\n");

                var noTradesPairs = new List<string>();
                foreach (var pair in pairs)
                {
                    try
                    {
                        int tradesCount = LoadTrades(pair);
                        if (tradesCount == 0)
                            noTradesPairs.Add(pair);
                        else 
                            log.Write($"{pair}  - #{tradesCount} trades");
                    }
                    catch (Exception ex)
                    {
                        log.WriteError($"{pair}  - loading trades FAILED", ex);
                    }
                }

                log.Write($"No trades for: {string.Join(", ", noTradesPairs)}\r\n");
            }
        }

        private int LoadTrades(string pair)
        {
            var last = _tradingToolsDataAdapter.GetLastTrade(pair, TradeCategory.Exchange | TradeCategory.MarginTrade);
            var trades = _tradingToolsDataAdapter.LoadLatestTrades(last, pair, DefaultScheduleTask.Seconds);
            return trades.Count;
        }


    }
}