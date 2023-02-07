using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.Extensions;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;
using AVS.CoreLib.Extensions;
using AVS.Trading.Data.Services.MarketTools;

namespace AVS.Trading.DataFiller.Tasks.MarketTools
{
    /// <summary>
    /// Represents a task for loading market data (coin prices)
    /// </summary>
    public partial class LoadMarketSummaryTask : TaskBase
    {
        private readonly MarketToolsDataAdapter _dataAdapter;
        private readonly IImportDataService _importDataService;
        private readonly IMarketDataEntityService _marketDataEntityService;
        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Load Market Summary",
                    Description = "",
                    Group = "Market Tools",
                    Seconds = 60,
                    Enabled = false,
                    StopOnError = true,
                    Type = typeof(LoadMarketSummaryTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public LoadMarketSummaryTask(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory, MarketToolsDataAdapter dataAdapter, IImportDataService importDataService, IMarketDataEntityService marketDataEntityService) : base(config, workContext, exchangeDirectory)
        {
            _dataAdapter = dataAdapter;
            _importDataService = importDataService;
            _marketDataEntityService = marketDataEntityService;
        }

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            //foreach(var client in Clients){...}

            this.ForEachExchange(client =>
            {
                IList<MarketData> marketData = null;

                Action action = new Action(() =>
                {
                    string[] pairs = this.GetPairs(client);
                    marketData = _dataAdapter.GetMarketData(pairs).ToList();
                    _importDataService.ImportTickerData(marketData);
                });

                var result = action.Invoke(log).OnSucess("Load ticker OK").OnFail("Load ticker FAILED");
                
                log.Write($"loaded market data #{marketData.Count} currency pairs - {result}");

                if (Config.LogLevel.IsDetailedLogging())
                {
                    foreach (var data in marketData)
                    {
                        if (ShouldPrint(data.Pair))
                            log.WriteDetails(data.ToString());
                    }
                }
                
                //clean up every hour
                if (DateTime.Now.Minute == 1)
                {
                    _marketDataEntityService.Shrink();
                    _marketDataEntityService.DeleteOldRecords(90);
                }
            });
        }


    }
}