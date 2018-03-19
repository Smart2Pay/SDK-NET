using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public abstract class FilterBase
    {
        private const string DateTimeFormat = "yyyyMMddHHmmss";
        private readonly IDictionary<string, object> PropertyDictionary = new ConcurrentDictionary<string, object>();

        protected T GetOrAddDefault<T>(string key)
        {
            key.ThrowIfNullOrWhiteSpace(nameof(key));
            if (!PropertyDictionary.ContainsKey(key))
            {
                PropertyDictionary.Add(new KeyValuePair<string, object>(key, default(T)));
            }

            return (T)PropertyDictionary[key];
        }

        protected void Set<T>(string key, T value)
        {
            key.ThrowIfNullOrWhiteSpace(nameof(key));

            PropertyDictionary[key] = value;
        }

        public Uri ToQueryStringUri(Uri uri)
        {
            uri.ThrowIfNull(nameof(uri));

            var query = string.Join("&", PropertyDictionary
                .Where(item => !string.IsNullOrWhiteSpace(item.Key) &&
                               item.Value != null)
                .Select(item => $"{item.Key.UrlEncoded()}={GetFilterString(item.Value).UrlEncoded()}"));
            var uriBuilder = new UriBuilder(uri) {Query = query};
            var queryStringUri = new Uri(uriBuilder.ToString());

            return queryStringUri;
        }

        private static string GetFilterString(object value)
        {
            if (value == null) { return string.Empty; }

            var type = value.GetType();
            if (type == typeof(DateTime)) { return ((DateTime)value).ToString(DateTimeFormat, CultureInfo.InvariantCulture); }
            if (type == typeof(DateTime?)) { return ((DateTime?)value).Value.ToString(DateTimeFormat, CultureInfo.InvariantCulture); }

            return value.ToString();
        }
    }
}
