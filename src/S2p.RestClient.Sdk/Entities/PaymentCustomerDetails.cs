namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentCustomerDetails
    {
        public string AccountNumber { get; set; }
        public string AccountHolder { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string PrepaidCard { get; set; }
        public string PrepaidCardPIN { get; set; }
        public string SerialNumbers { get; set; }
        public string Wallet { get; set; }
        public string ReferenceNumber { get; set; }
        public string PayerCountry { get; set; }
        public string PayerEmail { get; set; }
        public string PayerPhone { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankSortCode { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string BillingCycleStart { get; set; }
        public string BillingCycleEnd { get; set; }
        public string UnsubscribeInstructions { get; set; }
        public bool IsOffline { get; set; }
        public string StoreName { get; set; }
        public string StoreID { get; set; }
        public string TerminalID { get; set; }
    }
}
