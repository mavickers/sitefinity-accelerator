using System;
using System.Globalization;

namespace SitefinityAccelerator.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddMillisecond(this DateTime source)
        {
            return AddMilliseconds(source, 1);
        }

        public static DateTime AddMilliseconds(this DateTime source, int milliseconds)
        {
            return source.Add(new TimeSpan(0, 0, 0, milliseconds));
        }

        public static DateTime SubtractMillisecond(this DateTime source)
        {
            return SubtractMilliseconds(source, 1);
        }

        public static DateTime SubtractMilliseconds(this DateTime source, int milliseconds)
        {
            return source.Subtract(new TimeSpan(0, 0, 0, milliseconds));
        }

        public static string ToExact(this DateTime source)
        {
            return source.ToString("yyyyMMddHHmmssfff");
        }

        public static bool TryParse(string source, out DateTime result)
        {
            result = DateTime.MinValue;

            if (source == null)
            {
                return false;
            }

            if (DateTime.TryParse(source, out var dateTimeParse))
            {
                result = dateTimeParse;

                return true;
            }

            if (DateTime.TryParseExact(source, "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeParseExact))
            {
                result = dateTimeParseExact;

                return true;
            }

            return false;
        }
    }
}