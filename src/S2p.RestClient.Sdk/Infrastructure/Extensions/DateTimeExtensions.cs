using System;
using System.Globalization;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public const string GlobalPayDateTimeFormat = "yyyyMMddHHmmss";

        public static string ToGlobalPayFormattedString(this DateTime @this)
        {
            return @this.ToString(GlobalPayDateTimeFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime? ToGlobalPayDateTime(this string @this)
        {
            var parseResult = DateTime.TryParseExact(@this, GlobalPayDateTimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var result);

            if (parseResult) { return result; }
            return null;
        }
    }
}
