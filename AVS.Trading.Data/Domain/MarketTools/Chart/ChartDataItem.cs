using System;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Data.Domain.Market
{
    public class ChartDataItem : BaseEntity
    {
        public DateTime TimeStampUtc { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double VolumeBase { get; set; }
        public double VolumeQuote { get; set; }
        public double WeightedAverage { get; set; }

        public int ChartDataId { get; set; }

        public bool IsGrowingCandle => Close > Open;
        public double Change => Close - Open;
        public double Diff => High - Low;

        public override string ToString()
        {
            return
                $"ChartDataItem=> OHLC: {Open.FormatNumber()} {High.FormatNumber()} {Low.FormatNumber()} {Close.FormatNumber()}";
        }
    }

    /*
    public class ChartDataItemEx : ChartDataItem
    {
        //additional info is taken from analysing market trade items
        /// <summary>
        /// sell trades volume in base currency
        /// </summary>
        public double SellTradesVolume { get; set; }
        public int SellTradesCount { get; set; }

        public double SellTradesAvg { get; set; }

        public double BuyTradesVolume { get; set; }
        public int BuyTradesCount { get; set; }
        public double BuyTradesAvg { get; set; }

        public double BiggestSellTrade { get; set; }
        public double BiggestBuyTrade { get; set; }

        /// <summary>
        /// sell orders volume in order book at the beggining of the given timeframe
        /// limited by low and high price within given timeframe
        /// in base currency
        /// </summary>
        public double SellOrdersVolume { get; set; }
        public int SellOrdersCount { get; set; }

        public double BuyOrdersVolume { get; set; }
        public int BuyOrdersCount { get; set; }

        public double BiggestSellOrder { get; set; }
        public double BiggestBuyOrder { get; set; }
    }
    */
}