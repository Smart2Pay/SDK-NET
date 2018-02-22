using System;
using System.Net.Http;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static T WithIdempotencyToken<T>(this HttpClient @this, string idempotencyToken,
            Func<HttpClient, T> operation)
        {
            @this.ThrowIfNull(typeof(HttpClient).Name.ToLower());
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            operation.ThrowIfNull(nameof(operation));

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
