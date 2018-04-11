namespace S2p.RestClient.Sdk.Entities
{
    public sealed class PreapprovalDetails
    {
        public long? PreapprovedMaximumAmount { get; set; }
        public string MerchantPreapprovalID { get; set; }
        public string Frequency { get; set; }
        public string PreapprovalDescription { get; set; }
    }
}
