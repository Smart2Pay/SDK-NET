using System;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowIfNull(this object @this, string message)
        {
            var exceptionMessage = string.IsNullOrWhiteSpace(message)
                ? string.Empty
                : message;

            if (@this == null)
            {
                throw new ArgumentNullException(exceptionMessage);
            }
        }

        public static void ThrowIfNullOrWhiteSpace(this string @this, string message)
        {
            var exceptionMessage = string.IsNullOrWhiteSpace(message)
                ? string.Empty
                : message;

            if (@this == null)
            {
                throw new ArgumentNullException(exceptionMessage);
            }

            if (string.IsNullOrWhiteSpace(@this))
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
    }
}
