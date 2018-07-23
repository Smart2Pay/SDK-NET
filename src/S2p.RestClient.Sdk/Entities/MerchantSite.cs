using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class MerchantSite
    {
        public int MerchantID { get; set; }
        [JsonProperty("ID")]
        public int SiteID { get; set; }
        public string Created { get; set; }
        public string URL { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public bool? Active { get; set; }
        public bool ShouldSerializeActive()
        {
            return SiteID > 0;
        }
        public string NotificationURL { get; set; }
        public string ApiKey { get; set; }
        public string Alias { get; set; }
        public string IPList { get; set; }
        [JsonProperty("Details", NullValueHandling = NullValueHandling.Ignore)]
        public MerchantSiteDetails Details { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LegalName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LegalCompanyAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankAddress { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AccountIBAN { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string AccountSWIFT { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankSWIFTID { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BankCode { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string VATNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string RegistrationNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MerchantSiteReason> Reasons { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Logo { get; set; }
    }
}
