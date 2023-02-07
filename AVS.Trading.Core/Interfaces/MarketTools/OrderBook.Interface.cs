using System.Collections.Generic;

namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IPublicOrderBook
    {
        string Pair { get; set; }
        /// <summary>
        /// bids
        /// </summary>
        IList<IOrder> BuyOrders { get; }
        /// <summary>
        /// asks
        /// </summary>
        IList<IOrder> SellOrders { get; }
    }
}
