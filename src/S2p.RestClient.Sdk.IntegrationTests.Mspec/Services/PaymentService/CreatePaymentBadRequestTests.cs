using System.Globalization;
using System.Linq;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PaymentService
{
    partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.PaymentService))]
        public class When_creating_a_payment_with_wrong_country
        {
            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentService = new Sdk.Services.PaymentService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                PaymentRequest = new PaymentRequest
                {
                    MerchantTransactionID = MerchantTransactionID,
                    Amount = 11,
                    Currency = "CNY",
                    MethodID = 1066,
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    TokenLifetime = 10,
                    Customer = new Customer
                    {
                        Email = "john@doe.com"
                    },
                    BillingAddress = new Address
                    {
                        Country = "CNN"
                    }
                }.ToApiPaymentRequest();

                ApiResult = PaymentService.CreatePaymentAsync(PaymentRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_created_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
            };

            private It should_have_the_same_merchant_transaction_id = () => {
                ApiResult.Value.Payment.MerchantTransactionID.ShouldEqual(PaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_correct_amount = () => {
                ApiResult.Value.Payment.Amount.ShouldEqual(PaymentRequest.Payment.Amount);
            };

            private It should_have_the_same_currency = () => {
                ApiResult.Value.Payment.Currency.ShouldEqual(PaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_status_id = () => {
                ((int?)ApiResult.Value.Payment.Status.ID).ShouldEqual(4);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Payment.Status.Info.ShouldEqual("Failed");
            };

            private It should_have_the_correct_status__reason_code = () => {
                var reason = ApiResult.Value.Payment.Status.Reasons.First();
                reason.Code.ShouldEqual("147");
            };

            private It should_have_the_correct_status__reason_info = () => {
                var reason = ApiResult.Value.Payment.Status.Reasons.First();
                reason.Info.Trim().ShouldEqual("Address details are invalid (BillingAddress)Country - RegEx: ^[a-zA-Z]{2}$;");
            };
        }
    }
}
