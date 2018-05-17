using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiAlternativePaymentRequest
    {
        public AlternativePaymentRequest Payment { get; set; }
    }

    public static class PaymentRequestExtensions
    {
        public static ApiAlternativePaymentRequest ToApiAlternativePaymentRequest(this AlternativePaymentRequest paymentRequest)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            return new ApiAlternativePaymentRequest {Payment = paymentRequest};
        }
    }
}
