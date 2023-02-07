using System;
using System.Diagnostics;
using System.Linq;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Infrastructure.Config;
using AVS.CoreLib.Services.Installation;
using AVS.CoreLib.Services.Tasks;
using AVS.CoreLib.Services.Tasks.AppTasks;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services;
using AVS.Trading.OrdersWatch.Tasks.TradingTools;

namespace AVS.Trading.OrdersWatch.Infrastructure
{
    public class CodeFirstInstallationService : InstallationServiceBase
    {
        protected override ScheduleTask[] Tasks => new[]{LoadMyOrdersTask.DefaultScheduleTask};

        public CodeFirstInstallationService(AppConfig config, IScheduleTaskService scheduleTaskService) : base(config, scheduleTaskService)
        {
        }
    }
}

