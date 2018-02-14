using System;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class UriExtensions
    {
        public static Uri GetBaseUri(this Uri @this)
        {
            var baseUri = new Uri(string.Format("{0}://{1}", @this.Scheme, @this.Authority));
            return baseUri;
        }
    }
}
