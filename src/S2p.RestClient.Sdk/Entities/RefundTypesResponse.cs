namespace S2p.RestClient.Sdk.Entities
{
    public class RefundTypesResponse
    {
        public string Name { get; set; }
        public byte ID { get; set; }
        public bool AllowPartialRefund { get; set; }
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }
        public Address BankAddress { get; set; }
        public RefundDetails Details { get; set; }
    }
}
