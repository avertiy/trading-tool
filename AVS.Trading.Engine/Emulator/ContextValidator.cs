using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Pipeline.TradingAlgorithms.Context;

namespace AVS.Trading.Engine.Emulator
{
    class ContextValidator
    {
        private readonly ContextSettings _settings;

        public ContextValidator(ContextSettings settings)
        {
            _settings = settings;
        }

        public void Validate(AlgorithmContext ctx)
        {
            if (ctx == null)
                throw new AlgorithmContextException("Context is null");

            //validate market context
            if (_settings.MarketPrice)
            {
                if (ctx.Market == null)
                    throw new AlgorithmContextException("MarketContext is null");
                if (ctx.Market.Ticker == null)
                    throw new AlgorithmContextException("Ticker is null");
            }

            //if (ctx.Session == null)
            //    throw new AlgorithmContextException("TradeSesionContext is null");

            ////validate trading context
            //if (ctx.Session.Positions == null)
            //    throw new AlgorithmContextException("Positions map is null");

            //if (ctx.Session.OpenedOrders == null)
            //    throw new AlgorithmContextException("TradingContext.OpenOrders is null");
            //if (ctx.Session.Trades == null)
            //    throw new AlgorithmContextException("TradingContext.Trades is null");


        }
    }

    


}