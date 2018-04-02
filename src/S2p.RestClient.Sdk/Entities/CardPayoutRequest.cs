using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardPayoutRequest
    {
        [JsonRequired]
        public string MerchantTransactionID { get; set; }
        [JsonRequired]
        public long Amount { get; set; }
        [JsonRequired]
        public string Currency { get; set; }
        public string Description { get; set; }
        public string StatementDescriptor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BillingAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CardDetailsRequest Card { get; set; }
        public bool? Capture { get; set; }
        public bool? Retry { get; set; }
    }
}
