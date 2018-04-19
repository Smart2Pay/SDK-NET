using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public abstract class FilterBase
    {
        private readonly IDictionary<string, object> _propertyDictionary = new ConcurrentDictionary<string, object>();

        protected T GetOrAddDefault<T>(string key)
        {
            key.ThrowIfNullOrWhiteSpace(nameof(key));
            if (!_propertyDictionary.ContainsKey(key))
            {
                _propertyDictionary.Add(new KeyValuePair<string, object>(key, default(T)));
            }

            return (T)_propertyDictionary[key];
        }

        protected void Set<T>(string key, T value)
        {
            key.ThrowIfNullOrWhiteSpace(nameof(key));

            _propertyDictionary[key] = value;
        }

        public Uri ToQueryStringUri(Uri uri)
        {
            uri.ThrowIfNull(nameof(uri));

            var query = string.Join("&", _propertyDictionary
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
            if (type == typeof(DateTime)) { return ((DateTime)value).ToGlobalPayFormattedString(); }
            if (type == typeof(DateTime?)) { return ((DateTime?)value).Value.ToGlobalPayFormattedString(); }

            return value.ToString();
        }
    }
}
