using System.Linq;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PreapprovallService
{
    partial class PreapprovalServiceTests
    {
        [Subject(typeof(Sdk.Services.PreapprovalService))]
        public class When_requesting_preapproval_list
        {
            private static ApiResult<ApiPreapprovalListResponse> ApiListResult;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PreapprovalService = new Sdk.Services.PreapprovalService(HttpClient, BaseAddress);

            };

            private Because of = () => {
                ApiListResult = PreapprovalServiceTests.PreapprovalService.GetPreapprovalListAsync().GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_non_empty_preapproval_list = () => { ApiListResult.Value.Preapprovals.Count.ShouldBeGreaterThan(0); };

            private It should_be_successful = () => { ApiListResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                ApiListResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };
            
            private It should_have_correct_site_id = () => {
                ApiListResult.Value.Preapprovals.Count(p => p.SiteID == null ||
                                                        p.SiteID.Value != ServiceTestsConstants
                                                            .PaymentSystemAuthenticationConfiguration.SiteId)
                    .ShouldEqual(0);
            };

            private It should_have_not_null_payment_ids = () => {
                ApiListResult.Value.Preapprovals.Count(p => p.ID == null || p.ID <= 0).ShouldEqual(0);
            };

            private It should_have_not_null_merchant_preapproval_ids = () => {
                ApiListResult.Value.Preapprovals.Count(p => string.IsNullOrWhiteSpace(p.MerchantPreapprovalID)).ShouldEqual(0);
            };

        }

    }
}
