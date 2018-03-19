using System.ComponentModel;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CreditCardTokenDetailsRequest
    {
        public string Value { get; set; }
        public string SecurityCode { get; set; }

        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool RequireSecurityCode { get; set; } = false;

        public bool ShouldSerializeSecurityCode()
        {
            return !string.IsNullOrEmpty(SecurityCode);
        }
    }
}
