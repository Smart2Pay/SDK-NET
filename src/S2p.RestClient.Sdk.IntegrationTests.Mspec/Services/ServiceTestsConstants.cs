using S2p.RestClient.Sdk.Infrastructure.Authentication;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services
{
    public class ServiceTestsConstants
    {
        public const string PaymentSystemBaseUrl = "https://paytest.smart2pay.com";
        public static readonly AuthenticationConfiguration PaymentSystemAuthenticationConfiguration = new AuthenticationConfiguration
        {
            SiteId = 45614,
            ApiKey = "rAwLLh3rQk3uNTOPHpqydrEOdAGsRzZChCd4uyXsXoGE2tkoYA"
        };

        public const string CardPaymentSystemBaseUrl = "https://securetest.smart2pay.com";
        public static readonly AuthenticationConfiguration CardPaymentSystemAuthenticationConfiguration = new AuthenticationConfiguration
        {
            SiteId = 33258,
            ApiKey = "JOPxYQftN9xICry9koMuER6L4SrszVHI8SLh9Q83n964tFa2GK"
        };
    }
}
