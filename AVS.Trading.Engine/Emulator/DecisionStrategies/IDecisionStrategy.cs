using System;
using AVS.Trading.Core.Enums;
using AVS.Trading.Engine.Emulator.Conditions;
using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Engine.Emulator.Decisions;
using AVS.Trading.Engine.Emulator.Services;

namespace AVS.Trading.Engine.Emulator.DecisionStrategies
{
    //SRP - responsible for making a decision - post order [buy/sell] or do nothing etc.
    public interface IDecisionStrategy
    {
        IDecision MakeDecision(AlgorithmContext ctx);
    }

    public abstract class DecisionStrategy: IDecisionStrategy
    {
        private ICondition _condition;
        private readonly IPriceService _priceService;

        protected DecisionStrategy(IPriceService priceService)
        {
            _priceService = priceService;
        }

        protected ICondition Condition
        {
            get
            {
                if (_condition == null)
                {
                    Setup();
                    if(_condition == null)
                        throw new Exception("Invalid condition setup");
                }

                return _condition;
            }
            set => _condition = value;
        }

        protected abstract void Setup();

        public virtual IDecision MakeDecision(AlgorithmContext ctx)
        {
            var canBuy = Condition.Test(ctx, TradeIntention.Buy);
            var canSell = Condition.Test(ctx, TradeIntention.Sell);

            if(canBuy && canSell)
                throw new Exception("Invalid condition setup: buy and sell options must be mutually exclusive");

            if (canBuy)
                return PostOrder(ctx, TradeIntention.Buy);

            if (canSell)
                return PostOrder(ctx, TradeIntention.Sell);

            return new DoNothing();
        }

        protected virtual IDecision PostOrder(AlgorithmContext ctx, TradeIntention intention)
        {
            return new PostOrderDecision(
                intention == TradeIntention.Buy ? OrderType.Buy : OrderType.Sell,
                _priceService.GetBestPrice(ctx, intention),
                ctx.Amount,
                ctx.Pair.ToString());
        }
    }

    public class SimpleDecisionStrategy : DecisionStrategy
    {
        public SimpleDecisionStrategy(IPriceService priceService) : base(priceService)
        {
        }

        protected override void Setup()
        {
            var condition = new CompositeCondition();
            condition.Add(new BalanceCondition());
            condition.Add(new TickerAvgLowPriceCondition());
            condition.Add(new NoSimilarOpenOrderCondition());
            condition.Add(new NotLastTradeCondition());
            Condition = condition;
        }
    }
}