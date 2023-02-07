using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Data.Domain.TradingTools
{
    public class TradeSession : BaseEntity
    {
        public string Exchange { get; set; }
        public string Pair { get; set; }
        public string Algorithm { get; set; }
        
        public DateTime Opened { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime? Closed { get; set; }

        //transaction amount
        public double Amount { get; set; }
        public string BalanceSheetJson { get; set; }

        private BalanceSheet _balanceSheet;
        [NotMapped]
        public BalanceSheet BalanceSheet
        {
            get => _balanceSheet ?? (_balanceSheet = BalanceSheet.FromJson(BalanceSheetJson));
            set
            {
                _balanceSheet = value;
                BalanceSheetJson = value?.SerializeToJson();
            }
        }

        /// <summary>
        /// coma-separated order numbers
        /// </summary>
        public string OpenOrders { get; set; }

        

        public List<string> GetOrderNumbers()
        {
            var ids = new List<string>();
            if(!string.IsNullOrEmpty(OpenOrders))
                ids.AddRange(OpenOrders.Split(','));
            return ids;
        }

        public void SaveOrderNumbers(IEnumerable<string> ids)
        {
            OpenOrders = string.Join(",", ids);
        }


        ////Trading Range
        //public double PriceMin { get; set; }
        //public double PriceMax { get; set; }
        //public double BuyRange_PriceMin { get; set; }
        //public double BuyRange_PriceMax { get; set; }
        //public double SellRange_PriceMin { get; set; }
        //public double SellRange_PriceMax { get; set; }


        

        //it should be trades and from trades we can build position map
        //public virtual ICollection<PositionEntity> Positions { get; set; }

        //public TradingRange GetTradingRange()
        //{
        //    return new TradingRange(PriceMin, PriceMax, BuyRange_PriceMin, BuyRange_PriceMax, SellRange_PriceMin, SellRange_PriceMax);
        //}

        //public void SetTradingRange(TradingRange range)
        //{
        //    PriceMin = range.Min;
        //    PriceMax = range.Max;
        //    BuyRange_PriceMin = range.BuyRange.Min;
        //    BuyRange_PriceMax = range.BuyRange.Max;
        //    SellRange_PriceMin = range.SellRange.Min;
        //    SellRange_PriceMax = range.SellRange.Max;
        //}

        public override string ToString()
        {
            return $"{Exchange} {Pair} {Algorithm}";
        }
    }


    public class BalanceItem: BaseEntity
    {
        public int SessionId { get; set; }
        public string Currency { get; set; }
        public double Balance { get; set; }
        public DateTime DateUtc { get; set; }
        public string Details { get; set; }
    }
}
