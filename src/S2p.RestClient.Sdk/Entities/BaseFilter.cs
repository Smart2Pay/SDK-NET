using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public abstract class BaseFilter
    {
        protected readonly IDictionary<string, string> PropertyDictionary = new ConcurrentDictionary<string, string>();

        protected string GetOrAddDefault(string key)
        {
            key.ThrowIfNullOrWhiteSpace(nameof(key));
            if (!PropertyDictionary.ContainsKey(key))
            {
                PropertyDictionary.Add(new KeyValuePair<string, string>(key, default(string)));
            }

            return PropertyDictionary[key];
        }

        public Uri ToQueryStringUri(Uri uri)
        {
            uri.ThrowIfNull(nameof(uri));

            var query = string.Join("&", PropertyDictionary.Where(item => !string.IsNullOrWhiteSpace(item.Key) &&
                                                                          !string.IsNullOrWhiteSpace(item.Value))
                .Select(item =>
                    $"{System.Net.WebUtility.UrlEncode(item.Key)}={System.Net.WebUtility.UrlEncode(item.Value)}"));
            var uriBuilder = new UriBuilder(uri) { Query = query };
            var queryStringUri = new Uri(uriBuilder.ToString());

            return queryStringUri;
        }
    }
}
