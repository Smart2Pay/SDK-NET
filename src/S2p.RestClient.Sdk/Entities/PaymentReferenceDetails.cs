namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentReferenceDetails
    {
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string EntityID { get; set; }
        public string EntityNumber { get; set; }
        public string ReferenceID { get; set; }
        public string ReferenceNumber { get; set; }
        public string SwiftBIC { get; set; }
        public string AccountCurrency { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string IBAN { get; set; }
        public string QRCodeURL { get; set; }
    }
}
