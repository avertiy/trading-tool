using System.Threading.Tasks;
using AVS.CoreLib.ConsoleTools.Bootstraping;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.OrdersWatch.Tasks.TradingTools;

namespace AVS.Trading.OrdersWatch
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Bootstrap.RunAsService("avs-orders-watch", x => x.OnStart(b =>
            {
                b.AddStartingAppMessage("Starting Orders Watch..");
                b.SetupCulture("en");
                var config = b.InitConfig<TradingAppConfig>();

                b.AddWebApiHost("Poloniex API", "https://poloniex.com/public?command=returnTicker");
                //b.AddWebApiHost("Exmo API", "https://api.exmo.com/v1/ticker/");
                b.TestWebApiHosts(false);
                b.InitializeEngineContext();
                //b.InstallScheduledTasks(args.Length > 0 && args[0] == "clear", true);
                //b.StartTaskManager();
                TaskManager.ExecuteTask<LoadMyOrdersTask>();
            }));
        }
    }
}
