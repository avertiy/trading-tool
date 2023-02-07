using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.ResponseModels.MarketTools
{
    public class OrderBook : IPublicOrderBook
    {
        public string Pair { get; set; }
        public IList<IOrder> BuyOrders { get; set; } = new List<IOrder>();
        public IList<IOrder> SellOrders { get; set; } = new List<IOrder>();

        public class Order : IOrder
        {
            public Order(double price, double amountQuote)
            {
                Price = price;
                AmountQuote = amountQuote;
                AmountBase = amountQuote / price;
            }

            public double Price { get; set; }
            public double AmountQuote { get; set; }
            public double AmountBase { get; set; }

            /// <summary>
            /// кол-во заявок 
            /// </summary>
            public int Count { get; set; }
        }

        public void AddBuyOrder(Order order)
        {
            BuyOrders.Add(order);
        }

        public void AddSellOrder(Order order)
        {
            SellOrders.Add(order);
        }

        public void AddBuyOrder(double price, double quantity)
        {
            BuyOrders.Add(new Order(price, quantity));
        }

        public void AddSellOrder(double price, double quantity)
        {
            SellOrders.Add(new Order(price, quantity));
        }

        public override string ToString()
        {
            return
                $"OrderBook {Pair} [#{BuyOrders.Count} - {BuyOrders.Sum(b => b.AmountBase).FormatAsAmount()}; #{SellOrders.Count} - {SellOrders.Sum(s => s.AmountBase).FormatAsAmount()}";
        }
    }
}