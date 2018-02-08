using System;

namespace S2p.RestClient.Sdk.Validation
{
    public static class Extensions
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

        public static TOut Transform<TIn, TOut>(this TIn @this, Func<TIn, TOut> transform)
        {
            @this.ThrowIfNull("Cannot project null values");
            transform.ThrowIfNull("Transform function must be specified");

            return transform(@this);
        }
    }
}
