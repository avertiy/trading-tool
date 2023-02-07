using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Framework.Extensions;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Data.Domain.MarketTools;

namespace AVS.Trading.Framework.Utils
{
    internal class OrderBookHelper
    {
        private readonly List<BuyOrder> _buyOrders = new List<BuyOrder>();
        private readonly List<SellOrder> _sellOrders = new List<SellOrder>();

        #region settings
        public double? OrderTotalThreshold;
        public bool DynamicThreshold { get; set; } 
        #endregion

        #region counters
        protected double TotalBuys { get; set; }
        protected double TotalBoughtAmount { get; set; }
        protected double TotalSells { get; set; }
        protected double TotalSoldAmount { get; set; }
        /// <summary>
        /// all buy orders count (we filter out small orders so the _buyOrders.Count does not tell us the real count)
        /// </summary>
        protected int BuyOrdersCount { get; set; }
        protected int SellOrdersCount { get; set; } 
        #endregion

        public void Initialize(IPublicOrderBook orderBook, double koef)
        {
            //cut orders out of the market price +-60% 
            koef = 1.0 + koef;
            var thresholdSellPrice = orderBook.SellOrders[0].Price * koef;
            var thresholdBuyPrice = orderBook.BuyOrders[0].Price / koef;

            if (DynamicThreshold)
            {
                OrderTotalThreshold = orderBook.BuyOrders.Where(o => o.Price >= thresholdBuyPrice)
                    .Average(o => o.AmountBase)/10;
            }

            foreach (IOrder order in orderBook.BuyOrders)
            {
                if (order.Price < thresholdBuyPrice)
                    break;
                
                this.AddBuyOrder(new BuyOrder()
                {
                    Price = order.Price,
                    AmountBase = order.AmountBase,
                    AmountQuote = order.AmountQuote
                });
            }

            foreach (IOrder order in orderBook.SellOrders)
            {
                if (order.Price > thresholdSellPrice)
                    break;

                this.AddSellOrder(new SellOrder()
                {
                    Price = order.Price,
                    AmountBase = order.AmountBase,
                    AmountQuote = order.AmountQuote
                });
            }
        }
        
        public OrderBook CreateOrderBook(string market)
        {
            CalcRanks();
            BuyOrder support = GetFirstOrDefaultByRank(_buyOrders, 4)?? new BuyOrder();
            SellOrder resistance = GetFirstOrDefaultByRank(_sellOrders, 4)?? new SellOrder();

            var book = new OrderBook()
            {
                Pair = market,
                TimeStampUtc = DateTime.UtcNow,
                BuyOrders = _buyOrders,
                SellOrders = _sellOrders,
                TotalBuys = TotalBuys.Normalize(),
                TotalSells = TotalSells.Normalize(),
                BuyOrdersCount = BuyOrdersCount,
                SellOrdersCount = SellOrdersCount,
                SupportingWall = new Wall(){
                    Price = support.Price,
                    AmountQuote = support.AmountQuote,
                    AmountBase = support.AmountBase,
                    Sum =  support.Sum},
                ResistanceWall = new Wall()
                {
                    Price = resistance.Price,
                    AmountQuote = resistance.AmountQuote,
                    AmountBase = resistance.AmountBase,
                    Sum = resistance.Sum
                }
            };
            return book;
        }
        
        protected void AddBuyOrder(BuyOrder order)
        {
            TotalBuys += order.AmountBase;
            TotalBoughtAmount += order.AmountQuote;
            BuyOrdersCount++;
            order.Sum = TotalBuys.Round(2);
            order.SumQuoteAmount = TotalBoughtAmount.Round(2);

            if (OrderTotalThreshold.HasValue && order.AmountBase < OrderTotalThreshold.Value)
                return;

            _buyOrders.Add(order);
        }

        protected void AddSellOrder(SellOrder order)
        {
            TotalSells += order.AmountBase;
            TotalSoldAmount += order.AmountQuote;
            SellOrdersCount++;
            order.Sum = TotalSells.Round(2);
            order.SumQuoteAmount = TotalSoldAmount.Round(2);

            if (OrderTotalThreshold.HasValue && order.AmountBase < OrderTotalThreshold.Value)
                return;
            _sellOrders.Add(order);
        }

        protected void CalcRanks()
        {
            if (_buyOrders.Count > 0)
            {
                _buyOrders.CalcRank(o => o.AmountBase, (o, rank) => { o.Rank = rank; });
            }

            if (_sellOrders.Count > 0)
            {
                _sellOrders.CalcRank(o => o.AmountBase, (o, rank) =>{o.Rank = rank;});
            }
        }

        protected T GetFirstOrDefaultByRank<T>(List<T> list, int rank) where T : OrderBookItem
        {
            T res = null;
            do
            {
                res = list.FirstOrDefault(o => o.Rank >= rank);
                rank--;
            } while (res == null && rank > 0);
            return res;
        }
    }
}
