using System;
using System.Collections.Generic;
using System.Text;
using AVS.CoreLib.Data;
using AVS.Trading.Core;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;

namespace AVS.Trading.Data.Domain.Market
{
    public class MarketTradeHistory : BaseEntity
    {
        #region c-tor
        public MarketTradeHistory()
        {
            Trades = new List<MarketTradeItem>();
        }

        public MarketTradeHistory(int tradesCount)
        {
            Trades = new List<MarketTradeItem>(tradesCount);
        } 
        #endregion

        public string Market { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TimeSpan Period => To - From;

        /// <summary>
        /// volume bought in base amount
        /// </summary>
        public double VolumeBought { get; set; }
        /// <summary>
        /// volume sold in base amount
        /// </summary>
        public double VolumeSold { get; set; }

        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public int Buys { get; set; }
        public int Sells { get; set; }

        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        /// <summary>
        /// Avg = Sum(quote * amount) / Sum(amount)
        /// </summary>
        public double AvgPrice { get; set; }

        public virtual ICollection<MarketTradeItem> Trades { get; set; }

        public string GetInfo()
        {
            var pair = CurrencyPair.Parse(Market);
            var baseCurrency = pair.BaseCurrency;
            var sb = new StringBuilder();
            sb.AppendLine($"{Market} [{From:g}-{To:t}] #{Trades.Count} trades");
            sb.AppendLine($" bought/sold - {VolumeBought:N1}{baseCurrency}\t /\t {VolumeSold:N1}{baseCurrency}");
            sb.AppendLine($" open/close - {OpenPrice}{baseCurrency}\t /\t {ClosePrice}{baseCurrency}");
            sb.AppendLine($" min/max - {MinPrice}{baseCurrency}\t /\t {MaxPrice}{baseCurrency} \t[AVG {AvgPrice:#.########}{baseCurrency}\t]");
            return sb.ToString();
        }

        public override string ToString()
        {
            var pair = CurrencyPair.Parse(Market);
            var baseCurrency = pair.BaseCurrency;
            return $"{Market} [{From:g}-{To:t}]  #{Trades.Count} trades; bought/sold - {VolumeBought:N}{baseCurrency} / {VolumeSold:N}{baseCurrency}";
        }
    }



    public interface IPrice
    {
        double Open { get; set; }
        double Close { get; set; }
        double Min { get; set; }
        double Max { get; set; }
    }
    public interface IVolume
    {
        double Bought { get; set; }
        double Sold { get; set; }
        double Buys { get; set; }
        double Sells { get; set; }
        //double AvgBuy { get; set; }
        //double AvgSell { get; set; }
    }

   
    public struct TradeStruct
    {
        public double Volume { get; set; }
        public double Total { get; set; }
        public int Count { get; set; }
        public double PriceAvg => Volume > 0 ? Total / Volume : 0;
        public double MaxAmount { get; set; }
        public DistributionStruct Distribution { get; set; }
    }

    public struct DistributionStruct
    {
        public double Top { get; set; }
        public double Middle { get; set; }
        public double Bottom { get; set; }
    }
}