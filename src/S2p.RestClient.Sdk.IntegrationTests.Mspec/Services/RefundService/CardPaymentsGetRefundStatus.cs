using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.RefundService
{
    partial class CardRefundServiceTests
    {

        [Subject(typeof(Sdk.Services.RefundService))]
        public class When_requesting_refund_status_for_card_payments_api
        {
            protected static ApiResult<ApiCardRefundStatusResponse> ApiResult;
            public const int PaymentId = 279463;
            public const int RefundId = 4621;


            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = RefundService.GetRefundStatusAsync(PaymentId, RefundId).GetAwaiter().GetResult();
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
                string merchantTransactionID = "77502fff-195f-46fb-8430-bcafe114364d";
                ApiResult.Value.Refund.MerchantTransactionID.ShouldEqual(merchantTransactionID);
            };

            private It should_have_the_correct_id = () => {
                ApiResult.Value.Refund.ID.ShouldEqual(RefundId);
            };

            private It should_have_the_correct_initial_payment_id = () => {
                ApiResult.Value.Refund.InitialPaymentID.ShouldEqual(PaymentId);
            };

            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Refund.StateDetails.ID.ShouldEqual(PaymentStatusDefinition.Success);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Refund.StateDetails.Info.ShouldEqual(nameof(PaymentStatusDefinition.Success));
            };

        }

    }
}
