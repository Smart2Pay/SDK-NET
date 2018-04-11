using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentResponse
    {
        public int? ID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public long? InvalidRequestID { get; set; }
        public int? SkinID { get; set; }
        public string ClientIP { get; set; }
        public string Created { get; set; }
        public string MerchantTransactionID { get; set; }
        public string OriginatorTransactionID { get; set; }
        public long? Amount { get; set; }
        public string Currency { get; set; }
        public string ReturnURL { get; set; }
        public string Description { get; set; }
        public short? MethodID { get; set; }
        public short? MethodOptionID { get; set; }
        public List<int> IncludeMethodIDs { get; set; }
        public List<int> ExcludeMethodIDs { get; set; }
        public List<int> PrioritizeMethodIDs { get; set; }
        public int? SiteID { get; set; }
        public string NotificationDateTime { get; set; }
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public List<Article> Articles { get; set; }
        public PaymentCustomerDetails Details { get; set; }
        public PaymentReferenceDetails ReferenceDetails { get; set; }
        public Dictionary<string, string> CustomParameters { get; set; }
        public int? PreapprovalID { get; set; }
        public PaymentStateDetails Status { get; set; }
        public string MethodTransactionID { get; set; }
        public int? TokenLifetime { get; set; }
        public bool? Capture { get; set; }
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
        public PreapprovalDetails PreapprovalDetails { get; set; }
        public string RedirectURL { get; set; }
    }
}
