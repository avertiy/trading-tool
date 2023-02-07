using System;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.Trading.Core;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.MarketTools.Models
{
    public class Candlestick  : ICandlestick
    {
        [JsonProperty("date")]
        private ulong TimeInternal {
            set => Time = value.UnixTimeStampToDateTime();
        }

        [JsonProperty("low")]
        private string LowStringValue
        {
            set => Low = NumericHelper.ParseDouble(value);
        }
        [JsonProperty("high")]
        private string HighStringValue
        {
            set => High = NumericHelper.ParseDouble(value);
        }

        [JsonProperty("open")]
        private string OpenStringValue
        {
            set => Open = NumericHelper.ParseDouble(value);
        }

        [JsonProperty("close")]
        private string CloseStringValue
        {
            set => Close = NumericHelper.ParseDouble(value);
        }

        [JsonProperty("weightedAverage")]
        private string AvgStringValue
        {
            set => WeightedAverage = NumericHelper.ParseDouble(value);
        }

        public DateTime Time { get; private set; }

        public double Low { get; private set; }

        public double High { get; private set; }

        public double Open { get; private set; }

        public double Close { get; private set; }
        
        public double WeightedAverage { get; private set; }
        [JsonIgnore]
        public bool HasData => Open + Close + High + Low > 0.0;

        [JsonProperty("volume")]
        public double VolumeBase { get; private set; }
        [JsonProperty("quoteVolume")]
        public double VolumeQuote { get; private set; }

        public override string ToString()
        {
            return $"{Time:MMM dd HH:mm}  OHLC:[{this.Format("ohlc")}]";
        }
    }
}
