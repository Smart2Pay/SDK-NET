using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardFraudDetailsResponse
    {
        public string Status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CheckMode { get; set; }
        public int Score { get; set; }
        public string Reason { get; set; }
    }
}
