using System;
using System.Globalization;
using AVS.CoreLib.Extensions;
namespace AVS.Trading.Core.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime ParseUtcDateTime(string dateTime)
        {
            return DateTime.SpecifyKind(DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }

        public static DateTime ParseUtcDateTimeFromUnixTimestamp(string value, bool milliseconds = false)
        {
            var val = ulong.Parse(value);
            return milliseconds ? val.FromUnixTimeStampMs() : val.FromUnixTimeStamp();
        }
    }
}