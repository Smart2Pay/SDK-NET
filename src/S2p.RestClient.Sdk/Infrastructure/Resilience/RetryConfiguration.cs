namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    public class RetryConfiguration
    {
        public int Count { get; set; }
        public double DelayExponentialFactor { get; set; }
    }
}
