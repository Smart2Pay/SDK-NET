using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiRefundRequest
    {
        public RefundRequest Refund { get; set; }
    }

    public static class RefundRequestExtensions
    {
        public static ApiRefundRequest ToApiRefundRequest(this RefundRequest refundRequest)
        {
            refundRequest.ThrowIfNull(nameof(refundRequest));
            return new ApiRefundRequest {Refund = refundRequest};
        }
    }
}
