using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.RefundService
{
    public partial class RefundServiceTests
    {

        private static IRefundService RefundService;
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;
        private static Uri BaseAddress = new Uri(ServiceTestsConstants.PaymentBaseUrl);
        public const string PaymentId = "3708827";

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.PaymentAuthenticationConfiguration);
        }

        [Subject(typeof(Sdk.Services.RefundService))]
        public class When_requesting_refund_types
        {
            protected static ApiResult<ApiRefundTypeListResponse> ApiResult;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = RefundService.GetRefundTypesAsync(PaymentId).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_expected_number_of_refunds = () =>
            {
                ApiResult.Value.RefundTypes.Count.ShouldBeGreaterThan(0);
            };

            private It should_have_not_null_names = () => {
                ApiResult.Value.RefundTypes.Select(m => m.Name).Distinct().Count(i => string.IsNullOrWhiteSpace(i)).ShouldEqual(0);
            };

            private It should_have_not_null_ids = () => {
                ApiResult.Value.RefundTypes.Select(m => m.ID).Distinct().Where(i => i == 0 || i <= 0).Count().ShouldEqual(0);
            };
        }

    }
}
