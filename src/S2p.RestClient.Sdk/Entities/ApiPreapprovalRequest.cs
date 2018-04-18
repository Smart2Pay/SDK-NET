using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiPreapprovalRequest
    {
        public PreapprovalRequest Preapproval { get; set; }
    }

    public static class PreapprovalRequestExtensions
    {
        public static ApiPreapprovalRequest ToApiPreapprovalRequest(this PreapprovalRequest preapprovalRequest)
        {
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            return new ApiPreapprovalRequest {Preapproval = preapprovalRequest};
        }
    }
}
