using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class IdempotencyExtensions
    {
        private const string IdempotencyHeader = "RequestId";

        public static void AddIdempotencyHeader(this HttpRequestHeaders @this, string idempotencyToken)
        {
            @this.ThrowIfNull("Cannot add idempotency header to null request headers collection.");
            idempotencyToken.ThrowIfNullOrWhiteSpace("Cannot add null or empty idempotency token");

            @this.Add(IdempotencyHeader, idempotencyToken);
        }

        public static bool HasIdempotencyHeader(this HttpRequestHeaders @this)
        {
            @this.ThrowIfNull("Cannot check idempotency header for null request headers collection.");

            return @this.Contains(IdempotencyHeader);
        }

        public static string GetIdempotencyToken(this HttpRequestHeaders @this)
        {
            @this.ThrowIfNull("Cannot get idempotency header from null request headers collection.");

            var result = @this.GetValues(IdempotencyHeader)
                .FirstOrDefault()
                .ValueIfNull(() => string.Empty);
            
            return result;
        }

        public static bool RemoveIdempotencyHeader(this HttpRequestHeaders @this)
        {
            @this.ThrowIfNull("Cannot remove idempotency header from a null request headers collection.");

            return @this.Remove(IdempotencyHeader);
        }

        public static T WithIdempotencyToken<T>(this HttpClient @this, string idempotencyToken,
            Func<HttpClient, T> operation)
        {
            @this.ThrowIfNull("Cannot add idempotency header to null http client.");

            try
            {
                @this.DefaultRequestHeaders.AddIdempotencyHeader(idempotencyToken);
                return operation(@this);
            }
            finally
            {
                @this.DefaultRequestHeaders.RemoveIdempotencyHeader();
            }
        }
    }
}
