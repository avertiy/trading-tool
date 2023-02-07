using System;
using System.Linq;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Engine.Models
{
    class PriceRangeSet : PriceRange
    {
        public PriceRange[] Ranges;
        public int Position { get; protected set; }
        public PriceRange Current => Position >= 0 ? Ranges[Position] : null;
        public PriceRange Next => Position >=0 && Position < Ranges.Length-1? Ranges[Position+1] : null;
        public PriceRange Prev => Position > 0 ? Ranges[Position-1] : null;

        public PriceRangeSet(double min, double max, double step = 0.5) : base(min, max)
        {
            int n = (int)(GetLength() / step);
            if (n > 2000)
                throw new ArgumentException($"step is too small for range {this}");
            Position = -1;
            Ranges = this.Split(step);
        }

        public PriceRange GetRange(double price)
        {
            return Ranges.FirstOrDefault(r => r.Match(price));
        }

        public int Seek(double price)
        {
            ValidatePrice(price);
            var index = Array.FindIndex(Ranges, r => r.Match(price));
            if (index >= 0)
                Position = index;
            return index;
        }
        
        protected void ValidatePrice(double price)
        {
            if(!Match(price))
                throw new ApplicationException($"PriceOutOfRange {price.FormatAsPrice()} is out of range {this}");
        }
    }
}