using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Data.Services.TradingTools;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.OrdersWatch.Tasks.TradingTools
{
    public class LoadMyOrdersTask : TaskBase
    {
        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Load My Open Orders",
                    Description = "Maintain records about open orders",
                    Group = "Trading tools",
                    Seconds = 20,
                    Enabled = true,
                    StopOnError = false,
                    Type = typeof(LoadMyOrdersTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        private readonly ITradingToolsService _tradingToolsService;
        private readonly ITradingDataPreprocessor _dataPreprocessor;
        private readonly IOpenOrderEntityService _openOrderEntityService;

        public LoadMyOrdersTask(TradingAppConfig config, IWorkContext workContext, 
            ExchangeDirectory exchangeDirectory, ITradingToolsService tradingToolsService, 
            ITradingDataPreprocessor dataPreprocessor, IOpenOrderEntityService openOrderEntityService) : base(config, workContext, exchangeDirectory)
        {
            _tradingToolsService = tradingToolsService;
            _dataPreprocessor = dataPreprocessor;
            _openOrderEntityService = openOrderEntityService;
        }

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            log.Write(Config.ToString());
            this.ForEachPair((client, pair) =>
            {
                try
                {
                    ProcessAsync(log, pair);
                }
                catch (Exception ex)
                {
                    log.WriteFail($"Import orders for {pair} failed - {ex.Message}");
                }
            });
        }

        protected void ProcessAsync(TaskLogWriter log, ExchangePair pair)
        {
            ParallelDataLoader.ParallelLoad(
                ()=> _tradingToolsService.GetOpenOrders(pair),
                ()=> _tradingToolsService.LoadTrades(pair),
                ()=> _openOrderEntityService.GetAll(o => o.Pair == pair.Value && o.State <= OrderState.Executed),
                (limitOrders, trades, existingOrders) =>
                {
                    var openOrders = _dataPreprocessor.PreprocessOrders(limitOrders, o => o.Pair = pair);
                    ProcessOpenOrders(log, pair, openOrders, existingOrders, trades);
                }
            );
        }

        private void ProcessOpenOrders(TaskLogWriter log,
            ExchangePair pair,
            IList<OpenOrder> openOrders, 
            IList<OpenOrder> existingOrders, 
            IList<ITrade> trades)
        {
            int imported =0;
            foreach (var order in openOrders)
            {
                var item = existingOrders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);
                if (item == null)
                {
                    _openOrderEntityService.Insert(order);
                    imported++;
                }
                else
                {
                    //update existing order
                    if (Math.Abs(item.AmountQuote * item.Price - order.AmountQuote * order.Price) >
                        Constants.OneSatoshi)
                    {
                        item.Price = order.Price;
                        item.AmountQuote = order.AmountQuote;
                        item.AmountBase = order.AmountBase;
                        _openOrderEntityService.Update(item);
                    }

                    //remove item from the list
                    existingOrders.Remove(item);
                }
                
            }


            int canceled = 0;
            int executed = 0;
            int partiallyExecuted = 0;

            foreach (var order in existingOrders)
            {
                if (order.State == OrderState.Canceled)
                {
                    if (order.DateUtc < DateTime.UtcNow.AddDays(-1))
                    {
                        _openOrderEntityService.Delete(order);
                    }
                    continue;
                }
                if (order.State == OrderState.Executed)
                {
                    if (order.DateUtc < DateTime.UtcNow.AddDays(-7))
                    {
                        order.State = OrderState.Archived;
                        _openOrderEntityService.Update(order);
                    }
                    continue;
                }

                var tradeAmount = trades.Where(t => t.OrderNumber == order.OrderNumber).Sum(t => t.AmountQuote);
                if (tradeAmount > 0)
                {
                    if (Math.Abs(tradeAmount - order.AmountQuote) <= Constants.OneSatoshi)
                    {
                        order.State = OrderState.Executed;
                        order.DateUtc = DateTime.UtcNow;
                        executed++;
                    }
                    else
                    {
                        order.State = OrderState.PartiallyExecuted;
                        order.DateUtc = DateTime.UtcNow;
                        partiallyExecuted++;
                    }
                }
                else
                {
                    //here we can request returnOrderStatus to ensure that order is canceled but it's clear that order might be already canceled
                    order.State = OrderState.Canceled;
                    order.DateUtc = DateTime.UtcNow;
                    canceled++;
                }
                _openOrderEntityService.Update(order);
            }

            log.WriteF($"{pair} [imported {imported}; executed {executed}/{partiallyExecuted}; canceled:{canceled}]");
        }

    }
}