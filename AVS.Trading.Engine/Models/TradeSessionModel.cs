using AVS.Trading.Core;
using AVS.Trading.Engine.Models;

namespace AVS.Trading.Pipeline.Models
{
    public class TradeSessionModel
    {
        public string Exchange { get; set; }
        public string Pair { get; set; }

        public string Algorithm { get; set; }
        public double AmountQuote { get; set; } 
        public double AvailableAmountQuote { get; set; }
        public double AmountBase { get; set; }
        public double AvailableAmountBase { get; set; }
        
        public TradingRange Range { get; set; }

        /// <summary>
        /// amount который нужно откупить/скинуть если цена вышла из торгового диапозона
        /// </summary>
        public double RecoverAmount { get; set; }

        ////for margin trading put it into separate class
        //public double LoanRate { get; set; }
        //public double MaxLoanRate { get; set; }
        //public OrderCategory Category { get; set; }
    }
    
    //public class DoTrade
    //{
    //    public OrderType Type { get; set; }
    //    public double Price { get; set; }
    //    public double AmountQuote { get; set; }
    //    public double AmountBase { get; set; }
    //}
}
