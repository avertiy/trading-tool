using System;
using System.Linq;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.Formatters
{
    //no usages
    public abstract class BaseFormatInfo : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string GetString(string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            // Check whether this is an appropriate callback             
            if (!this.Equals(formatProvider))
                return null;

            return Format(format, arg);
        }

        protected virtual string Format(string format, object arg)
        {
            return string.IsNullOrEmpty(format) ? arg.ToString() : string.Format("{0:" + format + "}", arg);
        }
    }
}