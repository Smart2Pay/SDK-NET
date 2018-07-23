using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiWebsiteManagementRequest
    {
        public MerchantSite MerchantSite { get; set; }
    }

    public static class MerchantSiteRequestExtensions
    {
        public static ApiWebsiteManagementRequest ToApiWebsiteManagementRequest(this MerchantSite merchantSite)
        {
            merchantSite.ThrowIfNull(nameof(merchantSite));
            return new ApiWebsiteManagementRequest {MerchantSite = merchantSite};
        }
    }
}
