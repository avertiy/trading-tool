using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Domain.TradingTools;
using OrderBook = AVS.Trading.Core.ResponseModels.OrderBook;

namespace AVS.Trading.Engine.Emulator.Context
{
    public class AlgorithmContext
    {
        public string Exchange { get; set; }
        public CurrencyPair Pair;
        public string Algorithm { get; set; }
        /// <summary>
        /// tmp property means amount the algorithm operate with per transaction
        /// </summary>
        public double Amount;
        //public TradeSessionContext Session { get; set; }
        public MarketContext Market { get; set; }
        public StateContext State { get; set; }
        public TradingContext Trading { get; set; }

        public AlgorithmContext()
        {
          //  Session = new TradeSessionContext();
            Market = new MarketContext();
            State = new StateContext();
            Trading = new TradingContext();
        }

        public override string ToString()
        {
            return $"{Exchange} {Pair} {Algorithm}";
        }

        public void RestoreState(TradeSession session)
        {
            Amount = session.Amount;
            State.OpenOrderIds = session.GetOrderNumbers();
            State.Balance = RestoreBalance(session);
        }

        private BalanceSheet RestoreBalance(TradeSession session)
        {
            var balance = session.BalanceSheet;
            //find out executed orders and update balance respectively 
            foreach (var orderId in State.OpenOrderIds.ToArray())
            {
                var trade = Trading.Trades.FirstOrDefault(t => t.OrderId == orderId);
                if (trade == null)
                    continue;

                balance.Execute(trade, Pair);
                State.OpenOrderIds.Remove(orderId);
            }
            //session.BalanceSheet = balance;
            return balance;
        }

        public void SaveState(TradeSession session)
        {
            session.Amount = Amount;
            session.Algorithm = Algorithm;
            session.SaveOrderNumbers(State.OpenOrderIds);
            session.BalanceSheet = State.Balance;
            session.LastActivity = DateTime.Now;
        }
    }

    public class StateContext
    {
        public BalanceSheet Balance;
        public DateTime Started { get; set; }
        public List<string> OpenOrderIds { get; set; } = new List<string>();
    }

    public class TradingContext
    {
        /// <summary>
        /// Trade history 
        /// </summary>
        public IList<TradeItem> Trades { get; set; }
        public IList<OpenOrder> OpenOrders { get; set; }

        public override string ToString()
        {
            return $"#{Trades.Count} transactions; #{OpenOrders.Count} open orders";
        }
    }
    
    public class MarketContext
    {
        //ticker here means price data for a certain pair
        //the entity is used due to form exchange we get ticker for all pairs 
        public MarketData Ticker { get; set; }
        public OrderBook OrderBook { get; set; }
        public IList<ICandlestick> ChartData { get; set; }
    }

    public static class AlgorithmContextExtensions
    {
        public static void Validate(this AlgorithmContext ctx, ContextEnum part)
        {
            if (ctx == null)
                throw new AlgorithmContextException("Context is null");

            if (part.HasFlag(ContextEnum.Default))
            {
                if(ctx.Amount<=0)
                    throw new AlgorithmContextException("Amount must be a positive number");
                if(string.IsNullOrEmpty(ctx.Exchange))
                    throw new AlgorithmContextException("Exchange is required");
                if (string.IsNullOrEmpty(ctx.Algorithm))
                    throw new AlgorithmContextException("Algorithm is required");
                if (ctx.Pair == null)
                    throw new AlgorithmContextException("Pair is null");
            }

            if (ctx.Market == null)
                throw new AlgorithmContextException("Market context is null");

            if (part.HasFlag(ContextEnum.Ticker) && ctx.Market.Ticker == null)
                throw new AlgorithmContextException("Ticker is null");
            if (part.HasFlag(ContextEnum.OrderBook) && ctx.Market.OrderBook == null)
                throw new AlgorithmContextException("OrderBook is null");
            if (part.HasFlag(ContextEnum.Chart) && ctx.Market.ChartData == null)
                throw new AlgorithmContextException("Chart is null");
            
            if (ctx.Trading == null)
                    throw new AlgorithmContextException("Trading context is null");

            if (part.HasFlag(ContextEnum.Trades) && ctx.Trading.Trades == null)
                throw new AlgorithmContextException("Trades is null");
            if (part.HasFlag(ContextEnum.OpenOrders) && ctx.Trading.OpenOrders == null)
                throw new AlgorithmContextException("OpenOrders is null");

            if (ctx.State == null)
                throw new AlgorithmContextException("State context is null");

            if (part.HasFlag(ContextEnum.Balance) && ctx.State.Balance == null)
                throw new AlgorithmContextException("Balance is null");
        }
    }


    [Flags]
    public enum ContextEnum
    {
        Default = 0,
        Ticker = 1,
        OrderBook =2,
        Chart =4,
        Trades = 8,
        OpenOrders = 16,
        Balance=32,
        All =64
    }


    /*
    public class TradeSessionContext : StateContext
    {
        public int SessionId { get; set; }
        public DateTime Opened { get; set; }
        public TradingRange Range { get; set; }
        public PositionMap Positions { get; set; }

        public TradeSessionContext()
        {
            Positions = new PositionMap();
        }

        public void Initialize(TradeSession entity)
        {
            if (entity.Closed.HasValue)
                throw new InvalidOperationException("Trade session has already been closed");
            SessionId = entity.Id;
            Exchange = entity.Exchange;
            //Pair = entity.Pair;
            Algorithm = entity.Algorithm;
            Opened = entity.Opened;
            Range = entity.GetTradingRange();
            BalanceSheet.Initialize(entity.Balances);
        }

        public override string ToString()
        {
            return $"{Exchange} {Pair} {Algorithm} {Opened:d} {Positions} {BalanceSheet}";
        }
    }*/
}