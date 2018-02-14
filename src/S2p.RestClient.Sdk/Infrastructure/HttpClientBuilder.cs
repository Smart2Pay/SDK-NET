using System;
using System.Collections.Generic;
using System.Net.Http;
using Polly;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Resilience;

namespace S2p.RestClient.Sdk.Infrastructure
{
    public class HttpClientBuilder : IHttpClientBuilder
    {
        public bool IsResilient { get; set; }

        private Func<AuthenticationConfiguration> _authenticationProvider;
        private Func<HttpRequestMessage, object, IAsyncPolicy<HttpResponseMessage>> _resilienceAsyncPolicyGenerator;
        private object _resilienceConfiguration;
        private Func<string> _idempotencyKeyGenerator;
        private IEnumerable<DelegatingHandler> _outerCustomHandlers;
        private IEnumerable<DelegatingHandler> _innerCustomHandlers;
        private Uri _baseAddress;
        private HttpMessageHandler _primaryHandler;

        public HttpClientBuilder(Func<AuthenticationConfiguration> authenticationProvider)
        {
            authenticationProvider.ThrowIfNull("Cannot build http client without authentication provider");
            _authenticationProvider = authenticationProvider;
            IsResilient = true;
        }

        public HttpClientBuilder WithAuthenticationProvider(Func<AuthenticationConfiguration> authenticationProvider)
        {
            authenticationProvider.ThrowIfNull("Cannot build http client without authentication provider");
            _authenticationProvider = authenticationProvider;
            return this;
        }

        public HttpClientBuilder WithResilienceAsyncPolicyGenerator(Func<HttpRequestMessage, object, IAsyncPolicy<HttpResponseMessage>> resilienceAsyncPolicyGenerator)
        {
            _resilienceAsyncPolicyGenerator = resilienceAsyncPolicyGenerator;
            return this;
        }

        public HttpClientBuilder WithResilienceConfiguration(object resilienceConfiguration)
        {
            _resilienceConfiguration = resilienceConfiguration;
            return this;
        }

        public HttpClientBuilder WithIdempotencyKeyGenerator(Func<string> idempotencyKeyGenerator)
        {
            _idempotencyKeyGenerator = idempotencyKeyGenerator;
            return this;
        }

        public HttpClientBuilder WithOuterCustomHandlers(IEnumerable<DelegatingHandler> customHandlers)
        {
            _outerCustomHandlers = customHandlers;
            return this;
        }

        public HttpClientBuilder WithInnerCustomHandlers(IEnumerable<DelegatingHandler> customHandlers)
        {
            _innerCustomHandlers = customHandlers;
            return this;
        }

        public HttpClientBuilder WithBaseAddress(Uri baseAddress)
        {
            _baseAddress = baseAddress;
            return this;
        }

        internal HttpClientBuilder WithPrimaryHandler(HttpMessageHandler primaryHandler)
        {
            _primaryHandler = primaryHandler;
            return this;
        }

        public HttpClient Build()
        {
            var primaryHandler = _primaryHandler ?? new HttpClientHandler();
            var httpClient = primaryHandler
                .DecorateIf(() => new ResilienceHandler()
                        .WithAsyncPolicyGenerator(_resilienceAsyncPolicyGenerator)
                        .WithConfiguration(_resilienceConfiguration),
                    () => IsResilient)
                .DecorateIfNotNull(_innerCustomHandlers)
                .DecorateWith(new IdempotencyHandler().WithUniqueKeyGenerator(_idempotencyKeyGenerator))
                .DecorateWith(new AuthenticationHandler(_authenticationProvider))
                .DecorateIfNotNull(_outerCustomHandlers)
                .CreateHttpClient()
                .ApplyIf(c => c.BaseAddress = _baseAddress, c => _baseAddress != null);

            return httpClient;
        } 
    }
}
