namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentMethodValidator
    {
        public string Source { get; set; }
        public string Regex { get; set; }
        public bool Required { get; set; }
    }
}
