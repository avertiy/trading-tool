using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Framework.Services.MarketTools;

namespace AVS.Trading.Framework.Utils
{
    public class ChartSummaryModelBuilder
    {
        public void Build(IList<ICandlestick> candles, MarketPeriod scale)
        {
            var summary = new ChartSummaryModel();
            summary.Initialize(candles);
            
        }
    }

    public class ChartSummaryModel
    {
        public VolumeInfo Volume;
        public PriceInfo Price;

        public int Count { get; private set; }
        private double TotalCost { get; set; }

        public void Initialize(IList<ICandlestick> candles)
        {
            if (candles == null)
                throw new ArgumentNullException(nameof(candles));
            if (candles.Count == 0)
                return;

            Price.Open = candles.First().Open;
            Price.Open = candles.Last().Close;

            double changeSum = 0;

            for (var index = 0; index < candles.Count; index++)
            {
                var candle = candles[index];
                if (index > 0)
                {
                    var change = (candle.Close - candles[index - 1].Close) / candle.Close;
                    if (change < 0)
                        change *= -1;
                    changeSum += change;
                }

                Process(candle);
                Count++;
            }

            Volume.Avg = Volume.Total / Count;
            Price.AvgChange = changeSum / Count;
            Price.Avg = Volume.Total / TotalCost;
        }

        private void Process(ICandlestick candle)
        {
            TotalCost += candle.VolumeBase;
            if (Price.Low < 0.00000001 || candle.Low < Price.Low)
                Price.Low = candle.Low;
            if (candle.High > Price.High)
                Price.High = candle.High;

            //volume info
            Volume.Total += candle.VolumeQuote;
            if (Volume.Min < 0.00000001 || candle.VolumeQuote < Volume.Min)
                Volume.Min = candle.VolumeQuote;
            if (candle.VolumeQuote > Volume.Max)
                Volume.Max = candle.VolumeQuote;
        }
    }

    public struct VolumeInfo
    {
        public double Min;
        public double Max;
        public double Total;
        public double Avg;
        
        public override string ToString()
        {
            return $"total: {Total.FormatAsQuantity()};avg: {Avg.FormatAsQuantity()};";
        }
    }

    public struct PriceInfo
    {
        public double Open;
        public double High;
        public double Low;
        public double Close;
        public double Avg;

        public double AvgChange;

        public override string ToString()
        {
            return $"avg: {Avg.FormatAsPrice()}";
        }
    }
    
    public class ChartDataSummary
    {
        public DateRange Period { get; protected set; }
        public MarketPeriod TimeScale { get;protected set; }
        public int Count { get; protected set; }

        public double Low { get; protected set; }
        public double High { get; protected set; }

        public CandleSet Set { get; protected set; }

        public void Initialize(IList<ICandlestick> candles)
        {
            Set = new CandleSet();
            Set.AddRange(candles);
            //double Open { get; }
            //double Close { get; }
            //double High { get; }
            //double Low { get; }
            //double VolumeBase { get; }
            //double VolumeQuote { get; }
        }

    }

    public class CandleCollection
    {
        private readonly List<ICandlestick> _items = new List<ICandlestick>();
        public double Length;
        public int Count => _items.Count;
        public double AvgVolumeQuote => _items.Average(i => i.VolumeQuote);
        //public double AvgVolume => _items.Average(i => i.VolumeBase);
        public double AvgLength => _items.Average(i => i.GetLength());

        public void Add(ICandlestick candle)
        {
            var length = candle.GetLength();
            if (length > Length)
                throw new ArgumentException("Candle insufficient length");
            _items.Add(candle);
        }

    }

    public class CandleSet
    {
        public CandleCollection Short = new CandleCollection();
        public CandleCollection Average = new CandleCollection();
        public CandleCollection Long = new CandleCollection();
        public CandleCollection Longer = new CandleCollection();
        public CandleCollection VeryLong = new CandleCollection();

        public void AddRange(IList<ICandlestick> candles)
        {
            foreach (ICandlestick candle in candles)
            {
                Add(candle);
            }
        }

        public void Add(ICandlestick candle)
        {
            var length = candle.GetLength();
            if (length < 0.005)
                Short.Add(candle);
            else if (length < 0.01)
                Average.Add(candle);
            else if (length < 0.02)
                Long.Add(candle);
            else if (length < 0.04)
                Longer.Add(candle);
            else
                VeryLong.Add(candle);
        }
    }

    //var slice = summary["30"];
    //slice.Volume
    //slice.Price
    //slice.Length
    
    public class Slice 
    {
        public double Volume;//avg V
        public double TotalVolume;
        public double Price;//avg price
        public double Length;//avg length
        public double LengthMax;//avg length
        public double LengthMin;//avg length
        public double LongCandles;//count, long means candle length > avg length

        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
    }
}



/*
 var data = load last 5000 candles [TF=30M, ~ 4 months]
 
 var chartSummary = analisisEngine.BuildChartSummary(data);


  ChartAnalitics.Build(List<ICandlestick>)


  PairIndex{
      "LTCBTC"
      Scale=180
      TotalVolume
      Volume
      OHLC
      Price
      Length
  }


 */

/*
var candles = [...]; //(OHLC)

    R: [2186-3260]
    Count: 120
    LongCandles: 5


Channel{
   Range: [3030-3100] => [3006-3100]
   H/L
   Count (candles count): 27
   Length (delta): 3%

}  
 
 */
//(candles)=> 
