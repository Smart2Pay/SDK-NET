namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentMethodOption
    {
        public short ID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public bool Guaranteed { get; set; }
    }
}
