using System;

namespace AVS.Trading.Core.Formatters
{
    public abstract class BaseFormatter : ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return PreFormat(format, arg);
        }

        protected virtual string PreFormat(string format, object arg)
        {
            return NotEmpty(ref format, arg) ?? Format(format, arg);
        }

        protected virtual string Format(string format, object arg)
        {
            return string.IsNullOrEmpty(format) ? arg.ToString() : string.Format("{0:" + format + "}", arg);
        }

        protected string NotEmpty(ref string format, object arg)
        {
            string filter = "!empty";
            if (format != null && format.StartsWith(filter))
            {
                if (arg == null)
                    return String.Empty;

                else if (arg is int i && i == 0)
                    return String.Empty;

                else if (arg is double d && Math.Abs(d) < Constants.OneSatoshi)
                    return String.Empty;

                else if (arg is DateTime date && date == DateTime.MinValue)
                    return String.Empty;

                format = format.Length == filter.Length ? String.Empty 
                    : format.Substring(filter.Length+1, format.Length - (filter.Length+1));
            }
            return null;
        }
    }
}