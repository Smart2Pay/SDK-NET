namespace S2p.RestClient.Sdk.Entities
{
    public class ExchangeRateResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public string DateTime { get; set; }
        public decimal Rate { get; set; }
    }
}
