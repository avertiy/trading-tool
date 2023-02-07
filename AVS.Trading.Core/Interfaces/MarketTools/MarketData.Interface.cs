namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IMarketData
    {
        /// <summary>
        /// last price
        /// </summary>
        double PriceLast { get; }
        double PriceChange { get; }

        double Volume24HourBase { get; }
        //double Volume24HourQuote { get; }

        /// <summary>
        /// highestBid
        /// </summary>
        double HighestBid { get; }
        /// <summary>
        /// lowestAsk
        /// </summary>
        double LowestAsk { get; }
        
        //double OrderSpread { get; }
        //double OrderSpreadPercentage { get; }
    }



}
