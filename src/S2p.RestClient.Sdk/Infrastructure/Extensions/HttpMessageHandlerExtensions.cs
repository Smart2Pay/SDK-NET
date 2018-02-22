using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class HttpMessageHandlerExtensions
    {
        public static HttpMessageHandler DecorateWith(this HttpMessageHandler @this, DelegatingHandler decorator)
        {
            @this.ThrowIfNull(typeof(HttpMessageHandler).Name.ToLower());
            decorator.ThrowIfNull(nameof(decorator));

            decorator.InnerHandler = @this;
            return decorator;
        }

        public static HttpMessageHandler DecorateIf(this HttpMessageHandler @this, Func<DelegatingHandler> decoratorFactory
            , Func<bool> condition)
        {
            @this.ThrowIfNull(typeof(HttpMessageHandler).Name.ToLower());
            decoratorFactory.ThrowIfNull(nameof(decoratorFactory));
            condition.ThrowIfNull(nameof(condition));

            Func<DelegatingHandler> decorate = () =>
            {
                var decorator = decoratorFactory();
                decorator.InnerHandler = @this;
                return decorator;
            };

            return condition()
                ? decorate()
                : @this;

        }

        public static HttpMessageHandler DecorateIfNotNull(this HttpMessageHandler @this, DelegatingHandler decorator)
        {
            @this.ThrowIfNull(typeof(HttpMessageHandler).Name.ToLower());

            Func<DelegatingHandler> decorate = () =>
            {
                decorator.InnerHandler = @this;
                return decorator;
            };

            return decorator != null
                ? decorate()
                : @this;
        }

        public static HttpMessageHandler DecorateIfNotNull(this HttpMessageHandler @this,
            IEnumerable<DelegatingHandler> decoratorCollection)
        {
            @this.ThrowIfNull(typeof(HttpMessageHandler).Name.ToLower());

            return decoratorCollection != null
                ? decoratorCollection.Aggregate(@this, (current, delegatingHandler) => current.DecorateIfNotNull(delegatingHandler))
                : @this;
        }

        public static HttpClient CreateHttpClient(this HttpMessageHandler @this)
        {
            @this.ThrowIfNull(typeof(HttpMessageHandler).Name.ToLower());

            var httpClient = new HttpClient(@this);
            return httpClient;
        }
    }
}
