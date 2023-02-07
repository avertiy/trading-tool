using System;
using System.Collections.Generic;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.Extensions;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Framework.Extensions;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Data.Services.MarketTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.DataFiller.Tasks.MarketTools
{
    /// <summary>
    /// Represents a task for loading market trade history
    /// </summary>
    public partial class LoadTradeHystoryTask : TaskBase
    {
        private readonly IMarketToolsService _marketToolsService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;
        private readonly IMarketTradeItemEntityService _tradeItemEntityService;
        private readonly IImportDataService _importDataService;

        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Load Trade History",
                    Description = "Loading market trade items every 5 mins",
                    Seconds = 20,//every 5 mins
                    Enabled = true,
                    StopOnError = false,
                    Type = typeof(LoadTradeHystoryTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public LoadTradeHystoryTask(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory, IMarketToolsService marketToolsService, IMarketDataPreprocessor dataPreprocessor, IMarketTradeItemEntityService tradeItemEntityService, IImportDataService importDataService) : base(config, workContext, exchangeDirectory)
        {
            _marketToolsService = marketToolsService;
            _dataPreprocessor = dataPreprocessor;
            _tradeItemEntityService = tradeItemEntityService;
            _importDataService = importDataService;
        }

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            this.ForEachPair((client, pair) => { Execute(log, pair); });
        }

        protected void Execute(TaskLogWriter log, string market)
        {
            log.Run($"Load trade history {market}", market, LoadMarketTradeHistory);
        }

        private void LoadMarketTradeHistory(TaskLogWriter log, string market)
        {
            var from = _tradeItemEntityService.GetLastTrade(market)?.DateUtc.AddMilliseconds(1) ?? DateTime.UtcNow.Date.AddDays(-2);

            DateTime to = DateTime.UtcNow;
            if ((to - from).Days > 30)
                to = from.AddDays(30);

            var listResponse = _marketToolsService.LoadMarketTradeHistory(market, @from, to);

            if (!listResponse.Success)
            {
                log.WriteFail($"Load trade history for {market} failed [{listResponse.Error}]");
                return;
            }

            if (listResponse.Data.Count == 0)
            {
                log.WriteFail($"{market} no new trades from {@from:g}");
                return;
            }


            double koef = 0.1; //i.e. threshold will be 20% from average amount this will reduce really small orders
            IList<MarketTradeItem> tradeItems = _dataPreprocessor.PreprocessTrades(listResponse.Data, market).Reduce(koef);

            double reduce = (1 - (double)tradeItems.Count / listResponse.Data.Count) * 100;


            if (Config.LogLevel.IsDetailedLogging())
            {
                //from = trades.Last().DateUtc.ToLocalTime();
                //var to = trades.First().DateUtc.ToLocalTime();
                log.WriteDetails($"{market} loaded #{listResponse.Data.Count} trades => {tradeItems.Count} [reduce {reduce}%]");
            }
            else
            {
                log.Write($"{market} loaded #{tradeItems.Count} trades");
            }

            _importDataService.ImportTradeItems(tradeItems);
        }

        
    }
}