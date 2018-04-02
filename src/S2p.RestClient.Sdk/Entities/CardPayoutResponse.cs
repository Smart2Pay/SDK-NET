using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardPayoutResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InvalidRequestID { get; set; }
        public long? ID { get; set; }
        public int? SiteID { get; set; }
        public string Created { get; set; }
        public string MerchantTransactionID { get; set; }
        public long? Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string StatementDescriptor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BillingAddress { get; set; }
        public CardStateDetails Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MethodTransactionID { get; set; }
    }
}
