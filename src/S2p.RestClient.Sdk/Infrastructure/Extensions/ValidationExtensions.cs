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

        public static void ThrowIfNotCondition<T>(this T @this, Predicate<T> predicate, string message)
        {
            @this.ThrowIfNull(typeof(T).Name.ToLowerInvariant());
            predicate.ThrowIfNull(nameof(predicate));

            var exceptionMessage = string.IsNullOrWhiteSpace(message)
                ? string.Empty
                : message;

            if (!predicate(@this)) { throw new ArgumentException(exceptionMessage); }
        }
    }
}
