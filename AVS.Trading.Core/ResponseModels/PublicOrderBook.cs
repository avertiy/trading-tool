using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.ResponseModels
{
    public class PublicOrderBook : IPublicOrderBook
    {
        public string Pair { get; set; }
        public IList<IOrder> BuyOrders { get; set; }
        public IList<IOrder> SellOrders { get; set; }
    }

    
}