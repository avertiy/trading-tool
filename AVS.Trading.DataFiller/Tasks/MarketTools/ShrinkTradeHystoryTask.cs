using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Extensions;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.Trading.DataFiller.Tasks;
using AVS.Trading.Framework.Extensions;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Data.Services.MarketTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.DataFiller.Tasks.MarketTools
{
    /// <summary>
    /// Reduces number of trade items without data losses for avg calculations 
    /// i.e. small trades are combined into bigger so trade items count becomes smaller
    /// </summary>
    public partial class ShrinkTradeHystoryTask : TaskBase
    {
        private readonly IMarketTradeItemEntityService _tradeItemEntityService;

        public ShrinkTradeHystoryTask(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory, IMarketTradeItemEntityService tradeItemEntityService) : base(config, workContext, exchangeDirectory)
        {
            _tradeItemEntityService = tradeItemEntityService;
        }

        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Shrink Trade History",
                    Description = "Shrinks trade items table every day",
                    Seconds = 1799,//every 5 mins
                    Enabled = false,
                    StopOnError = false,
                    Type = typeof(ShrinkTradeHystoryTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            this.ForEachPair((client, pair) => Execute(log, pair));
        }

        protected void Execute(TaskLogWriter log, string market)
        {

            var cp = CurrencyPair.Parse(market);
            //we don't shrink items bigger than this threshold
            double thresholdAmountBase = 100;
            if (cp.BaseCurrency == "BTC")
                thresholdAmountBase = 1;
            if (cp.BaseCurrency == "ETH")
                thresholdAmountBase = 10;

            var trade = _tradeItemEntityService.GetFirstTrade(market);
            if (trade == null)
                return;

            var from = trade.DateUtc;
            var to = DateTime.Today.AddDays(-1);

            foreach (var month in @from.EachMonth(to))
            {
                var start = month;
                var end = month.AddMonths(1);
                if (end > to)
                    end = to;

                var count = _tradeItemEntityService.Count(market, start, end, thresholdAmountBase);

                if (count < 2000)
                    continue;

                var trades = _tradeItemEntityService.Search(market, start, end, thresholdAmountBase);

                var k = ReduceKoef(trades.Count);

                var reducedTrades = trades.Reduce(k);

                double compression = (1 - (double) trades.Count / reducedTrades.Count) * 100;

                log.Write($"{market} {month:Y} shrinked - {compression}% [#{trades.Count} => #{reducedTrades.Count}]");

                //delete cutted trade items 
                var ids = new List<int>();
                foreach (var item in trades.ToArray())
                {
                    if (reducedTrades.Any(t => t.Id == item.Id))
                        continue;
                    ids.Add(item.Id);
                }

                _tradeItemEntityService.DeleteMany(ids);

                //insert new trade items i.e. small items items have been joined and appended to the resulting array to avoid avg data losses
                var newItems = reducedTrades.Where(t => t.Id == 0).ToArray();
                _tradeItemEntityService.BulkInsert(newItems);
            }
        }

        private double ReduceKoef(int count)
        {
            double k = 1;
            if (count > 5000)
                k = 1.5;
            else if (count > 10000)
                k = 2;
            else if (count > 20000)
                k = 3;
            return k;
        }

        
    }
}