using System.ComponentModel;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardDetailsRequest
    {
        [JsonRequired]
        public string HolderName { get; set; }

        [JsonRequired]
        public string Number { get; set; }

        [JsonRequired]
        public string ExpirationMonth { get; set; }

        [JsonRequired]
        public string ExpirationYear { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SecurityCode { get; set; }

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool RequireSecurityCode { get; set; } = true;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string MaskedNumber { get; set; }
    }
}
