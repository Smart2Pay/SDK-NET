using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardPaymentResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string InvalidRequestID { get; set; }
        public long? ID { get; set; }
        public string ClientIP { get; set; }
        public int? SkinID { get; set; }
        public string Created { get; set; }
        public string MerchantTransactionID { get; set; }
        public string OriginatorTransactionID { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string CapturedAmount { get; set; }
        public string ReturnURL { get; set; }
        public string Description { get; set; }
        public string StatementDescriptor { get; set; }
        public int MethodID => 6; 
        public int? MethodOptionID { get; set; }
        [JsonProperty("SiteID")]
        public int? MerchantSiteID { get; set; }
        public string NotificationDateTime { get; set; }
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public List<Article> Articles { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public PaymentCustomerDetails Details { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ReferenceDetails { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CustomParameters { get; set; }
        public CardDetailsRequest Card { get; set; }
        public CreditCardTokenDetailsRequest CreditCardToken { get; set; }
        public CardStateDetails Status { get; set; }
        public string MethodTransactionID { get; set; }
        public int? PaymentTokenLifetime { get; set; }
        public bool? Capture { get; set; }
        public bool? Retry { get; set; }
        public string RedirectURL { get; set; }
        [JsonProperty("3DSecure")]
        public bool? ThreeDSecure { get; set; }
        public CardFraudDetailsResponse Fraud { get; set; }
        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public short Installments { get; set; }
    }
}
