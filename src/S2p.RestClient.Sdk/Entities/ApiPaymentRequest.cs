using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiPaymentRequest
    {
        public PaymentRequest Payment { get; set; }
    }

    public static class PaymentRequestExtensions
    {
        public static ApiPaymentRequest ToApiPaymentRequest(this PaymentRequest paymentRequest)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            return new ApiPaymentRequest {Payment = paymentRequest};
        }
    }
}
