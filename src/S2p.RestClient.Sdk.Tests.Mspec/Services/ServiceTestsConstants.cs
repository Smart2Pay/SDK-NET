using S2p.RestClient.Sdk.Infrastructure.Authentication;

namespace S2p.RestClient.Sdk.Tests.Mspec.Services
{
    public class ServiceTestsConstants
    {
        public const string BaseUrl = "https://paytest.smart2pay.com";
        public static readonly AuthenticationConfiguration AuthenticationConfiguration = new AuthenticationConfiguration
        {
            SiteId = 30201,
            ApiKey = "hJ5RobYx9r7FfNwCvHY9LXHqqr+FEzrc7aJvQQk4Gaz1mg7Ryy"
        };
    }
}
