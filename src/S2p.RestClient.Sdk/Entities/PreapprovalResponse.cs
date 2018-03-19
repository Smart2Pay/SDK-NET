using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class PreapprovalResponse
    {
        public int? ID { get; set; }
        public string Created { get; set; }
        public short? MethodID { get; set; }
        public int? SiteID { get; set; }
        public string MerchantPreapprovalID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int RecurringPeriod { get; set; }
        public string PreapprovedMaximumAmount { get; set; }
        public string Currency { get; set; }
        public string ReturnURL { get; set; }
        public string Description { get; set; }
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }
        public PreapprovalStateDetails Status { get; set; }
        public string RedirectURL { get; set; }
        public int MethodOptionID { get; set; }
        public string PreapprovedFrequency { get; set; }
    }
}
