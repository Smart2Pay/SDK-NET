using S2p.RestClient.Sdk.Services;
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

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.RefundService
{
    partial class RefundServiceTests
    {

        [Subject(typeof(Sdk.Services.RefundService))]
        public class When_requesting_refund_types_filtered_for_payments_api
        {
            protected static ApiResult<ApiRefundTypeListResponse> ApiResult;
            private const short MethodID = 49;
            private const string Currency = "MXN";
            private const string Country = "MX";

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = RefundService.GetRefundTypesAsync(MethodID.ToString(), Country, Currency).GetAwaiter().GetResult();
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
                ApiResult.Value.RefundTypes.Count(i => string.IsNullOrWhiteSpace(i.Name)).ShouldEqual(0);
            };

            private It should_have_not_null_ids = () => {
                ApiResult.Value.RefundTypes.Select(m => m.ID).Distinct().Count(i => i == 0 || i <= 0).ShouldEqual(0);
            };
        }

    }
}
