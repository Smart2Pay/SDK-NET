using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.RefundService
{
    partial class RefundServiceTests
    {

        [Subject(typeof(Sdk.Services.RefundService))]
        public class When_requesting_refund_details_for_payments_api
        {
            protected static ApiResult<ApiRefundResponse> ApiResult;
            public const long PaymentId = 3708827;
            public const int RefundId = 23293;


            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = RefundService.GetRefundAsync(PaymentId, RefundId).GetAwaiter().GetResult();
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

            private It should_have_the_correct_merchant_transaction_id = () => {
                const string merchantTransactionID = "e2530274-174a-46aa-a912-0df5975d64aa";
                ApiResult.Value.Refund.MerchantTransactionID.ShouldEqual(merchantTransactionID);
            };

            private It should_have_the_correct_amount = () => {
                const long amount = 980;
                ApiResult.Value.Refund.Amount.ShouldEqual(amount);
            };

            private It should_have_the_same_currency = () => {
                const string currency = "DKK";
                ApiResult.Value.Refund.Currency.ShouldEqual(currency);
            };

            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Refund.Status.ID.ShouldEqual(PaymentStatusDefinition.Success);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Refund.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Success));
            };

            private It should_have_correct_site_id = () => {
                ApiResult.Value.Refund.SiteID.ShouldEqual(ServiceTestsConstants.PaymentSystemAuthenticationConfiguration.SiteId);
            };
        }

    }
}
