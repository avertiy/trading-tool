using System;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Data.Domain.TradingTools
{
    public class OpenOrder : BaseEntity
    {
        public string Pair { get; set; }
        public string Exchange { get; set; }
        /// <summary>
        /// Order number on the exchange (Poloniex etc.)
        /// </summary>
        public string OrderNumber { get; set; }
        public double AmountQuote { get; set; }

        /// <summary>
        /// when order is partially executed, the amount might be changed
        /// e.g. Initial Amount 1 LTC, than 0.5 LTC have been executed so the order amount is updated to 0.5 LTC 
        /// </summary>
        public double InitialAmount { get; set; }

        public double AmountBase { get; set; }
        public OrderType Type { get; set; }
        public TradingAccount Account { get; set; }

        public double Price { get; set; }

        /// <summary>
        /// Take profit price 
        /// можно задать фиксированную цену после которой зафиксировать профит
        /// если фиксированная цена take profit не задана, 
        /// то используется глобальная настройка Margin
        /// в % от цены ордера
        /// </summary>
        public double? TakeProfit { get; set; }

        /// <summary>
        /// StopLoss price
        /// можно задать фиксированную цену для стоп лосса 
        /// если фиксированный стоплосс не задан, то используется глобальная настройка 
        /// стоп лосс в % от цены ордера
        /// </summary>
        public double? StopLoss { get; set; }

        public OrderState State { get; set; }
        public OrderCondition Condition { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        /// <summary>
        /// date when order has been executed (close)
        /// </summary>
        public DateTime? DateUtc { get; set; }
        

        public override string ToString()
        {
            return $"{Pair} {Account} {Type} {State}=> {AmountQuote} x {Price} = {AmountBase} {DateUtc:g}";
        }
    }

    public static class OrderExtensions
    {
        public static bool SamePrice(this OpenOrder order, double price, double dist)
        {
            return price.Distance(order.Price) <= dist;
        }

        public static bool SameBuyPrice(this OpenOrder order, double price, double dist)
        {
            return order.Type== OrderType.Buy && price.Distance(order.Price) <= dist;
        }

        public static bool SameSellPrice(this OpenOrder order, double price, double dist)
        {
            return order.Type == OrderType.Buy && price.Distance(order.Price) <= dist;
        }
    }

}

