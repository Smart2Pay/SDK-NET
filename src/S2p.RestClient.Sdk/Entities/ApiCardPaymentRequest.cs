using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiCardPaymentRequest
    {
        public CardPaymentRequest Payment { get; set; }
    }

    public static class CardPaymentRequestExtensions
    {
        public static ApiCardPaymentRequest ToApiCardPaymentRequest(this CardPaymentRequest cardPaymentRequest)
        {
            cardPaymentRequest.ThrowIfNull(nameof(cardPaymentRequest));
            return new ApiCardPaymentRequest {Payment = cardPaymentRequest};
        }
    }
}
