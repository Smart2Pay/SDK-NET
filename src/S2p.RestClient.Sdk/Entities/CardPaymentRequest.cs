using System.ComponentModel;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardPaymentRequest
    {
        public int? SkinID { get; set; }
        public string ClientIP { get; set; }
        [JsonRequired]
        public string MerchantTransactionID { get; set; }
        public string OriginatorTransactionID { get; set; }
        [JsonRequired]
        public long Amount { get; set; }
        [JsonRequired]
        public string Currency { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ReturnURL { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string StatementDescriptor { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BillingAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address ShippingAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CardDetailsRequest Card { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public CreditCardTokenDetailsRequest CreditCardToken { get; set; }
        [JsonProperty("3DSecure")]
        public bool? ThreeDSecureCheck { get; set; }
        public bool? Capture { get; set; }
        public bool? Retry { get; set; }
        public bool GenerateCreditCardToken { get; set; }
        public int? PaymentTokenLifetime { get; set; }
        [DefaultValue(0)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public short Installments { get; set; }
    }
}
