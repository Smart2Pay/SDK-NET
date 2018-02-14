using System;

namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    internal static class ResilienceDefault
    {
        public static readonly ResilienceConfiguration Configuration = new ResilienceConfiguration
        {
            Retry = new RetryConfiguration
            {
                Count = 3, // Retries 3 times
                DelayExponentialFactor = 2 // Wait time between retries is Math.Pow(2, retryAttempt)
            },
            CircuitBreaker = new CircuitBreakerConfiguration
            {
                FailureThreshold = 0.7, // Break on >=70% actions result in handled exceptions
                SamplingDuration = TimeSpan.FromSeconds(60), // over any 60 seconds period
                MinimumThroughput = 16, // provided at least 16 actions in the 60 seconds period.
                DurationOfBreak = TimeSpan.FromSeconds(60) // Break for 60 seconds.
            }
        };

        public static readonly DefaultPolicyProvider PolicyProvider = new DefaultPolicyProvider();

        public static readonly Func<string> UniqueKeyGenerator = () => Guid.NewGuid().ToString();
    }
}
