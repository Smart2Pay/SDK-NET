using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class RefundResponse
    {  
        public int ID { get; set; }
        public string Created { get; set; }
        public string MerchantTransactionID { get; set; }
        public string OriginatorTransactionID { get; set; }
        public int InitialPaymentID { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public int TypeID { get; set; }
        public int? SiteID { get; set; }
        public RefundDetails Details { get; set; }
        public Customer Customer { get; set; }
        public Address BillingAddress { get; set; }
        public Address BankAddress { get; set; }
        public List<Article> Articles { get; set; }
        [JsonProperty("Status")]
        public PaymentStateDetails PaymentStateDetails { get; set; }
    }
}
