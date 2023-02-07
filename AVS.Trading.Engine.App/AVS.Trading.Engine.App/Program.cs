using System;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Utils;
using AVS.PoloniexApi;
using AVS.Trading.Core;
using AVS.Trading.Core.Models;
using AVS.Trading.Engine.Emulator;
using AVS.Trading.Engine.Emulator.Algorithms;
using AVS.Trading.Engine.Models;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Pipeline.Models;

namespace AVS.Trading.Engine.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Run(b =>
            {
                b.AddWebApiHost("Poloniex API", "https://poloniex.com/public?command=returnTicker");
                //b.AddWebApiHost("Exmo API", "https://api.exmo.com/v1/ticker/");
                b.TestWebApiHosts(false);
                b.SetupCulture("en");
                b.InitConfig<TradingAppConfig>();
                b.InitializeEngineContext(false);
                //b.InstallScheduledTasks(args.Length > 0 && args[0] == "-clear", true);
                // b.StartTaskManager();

                Test<SimpleAlgorithm>();
            });
            
        }

        public static void Test<T>() where T : IAlgorithm
        {
            var emulator = EngineContext.Current.Resolve<TradingEmulator<T>>();
            var parameters = new Parameters()
            {
                Pair = new CurrencyPair("BTC", "MAID"),
                Amount = 100,
                Start = DateTime.Today.AddDays(-60),
                End = DateTime.Today,
                Exchange = PoloniexConstants.PoloniexExchange,
                InitialBalance = new BalanceSheet()
            };
            parameters.InitialBalance.Credit(0.04,"BTC","initial balance");
            parameters.InitialBalance.Credit(400,"MAID","initial balance");
            emulator.Run(parameters);
        }
    }
}
