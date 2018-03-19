namespace S2p.RestClient.Sdk.Entities
{
    public class CardPayoutStatusResponse
    {
        public long? ID { get; set; }
        public string MerchantTransactionID { get; set; }
        public CardStateDetails Status { get; set; }
    }
}
