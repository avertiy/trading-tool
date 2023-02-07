using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Engine.Models
{
    public class PositionMap
    {
        public string Pair { get; set; }
        public PositionSet Shorts { get; protected set; }
        public PositionSet Longs { get; protected set; }

        #region calculated properties
        public PositionType Type
        {
            get
            {
                var amount = Amount;
                return amount >= 0 ? PositionType.Long : PositionType.Short;
            }
        }
        public double Fees => Shorts.Fees + Longs.Fees;
        public double Amount => Longs.Amount - Shorts.Amount;
        public double Total => Shorts.Total - Longs.Total;
        public bool Any => Shorts.Any() || Longs.Any();
        #endregion

        public PositionMap()
        {
            Shorts = new PositionSet(PositionType.Short);
            Longs = new PositionSet(PositionType.Long);
        }

        public void Add(Position position)
        {
            if (position.Type == PositionType.Short)
                Shorts.Add(position);
            else
                Longs.Add(position);
        }

        public void Add(IEnumerable<Position> items)
        {
            foreach (Position position in items)
            {
                Add(position);
            }
        }

        #region auxilary methods
        public double CalculateProfitLoss(double marketPrice)
        {
            return Total + Amount * marketPrice;
        }

        public ProfitLossStruct GetProfitLossSummary()
        {
            var summary = new ProfitLossStruct();
            var longs = Longs.Amount;
            var shorts = Shorts.Amount;

            summary.Rest = longs - shorts;
            if (longs > shorts)
            {
                summary.Amount = shorts;
                summary.ProfitLoss = Shorts.Total - Longs.AvgPrice * shorts;
                summary.RestPrice = Longs.AvgPrice;
            }
            else
            {
                summary.Amount = longs;
                summary.ProfitLoss = Shorts.AvgPrice * longs - Longs.Total;
                summary.RestPrice = Shorts.AvgPrice;
            }

            return summary;
        }

        public override string ToString()
        {
            return $"short: {Shorts.Amount}; long: {Longs.Amount}";
        } 
        #endregion
    }

    public class PositionSet : IEnumerable<Position>
    {
        protected List<Position> Items { get; set; }
        public PositionType Type { get; protected set; }

        public PositionSet(PositionType positionType)
        {
            Type = positionType;
            Items = new List<Position>();
        }
        

        public int Length => Items.Count;
        public double Fees => Items.Sum(i => i.Total * i.Fees);
        public double Amount => Items.Sum(i=>i.Amount);
        public double Total => Items.Sum(i=>i.Total);
        public double AvgPrice => Total / Amount;
        public DateTime? LastTradeTime => Items.LastOrDefault()?.Timestamp;

        public void Add(Position item)
        {
            ValidatePostion(item);
            Items.Add(item);
        }

        protected void ValidatePostion(Position position)
        {
            if (Type != position.Type)
                throw new ArgumentException("PositionType mismatch");

            //if (Items.Any() && (Items[0].TradingType != position.TradingType))
            //    throw new ArgumentException("TradingType type mismatch");

            if (string.IsNullOrEmpty(position.TradeRef))
                throw new ArgumentException("TradeNumberRef is missing");
        }

        #region IEnumerable
        public IEnumerator<Position> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        } 
        #endregion

        public void AddRange(Position[] items)
        {
            if(items.Any(i=>i.Type != Type))
                throw new ArgumentException("PositionType mismatch");
            Items.AddRange(items);
        }
    }

    /*
    public class PositionSet: IEnumerable<Position>
    {
        //protected List<Position> Closed { get; set; } = new List<Position>();
        protected List<Position> Items { get; set; } = new List<Position>();
        public string Pair { get; set; }

        public double Amount
        {
            get
            {
                double amount = 0.0;
                foreach (var position in Items)
                {
                    if (position.Type == PositionType.Long)
                        amount += position.Amount;
                    else
                        amount -= position.Amount;
                }
                return amount;
            }
        }
        public double Total
        {
            get
            {
                double total = 0.0;
                foreach (var position in Items)
                {
                    if (position.Type == PositionType.Long)
                        total -= position.Total;
                    else
                        total += position.Total;
                }
                return total;
            }
        }
        public PositionType Type
        {
            get
            {
                var amount = Amount;
                return amount >= 0 ? PositionType.Long : PositionType.Short;
            }
        }

        public void Add(Position item)
        {
            Items.Add(item);
        }

        #region IEnumerable
        public IEnumerator<Position> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion

        public double CalculateProfitLoss(double marketPrice)
        {
            return Amount * marketPrice + Total;
        }

        public override string ToString()
        {
            var amount = Amount;
            var type = amount >= 0 ? PositionType.Long : PositionType.Short;
            return $"{type} {amount.Abs().FormatAsQuantity()} {Pair}";
        }
    }*/
}