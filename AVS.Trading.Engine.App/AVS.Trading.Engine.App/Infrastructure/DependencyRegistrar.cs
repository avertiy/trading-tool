using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autofac;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.CoreLib.Services.Installation;
using AVS.CoreLib.Services.Logging;
using AVS.CoreLib.Services.Logging.Loggers;
using AVS.CoreLib.Services.Tasks;
using AVS.CoreLib.Services.Tasks.AppTasks;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services;

namespace AVS.Trading.Engine.App.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
        }
        
        public int Order => 100;
    }

    public class CodeFirstInstallationService : IInstallationService
    {
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly IClearDataService _clearDataService;

        private readonly TradingAppConfig _config;

        public CodeFirstInstallationService(TradingAppConfig config, IScheduleTaskService scheduleTaskService, IClearDataService clearDataService)
        {
            _config = config;
            _scheduleTaskService = scheduleTaskService;
            _clearDataService = clearDataService;
        }

        public void InstallScheduledTasks(bool reinitialize = false)
        {
            var scheduleTasks = _scheduleTaskService.GetAll(t => t.ApplicationInstanceId == _config.AppInstance.Id)
                .ToArray();

            if (!reinitialize && scheduleTasks.Any())
                return;
            foreach (var task in scheduleTasks)
            {
                _scheduleTaskService.Delete(task);
            }

            var sendEmails = QueuedMessagesSendTask.DefaultScheduleTask;
            sendEmails.Enabled = false;
            var tasks = new ScheduleTask[]
            {
                VpnConnectionTask.DefaultScheduleTask,
            };

            foreach (var scheduleTask in tasks)
            {
                if (!string.IsNullOrEmpty(_config.Tasks.InstallForAppInstanceId))
                    scheduleTask.ApplicationInstanceId = _config.Tasks.InstallForAppInstanceId;
                Debug.WriteLine($"ScheduledTask {scheduleTask.Name} has been inserted");
                _scheduleTaskService.Insert(scheduleTask);
            }
        }

        public void InstallData()
        {

        }

        public void ClearData()
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
