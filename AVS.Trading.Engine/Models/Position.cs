using System;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Engine.Models
{
    public class Position
    {
        //--data fields
        public double Amount;
        public double Price;
        public double Total;
        public double Fees;
        public DateTime Timestamp;
        public PositionType Type;
        public TradingAccount Account;

        //--system fields
        public string TradeRef;
        public string CreatedBy;
    }
}