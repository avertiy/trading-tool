using AVS.Trading.Engine.Emulator.Context;

namespace AVS.Trading.Engine.Emulator.Services
{
    public interface IPriceService
    {
        double GetBestPrice(AlgorithmContext ctx, TradeIntention intention);
    }

    public class PriceService : IPriceService
    {
        public double GetBestPrice(AlgorithmContext ctx, TradeIntention intention)
        {
            if(intention == TradeIntention.Buy)
                return ctx.Market.Ticker.HighestBid;
            return ctx.Market.Ticker.LowestAsk;
        }
    }
}