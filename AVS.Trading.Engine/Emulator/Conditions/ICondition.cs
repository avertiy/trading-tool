using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools.Chart;
using AVS.Trading.Engine.Emulator.Context;

namespace AVS.Trading.Engine.Emulator.Conditions
{
    public interface ICondition
    {
        bool Test(AlgorithmContext ctx, TradeIntention intention);
    }

    public class CompositeCondition : ICondition
    {
        protected List<ICondition> Children = new List<ICondition>();

        public void Add(ICondition condition)
        {
            Children.Add(condition);
        }

        public bool Test(AlgorithmContext ctx, TradeIntention intention)
        {
            return Children.All(c => c.Test(ctx, intention));
        }
    }

    public class BalanceCondition : ICondition
    {
        public bool Test(AlgorithmContext ctx, TradeIntention intention)
        {
            ctx.Validate(ContextEnum.Ticker|ContextEnum.Balance);
            if (intention == TradeIntention.Buy)
            {
                var amountBase = ctx.Amount * ctx.Market.Ticker.PriceLast;
                return ctx.State.Balance[ctx.Pair.BaseCurrency].Balance > amountBase;
            }
            else
            {
                return ctx.State.Balance[ctx.Pair.QuoteCurrency].Balance > ctx.Amount;
            }
        }
    }

    public class TickerAvgLowPriceCondition : ICondition
    {
        public int N = 10;

        public bool Test(AlgorithmContext ctx, TradeIntention intention)
        {
            ctx.Validate(ContextEnum.Ticker | ContextEnum.Chart);
            
            var avgprice = GetAvgPrice(ctx.Market.ChartData);
            if (intention == TradeIntention.Buy)
                return ctx.Market.Ticker.PriceLast < avgprice;
            else
                return ctx.Market.Ticker.PriceLast > avgprice;
        }

        private double GetAvgPrice(IEnumerable<ICandlestick> candles)
        {
            return candles.Take(N).Average(c => c.Low);
        }
    }

    public class NoSimilarOpenOrderCondition : ICondition
    {
        public double Distance = 0.01;//1%

        public bool Test(AlgorithmContext ctx, TradeIntention intention)
        {
            ctx.Validate(ContextEnum.Ticker | ContextEnum.OpenOrders);

            var price = ctx.Market.Ticker.PriceLast;
            var exists = false;
            if (intention == TradeIntention.Buy)
            {
                exists = ctx.Trading.OpenOrders.Any(
                    o => o.Type == OrderType.Buy &&
                         o.Price.WithinDistance(price, Distance));
            }
            else
            {
                exists = ctx.Trading.OpenOrders.Any(
                    o => o.Type == OrderType.Sell &&
                         o.Price.WithinDistance(price, Distance));
            }

            return exists == false;
        }
    }

    public class NotLastTradeCondition : ICondition
    {
        public double Distance = 0.01;//1%

        public bool Test(AlgorithmContext ctx, TradeIntention intention)
        {
            ctx.Validate(ContextEnum.Ticker | ContextEnum.Trades);

            var price = ctx.Market.Ticker.PriceLast;
            var trade = ctx.Trading.Trades.LastOrDefault();
            if (trade == null)
                return true;
            
            if (intention == TradeIntention.Buy)
            {
                if (trade.Type == TradeType.Buy && trade.Price.WithinDistance(price, Distance))
                    return false;
            }
            else
            {
                if (trade.Type == TradeType.Sell && trade.Price.WithinDistance(price, Distance))
                    return false;
            }

            return true;
        }
    }

}