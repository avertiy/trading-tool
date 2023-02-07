using System;
using System.Collections.Generic;
using System.Text;
using AVS.CoreLib.Data;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Data.Domain.Market;

namespace AVS.Trading.Data.Domain.MarketTools.Chart
{
    public class Chart : BaseEntity
    {
        public Chart()
        {
            Candlesticks = new List<ChartDataItem>();
        }

        public Chart(int count)
        {
            Candlesticks = new List<ChartDataItem>(count);
        }

        public string Pair { get; set; }
        public MarketPeriod Period { get; set; }

        public DateTime CreatedUtc { get; set; }
        public DateTime LastUpdateUtc { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public double Open { get; set; }
        public double Close { get; set; }
        public double Change { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double VolumeBase { get; set; }
        public double VolumeQuote { get; set; }
        /// <summary>
        /// long candle is a candle with a diff (high - low) greater than threshold
        /// </summary>
        public int LongCandles { get; set; }

        public virtual ICollection<ChartDataItem> Candlesticks { get; set; }
        
        public override string ToString()
        {
            return $"{Pair} [{From:g}-{To:t}] open/close: {Open.FormatNumber()}/{Close.FormatNumber()} volume {VolumeBase.FormatNumber()}";
        }
    }


    public static class ChartDataExtensions
    {
        public static string GetInfo(this Chart data, CurrencyPair pair)
        {
            var market = pair.ToMarketString();
            var baseCurrency = pair.BaseCurrency;
            var quoteCurrency = pair.BaseCurrency;
            var sb = new StringBuilder();
            sb.AppendLine($"{market} [{data.From:g}-{data.To:t}] #{data.Candlesticks.Count} candles");
            sb.AppendLine($" volume - {data.VolumeQuote:N1}{quoteCurrency}\t /\t {data.VolumeBase:N1}{baseCurrency}");
            sb.AppendLine($" open/close - {data.Open}{baseCurrency}\t /\t {data.Close}{baseCurrency}");
            sb.AppendLine($" low/high - {data.Low}{baseCurrency}\t /\t {data.High}{baseCurrency}");
            return sb.ToString();
        }
    }
}