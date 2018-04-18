using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiCardPayoutRequest
    {
        public CardPayoutRequest Payout { get; set; }
    }

    public static class CardPayoutRequestExtenions
    {
        public static ApiCardPayoutRequest ToApiCardPayoutRequest(this CardPayoutRequest cardPayoutRequest)
        {
            cardPayoutRequest.ThrowIfNull(nameof(cardPayoutRequest));
            return new ApiCardPayoutRequest {Payout = cardPayoutRequest};
        }
    }
}
