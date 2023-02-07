using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AVS.CoreLib.Data;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Data.Domain.MarketTools
{
    public class OrderBook : BaseEntity
    {
        private ICollection<BuyOrder> _buyOrders;
        private ICollection<SellOrder> _sellOrders;

        public OrderBook()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
            SupportingWall = new Wall();
            ResistanceWall = new Wall();
        }

        public string Pair { get; set; }
        public DateTime TimeStampUtc { get; set; }

        public Wall SupportingWall { get; set; }
        public Wall ResistanceWall { get; set; }
        
        public double TotalBuys { get; set; }
        public double TotalSells { get; set; }

        public int BuyOrdersCount { get; set; }
        public int SellOrdersCount { get; set; }

        public virtual ICollection<BuyOrder> BuyOrders
        {
            get => _buyOrders;
            set => _buyOrders = value;
        }

        public virtual ICollection<SellOrder> SellOrders
        {
            get => _sellOrders;
            set => _sellOrders = value;
        }
        
        public override string ToString()
        {
            var pair = CurrencyPair.Parse(Pair);
            return $"{Pair.PadRight(8)}\t buys /sells {TotalBuys:N1}{pair.BaseCurrency} / {TotalSells:N1}{pair.BaseCurrency}";
        }

        public bool IsEquivalentTo(OrderBook book)
        {
            if (book == null)
                return false;

            bool sameSupWall = book.SupportingWall.IsEquivalentTo(SupportingWall);
            bool sameResWall = book.ResistanceWall.IsEquivalentTo(ResistanceWall);

            var sameBuyCount = book.BuyOrdersCount.WithinRange(BuyOrdersCount-15, BuyOrdersCount + 15);
            var sameSellCount = book.SellOrdersCount.WithinRange(SellOrdersCount- 15, SellOrdersCount + 15);

            if (sameResWall && sameSupWall && sameBuyCount && sameSellCount)
            {
                return true;
            }

            return false;
        }

    }

    [ComplexType]
    public class Wall
    {
        public double Price { get; set; }
        public double AmountBase { get; set; }
        [NotMapped]
        public double AmountQuote { get; set; }
        /// <summary>
        /// in BaseCurrency
        /// </summary>
        public double Sum { get; set; }

        public bool IsEquivalentTo(Wall wall)
        {
            var tolerance = 0.00000001;
            var res = Price.Eq(wall.Price, tolerance);

            var k = 0.02; //2%          
            res = res && AmountBase.Eq(wall.AmountBase, AmountBase * k);

            k = 0.25;//25%
            res = res && (Sum).Eq(wall.Sum, Sum * k);
            return res;
        }
    }

    public abstract class OrderBookItem : BaseEntity
    {
        public double Price { get; set; }
        public double AmountQuote { get; set; }
        public double AmountBase { get; set; }
        public int OrderBookId { get; set; }
        
        public double Sum { get; set; }
        [NotMapped]
        public double SumQuoteAmount { get; set; }
        /// <summary>
        /// Rank indicates how big the order relatively to other orders
        /// </summary>
        public int Rank { get; set; }

        //public DateTime CreatedOnUtc { get; set; }
        //public DateTime? DeletedOnUtc { get; set; }
        //public OrderType Type { get; set; }
        //public virtual OrderBook OrderBook { get; set; }

        public override string ToString()
        {
            return $"{Price}x{AmountQuote.FormatNumber()}={AmountBase.FormatNumber()}  [rank:{Rank}]";
        }
    }

    public class BuyOrder : OrderBookItem
    {   
    }

    public class SellOrder : OrderBookItem
    {
    }


    public static class OrderBookExtensions
    {
        public static string GetInfo(this OrderBook book, CurrencyPair pair)
        {
            var sb = new StringBuilder();
            var wall = book.SupportingWall;
            sb.AppendLine($" -bids: \tSupporting wall: {wall.Price} {wall.AmountBase:N1}{pair.BaseCurrency} => total volume: {wall.Sum:N1}{pair.BaseCurrency}");
            wall = book.ResistanceWall;
            sb.AppendLine($" -asks: \tResistance wall: {wall.Price} {wall.AmountBase:N1}{pair.BaseCurrency} => total volume: {wall.Sum:N1}{pair.BaseCurrency}");
            return sb.ToString();
        }
    }
}