using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentRequest
    {
        public int ID { get; set; }
        public int? SkinID { get; set; }
        public string ClientIP { get; set; }
        public string MerchantTransactionID { get; set; }
        public string OriginatorTransactionID { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string ReturnURL { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? PreapprovalID { get; set; }
        public short? MethodID { get; set; }
        public bool ShouldSerializeMethodID()
        {
            return MethodID.HasValue;
        }
        public short? MethodOptionID { get; set; }
        public bool ShouldSerializeMethodOptionID()
        {
            return MethodOptionID.HasValue;
        }
        public bool? Guaranteed { get; set; }
        public bool ShouldSerializeGuaranteed()
        {
            return Guaranteed.HasValue;
        }
        public bool? RedirectInIframe { get; set; }
        public bool ShouldSerializeRedirectInIframe()
        {
            return RedirectInIframe.HasValue;
        }
        public bool? RedirectMerchantInIframe { get; set; }
        public bool ShouldSerializeRedirectMerchantInIframe()
        {
            return RedirectMerchantInIframe.HasValue;
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int> IncludeMethodIDs { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int> ExcludeMethodIDs { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<int> PrioritizeMethodIDs { get; set; }
        public string Language { get; set; }
        public bool ShouldSerializeLanguage()
        {
            return !string.IsNullOrEmpty(Language);
        }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PaymentCustomerDetails Details { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> CustomParameters { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CardDetails CardDetails { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BillingAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address ShippingAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Article> Articles { get; set; }
        public int? TokenLifetime { get; set; }
        public bool? Capture { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PreapprovalDetails PreapprovalDetails { get; set; }
    }
}
