using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.CoreLib.ConsoleTools.Bootstraping;
using AVS.CoreLib.ConsoleTools.Utils;
using AVS.CoreLib.Infrastructure;
using AVS.PoloniexApi.General;
using AVS.PoloniexApi.LiveTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.OrderBookWatch.Commands;

namespace AVS.Trading.OrderBookWatch
{
    class Program
    {
        private static WatchOrderBookService _watchService;
        static void Main(string[] args)
        {
            //Bootstrap.RunAsService("avs-orderbook-watch-service", x => x.OnStart(b =>
            //{
            //    b.SetupCulture("en");
            //    b.UseDefaultConfig<TradingAppConfig>("orderbook-watcher");
            //    b.AddWebApiHost("Poloniex API", "https://poloniex.com/public?command=returnTicker");
            //    b.TestWebApiHosts(false);
            //    b.InitializeEngineContext();
            //    _watchService = EngineContext.Current.Resolve<WatchOrderBookService>();
            //}));


            _watchService = new WatchOrderBookService(new PoloniexChannelClient());
            Subscribe("XRP_BTC", 10);
            Subscribe("DOGE_BTC", 1000);
            Subscribe("BTC_USDT", 0.0001);
            Subscribe("BTC_USDC", 0.0001);
            
            _watchService.PrintPairs();
            ConsoleInput.Register(new SubscribePairCommand(), new PrintOptionsCommand(), new SwitchPairCommand());
            ConsoleInput.WaitForInput();
        }

        public static void Subscribe(string pair, double minOrderAmount)
        {
            var task = _watchService.SubscribeOn(pair, minOrderAmount);
            Task.WaitAll(task);
        }
    }

    public class PriceRangeTrackerSettings
    {
        public string Pair { get; set; }
        public PriceRange Range { get; set; }
        public double AmountToBuy { get; set; }
        public double AmountToSell { get; set; }
    }

}
