using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiCardPayoutStatusResponse
    {
        [JsonProperty("Payout")]
        public CardPayoutStatusResponse PayoutStatus { get; set; }
    }
}
