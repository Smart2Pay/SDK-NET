using System;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public static class Extensions
    {
        public static Tuple<RetryPolicy<HttpResponseMessage>, CircuitBreakerPolicy<HttpResponseMessage>>
            GetDefaultPolicies(this IAsyncPolicy<HttpResponseMessage> policy)
        {
            var wrapPolicy = policy as IPolicyWrap;
            wrapPolicy.ThrowIfNull(nameof(wrapPolicy));

            var circuitBreakerPolicy =
                wrapPolicy.Inner as CircuitBreakerPolicy<HttpResponseMessage>;
            var retryPolicy = wrapPolicy.Outer as RetryPolicy<HttpResponseMessage>;

            return Tuple.Create(retryPolicy, circuitBreakerPolicy);
        }
    }
}
