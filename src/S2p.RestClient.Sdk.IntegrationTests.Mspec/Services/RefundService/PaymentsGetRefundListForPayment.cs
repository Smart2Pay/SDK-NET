using System.Linq;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.RefundService
{
    partial class RefundServiceTests
    {

        [Subject(typeof(Sdk.Services.RefundService))]
        public class When_requesting_the_refund_list_for_a_payment
        {
            protected static ApiResult<ApiRefundListResponse> ApiListResult;
            public const int PaymentId = 3708827;
            public const int RefundCount= 1;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiListResult = RefundService.GetRefundListAsync(PaymentId).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_be_successful = () => {
                ApiListResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiListResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_non_empty_refunds_list = () => { ApiListResult.Value.Refunds.Count.ShouldBeGreaterThan(0); };


            private It should_have_correct_number_of_refunds_in_list = () => { ApiListResult.Value.Refunds.Count.ShouldEqual(RefundCount); };


            private It should_have_not_null_refund_ids = () => {
                ApiListResult.Value.Refunds.Count(p => p.ID == null || p.ID <= 0).ShouldEqual(0);
            };

            private It should_have_not_null_merchant_transaction_ids = () => {
                ApiListResult.Value.Refunds.Count(p => string.IsNullOrWhiteSpace(p.MerchantTransactionID)).ShouldEqual(0);
            };

            private It should_have_not_null_amounts = () => {
                ApiListResult.Value.Refunds.Count(p => p.Amount == null || p.Amount <= 0).ShouldEqual(0);
            };

            private It should_have_not_null_currency = () => {
                ApiListResult.Value.Refunds.Count(p => string.IsNullOrWhiteSpace(p.Currency)).ShouldEqual(0);
            };

            private It should_have_correct_initial_payment_id = () => {
                ApiListResult.Value.Refunds.Count(p => p.InitialPaymentID != PaymentId).ShouldEqual(0);
            };

        }

    }
}
