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
    public partial class CardRefundServiceTests
    {
        private static IRefundService RefundService;
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;
        private static Uri BaseAddress = new Uri(ServiceTestsConstants.PayoutBaseUrl);

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.PayoutAuthenticationConfiguration);
        }
        [Subject(typeof(Sdk.Services.RefundService))]
        public class When_requesting_refund_details_for_card_payments_api
        {
            protected static ApiResult<ApiRefundResponse> ApiResult;
            public const int PaymentId = 279463;
            public const int RefundId = 4621;


            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = RefundService.GetRefundAsync(PaymentId.ToString(), RefundId.ToString()).GetAwaiter().GetResult();
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

            private It should_have_the_correct_id = () => {
                ApiResult.Value.Refund.ID.ShouldEqual(RefundId);
            };

            private It should_have_the_correct_initial_payment_id = () => {
                ApiResult.Value.Refund.InitialPaymentID.ShouldEqual(PaymentId);
            };

            private It should_have_the_correct_merchant_transaction_id = () => {
                string merchantTransactionID = "77502fff-195f-46fb-8430-bcafe114364d";
                ApiResult.Value.Refund.MerchantTransactionID.ShouldEqual(merchantTransactionID);
            };

            private It should_have_the_correct_amount = () => {
                long amount = 9000;
                ApiResult.Value.Refund.Amount.ShouldEqual(amount);
            };

            private It should_have_the_same_currency = () => {
                string currency = "EUR";
                ApiResult.Value.Refund.Currency.ShouldEqual(currency);
            };

            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Refund.Status.ID.ShouldEqual(PaymentStatusDefinition.Success);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Refund.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Success));
            };

            private It should_have_correct_site_id = () => {
                ApiResult.Value.Refund.SiteID.ShouldEqual(ServiceTestsConstants.PayoutAuthenticationConfiguration.SiteId);
            };
        }

    }
}
