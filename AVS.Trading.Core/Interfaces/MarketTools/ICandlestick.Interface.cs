using System;
using AVS.Trading.Core.Formatters;

namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IOhlc
    {
        double Open { get; }
        double Close { get; }

        double High { get; }
        double Low { get; }
    }

    public interface ICandlestick: IOhlc
    {
        DateTime Time { get; }

        double VolumeBase { get; }
        double VolumeQuote { get; }

        double WeightedAverage { get; }

        bool HasData { get; }
    }

    public static class OhlcExtensions
    {
        /// <summary>
        /// candle length in % formula: (High - Low)/Low
        /// </summary>
        public static double GetLength(this IOhlc candle)
        {
            return Math.Round((candle.High - candle.Low) / candle.Low*100,2);
        }

        public static double GetMediana(this IOhlc candle)
        {
            return Math.Round((candle.High + candle.Low) / 2, 2);
        }

        public static double GetBodyLength(this IOhlc candle)
        {
            return Math.Round((candle.Open > candle.Close
                ? (candle.Open - candle.Close) / candle.Close
                : (candle.Close - candle.Open) / candle.Open)*100, 2);
        }

        public static bool HasLongShadow(this IOhlc candle)
        {
            return candle.GetLength() / candle.GetBodyLength() > 2;
        }

        public static bool IsGrowing(this IOhlc candle)
        {
            return candle.Open < candle.Close;
        }

        public static bool Contains(this IOhlc candle, double price)
        {
            return candle.Low < price && price < candle.High;
        }

        public static string Format(this IOhlc ohlc, string format)
        {
            return OhlcFormatter.Format(format, ohlc);
        }
    }
}
