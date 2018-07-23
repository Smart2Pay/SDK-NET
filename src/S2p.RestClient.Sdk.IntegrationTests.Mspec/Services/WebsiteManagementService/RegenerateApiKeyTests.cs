using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.WebsiteManagementService
{
    public partial class When_running_a_complete_website_management_flow
    {
        private static ApiResult<ApiWebsiteManagementResponse> RegenerateApiKeyApiResult;

        private It regenerate_should_have_ok_http_status = () => {
            RegenerateApiKeyApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
        };

        private It regenerate_should_have_different_api_key = () => {
            RegenerateApiKeyApiResult.Value.MerchantSite.ApiKey.ShouldNotEqual(
                ChangeApiResult.Value.MerchantSite.ApiKey);
        };
    }
}
