using System.Net.Http;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class Constants
    {
        public const string RequestContextKey = "request";
        public static HttpMethod HttpMethodPatch => new HttpMethod("PATCH");
    }
}
