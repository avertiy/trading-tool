using System;
using System.Collections.Generic;
using System.Text;

namespace AVS.Trading.Framework.Tasks
{
    public class TaskParameters: Dictionary<string, string>
    {
        public string Exchange => GetValueOrDefault("x");

        public string Account => GetValueOrDefault("a");

        /// <summary>
        /// comma separated pairs e.g. -pairs BTC_LTC,BTC_ETH etc. 
        /// Also u can specify special values:
        /// -pairs all - all pairs supported by exchange 
        /// -pairs x - takes pairs from Exchange node config 
        /// </summary>
        public string Pairs => GetValueOrDefault("pairs");

        public string Print => GetValueOrDefault("print");

        public string[] GetPairs()
        {
            if (string.IsNullOrEmpty(Pairs))
            {
                return new string[]{};
            }
            return Pairs.Split(',');
        }

        public bool HasAny => !string.IsNullOrEmpty(Exchange + Pairs);

        public string GetValueOrDefault(string key, string defaultValue = null)
        {
            return this.TryGetValue(key, out string value) ? value : defaultValue;
        }

        public double GetDouble(string key, double @default = 0.0) 
        {
            if (!TryGetValue(key, out string strValue))
                return @default;

            if(!double.TryParse(strValue, out double value))
                return @default;
            return value;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var kp in this)
            {
                sb.Append($"{kp.Key} -{kp.Value} ");
            }

            if (sb.Length > 0)
                sb.Length--;
            return sb.ToString();
        }
    }


}