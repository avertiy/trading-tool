using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Engine.Emulator.Decisions;
using AVS.Trading.Engine.Emulator.DecisionStrategies;
using AVS.Trading.Pipeline.TradingAlgorithms.Context;

namespace AVS.Trading.Engine.Emulator.Algorithms
{
    public interface IAlgorithm
    {
        ContextEnum ContextSetup { get; }
        IDecision Execute(AlgorithmContext ctx);
    }

    public abstract class AlgorithmBase: IAlgorithm
    {
        private readonly IDecisionStrategy _strategy;
        //private ContextValidator _validator;

        protected AlgorithmBase(IDecisionStrategy strategy)
        {
            _strategy = strategy;
        }

        public virtual ContextEnum ContextSetup => ContextEnum.Default;

        public virtual IDecision Execute(AlgorithmContext ctx)
        {
            ctx.Validate(ContextSetup);

            IDecision decision = _strategy.MakeDecision(ctx);

            return decision;
        }
    }


    /*      
      map:: longs 300|2611  shorts 200|2710   => LONG(1)
      last trades:: buy 2604 2605 2624; sell 2715,2705;      => next action -> sell(from:2728) or buy (when <= 2636)
      open orders:: buy 2611 2605 [no buy actions required]; sell 2860 [sell order required]      
      ticker 2716 => do nothing or place buy order (suppose sell order already  posted at 2730)
      ticker=>MatchRange(buyStrategy, sellStrategy);     
     */
    public class SimpleAlgorithm : AlgorithmBase
    {
        public SimpleAlgorithm(SimpleDecisionStrategy strategy) : base(strategy)
        {
        }

        public override ContextEnum ContextSetup => 
            ContextEnum.Ticker | 
            ContextEnum.OpenOrders | 
            ContextEnum.Balance |
            ContextEnum.Trades |
            ContextEnum.Chart;
    }
}