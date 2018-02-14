namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    public class ResilienceConfiguration
    {
        public RetryConfiguration Retry { get; set; }
        public CircuitBreakerConfiguration CircuitBreaker { get; set; }
    }
}
