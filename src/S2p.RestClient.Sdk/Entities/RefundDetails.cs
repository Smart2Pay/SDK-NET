using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class RefundDetails
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerAccountNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CPFAccountHolder { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankAgencyCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankSWIFTID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankSortCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CustomerIBAN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankAccountType { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankBranch { get; set; }
    }
}
