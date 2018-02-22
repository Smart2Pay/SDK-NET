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
            @this.ThrowIfNull(typeof(HttpRequestHeaders).Name.ToLower());
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));

            @this.Add(IdempotencyHeader, idempotencyToken);
        }

        public static bool HasIdempotencyHeader(this HttpRequestHeaders @this)
        {
            @this.ThrowIfNull(typeof(HttpRequestHeaders).Name.ToLower());

            return @this.Contains(IdempotencyHeader);
        }

        public static string GetIdempotencyToken(this HttpRequestHeaders @this)
        {
            @this.ThrowIfNull(typeof(HttpRequestHeaders).Name.ToLower());

            var result = @this.GetValues(IdempotencyHeader)
                .FirstOrDefault()
                .ValueIfNull(() => string.Empty);
            
            return result;
        }

        public static bool RemoveIdempotencyHeader(this HttpRequestHeaders @this)
        {
            @this.ThrowIfNull(typeof(HttpRequestHeaders).Name.ToLower());

            return @this.Remove(IdempotencyHeader);
        }

        public static T WithIdempotencyToken<T>(this HttpClient @this, string idempotencyToken,
            Func<HttpClient, T> operation)
        {
            @this.ThrowIfNull(typeof(HttpClient).Name.ToLower());

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
