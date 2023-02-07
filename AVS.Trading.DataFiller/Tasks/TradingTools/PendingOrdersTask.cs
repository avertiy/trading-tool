using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.ExmoApi.Tasks;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Services.TradingTools;

namespace AVS.Trading.DataFiller.Tasks.TradingTools
{
    /*
    public class PendingOrdersTask : ProcessMarketTaskBase
    {
        private readonly TradingToolsDataAdapter _tradingToolsDataAdapter;
        private readonly MarketToolsDataAdapter _marketToolsDataAdapter;
        private readonly IMyOrderEntityService _myOrderEntityService;

        private IList<MarketData> _marketsData;

        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Pending orders task",
                    Description = "Trading tools. Watch market conditions to execute pending orders.",
                    Group = "Trading tools",
                    Seconds = 60,//720
                    Enabled = true,
                    StopOnError = false,
                    Type = typeof(PendingOrdersTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public PendingOrdersTask(TradingAppConfig config, TradingToolsDataAdapter tradingToolsDataAdapter, IMyOrderEntityService myOrderEntityService, MarketToolsDataAdapter marketToolsDataAdapter) : base(config)
        {
            _tradingToolsDataAdapter = tradingToolsDataAdapter;
            _myOrderEntityService = myOrderEntityService;
            _marketToolsDataAdapter = marketToolsDataAdapter;
        }

        protected override bool IterateMarkets(TaskLogWriter log, string[] markets)
        {
            _marketsData = _marketToolsDataAdapter.GetMarketData(markets);
            return base.IterateMarkets(log, markets);
        }

        protected override bool Execute(TaskLogWriter log, string market)
        {
            var pair = CurrencyPair.Parse(market);
            if (pair == null)
            {
                log.IterationFailed("Invalid market " + market);
                return false;
            }

            var marketData = _marketsData.FirstOrDefault(m => m.Pair == pair);
            if (marketData == null)
            {
                log.IterationFailed("MarketData is null for " + market);
                return false;
            }
                
            var orders =
                _myOrderEntityService.GetAll(o => o.Market == pair.ToString() && o.State < OrderState.Executed).ToList();

            if (!orders.Any())
                return true;

            var pendingOrders = orders.Where(o => o.State == OrderState.Pending).ToArray();

            foreach (var order in pendingOrders)
            {
                var diffPercentage = marketData.PriceLast.DiffPercentage(order.Price); // (PriceLast - order.Price)/PriceLast (%)

                if (order.Type == OrderType.Sell)
                {
                    if(diffPercentage < -1.5)// < -1.5% - market price is too low  we are not interested 
                        continue;

                    order.State = OrderState.Processing;
                    _myOrderEntityService.Update(order);

                    if (diffPercentage >= 0) // PriceLast >= order.Price => we can sell
                    {
                        if (marketData.HighestBid >= order.Price)
                        {

                        }
                    }
                    
                }

                if (order.Type == OrderType.Buy)
                {
                    if (diffPercentage > 1.5)// > 1.5% - market price is too high we are not interested 
                        continue;
                    order.State = OrderState.Processing;
                    _myOrderEntityService.Update(order);
                }
            }

            
            var last = _tradingToolsDataAdapter.GetLastTrade(pair.ToString(), TradeCategory.Exchange | TradeCategory.MarginTrade);
            var trades = _tradingToolsDataAdapter.LoadLatestTrades(last, pair.ToString(), DefaultScheduleTask.Seconds);

            log.WriteDetailsIf(trades.Count > 0, $"{market} loaded {trades.Count} trades");

            return true;
        }
    }*/
}