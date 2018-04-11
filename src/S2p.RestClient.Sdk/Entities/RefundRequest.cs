using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class RefundRequest
    {
        public int ID { get; set; }
        public string MerchantTransactionID { get; set; }
        public string OriginatorTransactionID { get; set; }
        public long Amount { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public RefundDetails Details { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Customer Customer { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BillingAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Address BankAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Article> Articles { get; set; }
        public int? TokenLifetime { get; set; }
        public bool ShouldSerializeTokenLifetime()
        {
            return TokenLifetime.HasValue;
        }
    }
}
