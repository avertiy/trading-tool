using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib._System.Threading;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Pipeline.Models;
using AVS.Trading.Pipeline.TradingAlgorithms.Context;

namespace AVS.Trading.Engine.Emulator.Context
{
    public interface IContextBuilder
    {
        AlgorithmContext Build(TradeSession session, ContextEnum setup);
        Task<AlgorithmContext> BuildAsync(TradeSession session, ContextEnum setup);
    }

    public class EmulatorContextBuilder : IContextBuilder
    {
        private readonly EmulatorDataProvider _dataProvider;

        public EmulatorContextBuilder(EmulatorDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public virtual AlgorithmContext Build(TradeSession session, ContextEnum setup)
        {
            var ctx = new AlgorithmContext
            {
                Algorithm = session.Algorithm,
                Exchange = session.Exchange,
                Pair = CurrencyPair.Parse(session.Pair),
                Amount = session.Amount,
            };


            if (setup.HasFlag(ContextEnum.Ticker))
            {
                LoadTicker(ctx);
            }

            if (setup.HasFlag(ContextEnum.OpenOrders))
            {
                LoadOpenOrders(ctx);
            }
            if (setup.HasFlag(ContextEnum.Trades))
            {
                LoadTradeHistory(ctx);
            }

            if (setup.HasFlag(ContextEnum.Chart))
            {
                LoadChartData(ctx);
            }
            
            return ctx;
        }

        private void LoadChartData(AlgorithmContext ctx)
        {
            ctx.Market.ChartData = _dataProvider.ChartData;
        }

        private void LoadTicker(AlgorithmContext ctx)
        {
            ctx.Market.Ticker = _dataProvider.Ticker;
        }

        public Task<AlgorithmContext> BuildAsync(TradeSession session, ContextEnum setup)
        {
            throw new NotImplementedException();
        }

        protected void LoadTradeHistory(AlgorithmContext ctx)
        {
            ctx.Trading.Trades = _dataProvider.Trades;
        }

        protected void LoadOpenOrders(AlgorithmContext ctx)
        {
            ctx.Trading.OpenOrders = _dataProvider.OpenOrders;
        }
    }

    


    class ContextBuilder : IContextBuilder
    {
        private readonly ITradingToolsService _tradingToolsService;
        private readonly ITradingDataPreprocessor _dataPreprocessor;

        public ContextBuilder(ITradingToolsService tradingToolsService, ITradingDataPreprocessor dataPreprocessor)
        {
            _tradingToolsService = tradingToolsService;
            _dataPreprocessor = dataPreprocessor;
        }

        public AlgorithmContext Build(TradeSession session, ContextEnum setup)
        {
            var ctx = new AlgorithmContext();
            //ctx.Session.Initialize(session);
            using (NoSynchronizationContextScope.Enter())
            {
                var task = BuildAsync(ctx, setup);
                task.Wait();
            }

            return ctx;
        }
        
        public async Task<AlgorithmContext> BuildAsync(TradeSession session, ContextEnum setup)
        {
            var ctx = new AlgorithmContext();
            using (NoSynchronizationContextScope.Enter())
            {
                await BuildAsync(ctx, setup);
                return ctx;
            }
        }

        protected async Task BuildAsync(AlgorithmContext ctx, ContextEnum setup)
        {
            var tasks = new List<Task>();

            //run tasks
            if (setup.HasFlag(ContextEnum.Ticker))
            {

            }

            if (setup.HasFlag(ContextEnum.OpenOrders))
            {
                tasks.Add(LoadOpenOrdersAsync(ctx));
            }
            if (setup.HasFlag(ContextEnum.Trades))
            {
                tasks.Add(LoadTradeHistoryAsync(ctx));
            }

            if (!tasks.Any())
            {
                return;
            }

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        protected async Task LoadOpenOrdersAsync(AlgorithmContext ctx)
        {
            var response = await _tradingToolsService.GetOpenOrdersAsync(ctx.Pair.ToString());

            if (response.HasError)
                throw new Exception($"Unable to load OpenOrders {response.Error}");

            var orders = _dataPreprocessor.PreprocessOpenOrders(response.Data, ctx.Pair.ToString());

            //todo filter out orders opened not within current trade session (by current algorithm)
            ctx.Trading.OpenOrders = orders;
        }

        protected async Task LoadTradeHistoryAsync(AlgorithmContext ctx)
        {
            var response = await _tradingToolsService.LoadTradesAsync(ctx.Pair.ToString());
            if (response.HasError || response.Data == null)
                throw new Exception($"Load trade history failed {response.Error}");

            var trades = response.Data.Where(t => t.DateUtc >= ctx.State.Started);
            //todo filter out trades done manually or not by current algorithm
            var sessionTrades = _dataPreprocessor.PreprocessTrades(trades, ctx.Pair.ToString());

            //ctx.Session.Positions.Update()
            //todo update PositionMap left only latest orders

            ctx.Trading.Trades = sessionTrades;

            //if (BuildPositionMap)
            //{
            //    ctx.TradingContext.Trades[0].
            //    ctx.Positions = new PositionMap();
            //}
        }
    }

   
}