using System;
using System.Diagnostics;
using System.Linq;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Infrastructure.Config;
using AVS.CoreLib.Services.Installation;
using AVS.CoreLib.Services.Tasks;
using AVS.CoreLib.Services.Tasks.AppTasks;
using AVS.Trading.DataFiller.Tasks.MarketTools;
using AVS.Trading.DataFiller.Tasks.TradingTools;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services;

namespace AVS.Trading.DataFiller.Infrastructure
{
    public class CodeFirstInstallationService : InstallationServiceBase
    {
        private readonly IClearDataService _clearDataService;

        public CodeFirstInstallationService(AppConfig config, IScheduleTaskService scheduleTaskService, IClearDataService clearDataService) : base(config, scheduleTaskService)
        {
            _clearDataService = clearDataService;
        }

        protected override ScheduleTask[] Tasks => new[]
        {
            //VpnConnectionTask.DefaultScheduleTask,
            LoadChartDataTask.DefaultScheduleTask,
            //LoadMarketSummaryTask.DefaultScheduleTask,
            
            //LoadMarketOrdersTask.DefaultScheduleTask,
            //ShrinkOpenOrdersTask.DefaultScheduleTask,
            //LoadTradeHystoryTask.DefaultScheduleTask,
            //ShrinkTradeHystoryTask.DefaultScheduleTask,

            LoadTradesTask.DefaultScheduleTask
        };

        public override void ClearData()
        {
            Console.WriteLine("Clearing existing data");
            var deleted = _clearDataService.ClearMarketData();
            RecordsDeleted("MarketData", deleted);

            deleted = _clearDataService.ClearChartData();
            RecordsDeleted("ChartData", deleted);

            deleted = _clearDataService.ClearMarketTradeItems();
            RecordsDeleted("MarketTradeItems", deleted);

            deleted = _clearDataService.ClearMarketOrderBookData();
            RecordsDeleted("OrderBookData", deleted);

            deleted = _clearDataService.ClearTradeHistory();
            RecordsDeleted("TradeHistory", deleted);
        }

        private void RecordsDeleted(string entity, int deleted)
        {
            if (deleted > 0)
                Console.WriteLine($"{entity} #{deleted} records deleted");
        }
    }
}

