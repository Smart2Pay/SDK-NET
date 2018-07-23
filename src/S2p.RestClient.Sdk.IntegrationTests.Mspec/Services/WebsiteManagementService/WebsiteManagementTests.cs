using System;
using System.Net.Http;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.WebsiteManagementService
{
    [Subject(typeof(Sdk.Services.WebsiteManagementService))]
    public partial class When_running_a_complete_website_management_flow
    {
        private static Uri BaseAddress = new Uri(ServiceTestsConstants.PaymentSystemBaseUrl);

        private static IHttpClientBuilder OriginalHttpClientBuilder;
        private static HttpClient OriginalHttpClient;
        private static IWebsiteManagementService OriginalWebsiteManagementService;

        private static HttpClientBuilder AfterCreateWebSiteHttpBuilder;
        private static HttpClient AfterCreateWebsiteHttpClient;
        private static IWebsiteManagementService AfterCreateManagementService;

        private static HttpClientBuilder AfterRegenerateWebSiteHttpBuilder;
        private static HttpClient AfterRegenerateWebsiteHttpClient;
        private static IWebsiteManagementService AfterRegenerateManagementService;

        private static void InitializeOriginalWebsiteService()
        {
            OriginalHttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.PaymentSystemAuthenticationConfiguration);
            OriginalHttpClient = OriginalHttpClientBuilder.Build();
            OriginalWebsiteManagementService = new Sdk.Services.WebsiteManagementService(OriginalHttpClient, BaseAddress);
        }

        private static void InitializeAfterCreateWebsiteService()
        {
            AfterCreateWebSiteHttpBuilder = new HttpClientBuilder(() => new AuthenticationConfiguration
                { ApiKey = CreateApiResult.Value.MerchantSite.ApiKey, SiteId = CreateApiResult.Value.MerchantSite.SiteID });
            AfterCreateWebsiteHttpClient = AfterCreateWebSiteHttpBuilder.Build();
            AfterCreateManagementService = new Sdk.Services.WebsiteManagementService(AfterCreateWebsiteHttpClient, BaseAddress);
        }

        private static void InitializeAfterRegenerateWebsiteService()
        {
            AfterRegenerateWebSiteHttpBuilder = new HttpClientBuilder(() => new AuthenticationConfiguration
                { ApiKey = RegenerateApiKeyApiResult.Value.MerchantSite.ApiKey, SiteId = RegenerateApiKeyApiResult.Value.MerchantSite.SiteID });
            AfterRegenerateWebsiteHttpClient = AfterRegenerateWebSiteHttpBuilder.Build();
            AfterRegenerateManagementService = new Sdk.Services.WebsiteManagementService(AfterRegenerateWebsiteHttpClient, BaseAddress);
        }

        private Establish context = () => { InitializeOriginalWebsiteService(); };

        private Because of = () => { BecauseAsync().GetAwaiter().GetResult(); };

        private static async Task BecauseAsync()
        {
            CreateApiResult = await OriginalWebsiteManagementService.CreateWebsiteAsync(CreateRequest.Value);

            InitializeAfterCreateWebsiteService();

            ChangeApiResult =
                await AfterCreateManagementService.ChangeWebsiteAsync(CreateApiResult.Value.MerchantSite.SiteID,
                    ChangeRequest.Value);

            RegenerateApiKeyApiResult =
                await AfterCreateManagementService.RegenerateWebsiteApiKeyAsync(CreateApiResult.Value.MerchantSite
                    .SiteID);

            InitializeAfterRegenerateWebsiteService();

            GetWebsiteInfoApiResult = await AfterRegenerateManagementService.GetWebsiteAsync(CreateApiResult.Value
                .MerchantSite.SiteID);

            DeleteApiResult =
                await AfterRegenerateManagementService.DeleteWebsiteAsync(CreateApiResult.Value.MerchantSite.SiteID);
        }

        private Cleanup after = () => {
            OriginalHttpClient.Dispose();
            AfterCreateWebsiteHttpClient.Dispose();
            AfterRegenerateWebsiteHttpClient.Dispose();
        };
    }
}
