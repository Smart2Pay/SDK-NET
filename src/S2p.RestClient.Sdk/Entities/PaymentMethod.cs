using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentMethod
    {
        public short ID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string LogoURL { get; set; }
        public bool Guaranteed { get; set; }
        public bool Active { get; set; }
        public bool SupportIframe { get; set; }
        public short IframeWidth { get; set; }
        public short IframeHeight { get; set; }
        public List<string> Countries { get; set; }
        public List<string> Currencies { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DetailsURL { get; set; }
        public List<PaymentMethodValidator> ValidatorsPayin { get; set; }
        public List<PaymentMethodValidator> ValidatorsRecurrent { get; set; }
        public List<PaymentMethodOption> Options { get; set; }
    }
}
