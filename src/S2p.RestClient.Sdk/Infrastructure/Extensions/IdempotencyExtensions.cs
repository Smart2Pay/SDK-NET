using System.Linq;
using System.Net.Http.Headers;

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
    }
}
