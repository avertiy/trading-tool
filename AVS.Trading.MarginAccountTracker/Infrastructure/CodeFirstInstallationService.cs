using System;
using System.Diagnostics;
using System.Linq;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Infrastructure.Config;
using AVS.CoreLib.Services.Installation;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.Framework.Services;
using AVS.Trading.MarginAccountTracker.Tasks;

namespace AVS.Trading.MarginAccountTracker.Infrastructure
{
    public class CodeFirstInstallationService : InstallationServiceBase
    {

        public CodeFirstInstallationService(AppConfig config, IScheduleTaskService scheduleTaskService) : base(config, scheduleTaskService)
        {
        }

        protected override ScheduleTask[] Tasks => new[]
        {
            MarginAccountTrackerTask.DefaultScheduleTask
        };
    }
}

