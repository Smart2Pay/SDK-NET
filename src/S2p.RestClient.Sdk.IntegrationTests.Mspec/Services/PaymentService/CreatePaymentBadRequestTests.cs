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
                PaymentServiceTests.InitializeHttpBuilder();
                PaymentServiceTests.HttpClient = PaymentServiceTests.HttpClientBuilder.Build();
                PaymentServiceTests.PaymentService = new Sdk.Services.PaymentService(PaymentServiceTests.HttpClient, PaymentServiceTests.BaseAddress);
            };

            private Because of = () => {
                PaymentServiceTests.PaymentRequest = new ApiPaymentRequest
                {
                    Payment = new PaymentRequest
                    {
                        MerchantTransactionID = PaymentServiceTests.MerchantTransactionID,
                        Amount = 11.ToString(CultureInfo.InvariantCulture),
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
                    }
                };

                PaymentServiceTests.ApiResult = PaymentServiceTests.PaymentService.CreatePaymentAsync(PaymentServiceTests.PaymentRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { PaymentServiceTests.HttpClient.Dispose(); };

            private It should_have_created_status_code = () => {
                PaymentServiceTests.ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
            };

            private It should_have_the_same_merchant_transaction_id = () => {
                PaymentServiceTests.ApiResult.Value.Payment.MerchantTransactionID.ShouldEqual(PaymentServiceTests.PaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_correct_amount = () => {
                PaymentServiceTests.ApiResult.Value.Payment.Amount.ShouldEqual(PaymentServiceTests.PaymentRequest.Payment.Amount);
            };

            private It should_have_the_same_currency = () => {
                PaymentServiceTests.ApiResult.Value.Payment.Currency.ShouldEqual(PaymentServiceTests.PaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_status_id = () => {
                ((int?)PaymentServiceTests.ApiResult.Value.Payment.Status.ID).ShouldEqual(4);
            };

            private It should_have_the_correct_status_info = () => {
                PaymentServiceTests.ApiResult.Value.Payment.Status.Info.ShouldEqual("Failed");
            };

            private It should_have_the_correct_status__reason_code = () => {
                var reason = PaymentServiceTests.ApiResult.Value.Payment.Status.Reasons.First();
                reason.Code.ShouldEqual("147");
            };

            private It should_have_the_correct_status__reason_info = () => {
                var reason = PaymentServiceTests.ApiResult.Value.Payment.Status.Reasons.First();
                reason.Info.Trim().ShouldEqual("Address details are invalid (BillingAddress)Country - RegEx: ^[a-zA-Z]{2}$;");
            };
        }
    }
}
