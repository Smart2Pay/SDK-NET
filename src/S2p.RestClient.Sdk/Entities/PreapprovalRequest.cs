using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class PreapprovalRequest
    {
        public short MethodID { get; set; }
        public bool ShouldSerializeMethodID()
        {
            return MethodID > 0;
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MerchantPreapprovalID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int RecurringPeriod { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string PreapprovedMaximumAmount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnURL { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int MethodOptionID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BillingAddress { get; set; }
    }
}
