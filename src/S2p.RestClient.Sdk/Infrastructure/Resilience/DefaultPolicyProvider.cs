using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    internal class DefaultPolicyProvider
    {
        private static readonly HttpStatusCode[] HttpStatusCodesToRetry =
        {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout // 504
        };

        internal static readonly ConcurrentDictionary<string, IAsyncPolicy<HttpResponseMessage>>
            PolicyCollection = new ConcurrentDictionary<string, IAsyncPolicy<HttpResponseMessage>>();

        private static RetryPolicy<HttpResponseMessage> CreateAsyncRetryPolicy(RetryConfiguration configuration)
        {
            return Policy
                .Handle<TimeoutException>()
                .Or<HttpRequestException>()
                .OrResult<HttpResponseMessage>(response => HttpStatusCodesToRetry.Contains(response.StatusCode))
                .WaitAndRetryAsync(configuration.Count, 
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(configuration.DelayExponentialFactor, retryAttempt)));
        }

        private static CircuitBreakerPolicy<HttpResponseMessage> CreateAsyncCircuitBreaker(CircuitBreakerConfiguration configuration)
        {
            return Policy
                .Handle<TimeoutException>()
                .Or<HttpRequestException>()
                .OrResult<HttpResponseMessage>(response => HttpStatusCodesToRetry.Contains(response.StatusCode))
                .AdvancedCircuitBreakerAsync(configuration.FailureThreshold, configuration.SamplingDuration,
                    configuration.MinimumThroughput, configuration.DurationOfBreak);
        }

        public IAsyncPolicy<HttpResponseMessage> GetAsyncPolicy(HttpRequestMessage request, object configuration)
        {
            var config = (configuration as ResilienceConfiguration).ValueIfNull(() => ResilienceDefault.Configuration);
            var retryConfig = config.Retry.ValueIfNull(() => ResilienceDefault.Configuration.Retry);
            var circuitBreakerConfig = config.CircuitBreaker.ValueIfNull(() => ResilienceDefault.Configuration.CircuitBreaker);

            var baseUri = request.RequestUri.GetBaseUri().ToString();
            
            var policy = PolicyCollection.GetOrAdd(baseUri, uri =>
            {
                var retryPolicy = CreateAsyncRetryPolicy(retryConfig);
                var circuitBreaker = CreateAsyncCircuitBreaker(circuitBreakerConfig);

                return retryPolicy.WrapAsync(circuitBreaker);
            });

            return policy;
        }
    }
}
