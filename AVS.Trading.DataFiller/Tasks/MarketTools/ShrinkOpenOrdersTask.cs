using System;
using System.Linq;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Data.Services.MarketTools;

namespace AVS.Trading.DataFiller.Tasks.MarketTools
{
    /// <summary>
    /// replaces OrderBooks for a day with a single orderbook 
    /// </summary>
    public partial class ShrinkOpenOrdersTask : ITask
    {
        private readonly IOrderBookEntityService _openOrderBookEntityService;
        
        private readonly TradingAppConfig _config;

        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Shrink Open Orders",
                    Seconds = 1799,//
                    Enabled = false,
                    StopOnError = false,
                    Type = typeof(ShrinkOpenOrdersTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public ShrinkOpenOrdersTask(TradingAppConfig config, IOrderBookEntityService openOrderBookEntityService)
        {
            _config = config;
            _openOrderBookEntityService = openOrderBookEntityService;
        }

        public void Execute(TaskLogWriter log)
        {
            var date = DateTime.Today.AddDays(-1);
            var books = _openOrderBookEntityService.GetOrderBooksByDate(date).ToList();

            var pairs = books.Select(b => b.Pair).Distinct().ToArray();
            foreach (var pair in pairs)
            {
                var count = books.Count(b => b.Pair == pair);
                if(count == 1)
                    continue;

                _openOrderBookEntityService.ShrinkOpenOrders(date, pair);
                log.Write($"Orderbooks for pair {pair} for {date:d} have been shrinked");
            }
        }
    }
}