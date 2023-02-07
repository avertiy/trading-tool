using System;

namespace AVS.Trading.Core.Models
{
    public class DateRange
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateRange()
        {
        }

        public DateRange(DateTime @from, DateTime to)
        {
            From = @from;
            To = to;
        }
    }
}