using S2p.RestClient.Sdk.Infrastructure.Authentication;
using System.Net;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services
{
    public class ServiceTestsConstants
    {
        public const string PaymentBaseUrl = "https://paytest.smart2pay.com";
        public static readonly AuthenticationConfiguration PaymentAuthenticationConfiguration = new AuthenticationConfiguration
        {
            SiteId = 45614,
            ApiKey = "rAwLLh3rQk3uNTOPHpqydrEOdAGsRzZChCd4uyXsXoGE2tkoYA"
        };
        public const string PayoutBaseUrl = "https://securetest.smart2pay.com";
        public static readonly AuthenticationConfiguration PayoutAuthenticationConfiguration = new AuthenticationConfiguration
        {
            SiteId = 33258,
            ApiKey = "JOPxYQftN9xICry9koMuER6L4SrszVHI8SLh9Q83n964tFa2GK"
        };

        public static void EnableTLS12()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}
