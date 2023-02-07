using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data;

namespace AVS.Trading.Data.Domain.Trading
{
    /*/// <summary>
    /// not entity
    /// </summary>
    public class MyOrderBook : BaseEntity
    {
        public string Market => Orders.FirstOrDefault()?.Market;
        public IList<OpenOrder> Orders { get; set; }
        public IList<OpenOrder> BuyOrders => Orders.Where(o => o.Type == OrderType.Buy).ToList();
        public IList<OpenOrder> SellOrders => Orders.Where(o => o.Type == OrderType.Sell).ToList();
    }

    public class OrderBookTotals
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public double LendingFees { get; set; }
        public double SettlementTotal { get; set; }
        public double AvgBuyVolume { get; set; }
        public double Sells { get; set; }

        public double MarginBuys { get; set; }
        public double MarginSells { get; set; }

        public double MarginProfitLoss => MarginSells - MarginBuys - LendingFees - SettlementTotal;
        public double ExchangeProfitLoss => Sells - AvgBuyVolume;
        public double ProfitLoss => ExchangeProfitLoss + MarginProfitLoss;
        
        public double AvgSellPrice { get; set; }
        public double AvgBuyPrice { get; set; }

        public IList<OpenOrder> LastMarginTrades { get; set; }
        public IList<OpenOrder> LastExchangeTrades { get; set; }
    }*/

}