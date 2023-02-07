using System;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.Extensions;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Services.MarketTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.DataFiller.Tasks.MarketTools
{
    /// <summary>
    /// loads market order book
    /// </summary>
    public partial class LoadMarketOrdersTask : TaskBase
    {
        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Load Market Orders",
                    Description = "Load open orders",
                    Group = "Market tools",
                    Seconds = 300,
                    Enabled = true,
                    StopOnError = false,
                    Type = typeof(LoadMarketOrdersTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public LoadMarketOrdersTask(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory, IMarketToolsService marketToolsService, IMarketDataPreprocessor dataPreprocessor, IImportDataService importDataService) : base(config, workContext, exchangeDirectory)
        {
            _marketToolsService = marketToolsService;
            _dataPreprocessor = dataPreprocessor;
            _importDataService = importDataService;
        }

        private readonly IMarketToolsService _marketToolsService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;
        private readonly IMarketDataEntityService _marketDataEntityService;
        private readonly IImportDataService _importDataService;
    
        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            this.ForEachPair(pair =>
            {
                try
                {
                    //load the past trades from the latest one up to UtcNow
                    var response = _marketToolsService.LoadOrderBook(pair);

                    if (!response.Success)
                    {
                        log.Write($"{pair} load orders failed: {response.Error}");
                        return;
                    }

                    var book = _dataPreprocessor.PreprocessOrderBook(response.Data);

                    if (ShouldPrint(pair))
                    {
                        log.Write(book.ToString());
                        if (Config.LogLevel.IsDetailedLogging())
                        {
                            //log.WriteDetails(book.GetInfo());
                        }
                    }

                    var wallsChanged = _importDataService.ImportOrderBook(book);
                }
                catch (Exception ex)
                {
                    log.Write($"{pair} failed - {ex.Message}");
                    log.WriteDetails(ex.ToString());
                }
            });
        }

        
    }
}