using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.CoreLib.ConsoleTools.Bootstraping;
using AVS.Trading.Framework.Infrastructure;

namespace AVS.Trading.MarginAccountTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.RunAsService("avs-data-filler", x => x.OnStart(b =>
            {
                b.SetupCulture("en");
                b.InitConfig<TradingAppConfig>();
                b.AddWebApiHost("Poloniex API", "https://poloniex.com/public?command=returnTicker");
                b.TestWebApiHosts(false);
                b.InitializeEngineContext();
                b.InstallScheduledTasks(clearData: args.Length > 0 && args[0] == "clear", reinitialize: true);
                b.StartTaskManager();
            }));
        }
    }
}
