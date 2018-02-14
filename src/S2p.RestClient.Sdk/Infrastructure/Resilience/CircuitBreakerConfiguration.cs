using System;

namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    public class CircuitBreakerConfiguration
    {
        public double FailureThreshold { get; set; }
        public TimeSpan SamplingDuration { get; set; }
        public int MinimumThroughput { get; set; }
        public TimeSpan DurationOfBreak { get; set; }
    }
}
