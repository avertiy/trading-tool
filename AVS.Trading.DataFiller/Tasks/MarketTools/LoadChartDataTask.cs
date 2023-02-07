using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data.Domain.Logging;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.Extensions;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.Trading.Core;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Data.Domain.MarketTools.Chart;
using AVS.Trading.Data.Services.MarketTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.DataFiller.Tasks.MarketTools
{
    /// <summary>
    /// Represents a task for sending queued message 
    /// </summary>
    public class LoadChartDataTask : TaskBase
    {
        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Load Chart Data",
                    Seconds = 30,//* 5,
                    Enabled = false,
                    StopOnError = false,
                    Type = typeof(LoadChartDataTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        private readonly IMarketToolsService _marketToolsService;
        private readonly IChartDataEntityService _chartDataEntityService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;
        private readonly IImportDataService _importDataService;

        private IPairProvider PairProvider => ((ExchangeServiceBase) _marketToolsService).Client.Pairs;

        

        public LoadChartDataTask(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory, IMarketToolsService marketToolsService, IChartDataEntityService chartDataEntityService, IMarketDataPreprocessor dataPreprocessor, IImportDataService importDataService) : base(config, workContext, exchangeDirectory)
        {
            _marketToolsService = marketToolsService;
            _chartDataEntityService = chartDataEntityService;
            _dataPreprocessor = dataPreprocessor;
            _importDataService = importDataService;
        }

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            this.ForEachPair((client, pair) => { Execute(log, pair); });
        }

        protected void Execute(TaskLogWriter log, string market)
        {
            var period = MarketPeriod.M5;
            var utcNow = DateTime.UtcNow;
            try
            {
                var from = _chartDataEntityService.GetLastCandle(market, period)?.To.AddMilliseconds(1);
                if (!from.HasValue)
                {
                    from = DateTime.UtcNow.AddDays(-1);
                }

                if (from.Value.AddSeconds((int) period) > utcNow)
                    return;

                var chartData = _marketToolsService.LoadChartData(market, period, @from.Value, utcNow);
                if (!chartData.Success)
                {
                    log.WriteFail($"Failed chart data load {chartData.Error}");
                    return;
                }

                var pair = CurrencyPair.Parse(market);
                Chart chart = _dataPreprocessor.PreprocessChartData(chartData.Data, pair.ToString(), period, from.Value, utcNow);
                
                if (Config.LogLevel.IsDetailedLogging())
                {
                    log.WriteDetails(chart.GetInfo(pair));
                }
                else
                {
                    log.Write(chart.ToString());
                }
                _importDataService.ImportChartData(chart);
            }
            catch (Exception ex)
            {
                log.WriteError($"Load chart data for {market} FAILED",ex);
            }
        }

        
    }

    
}