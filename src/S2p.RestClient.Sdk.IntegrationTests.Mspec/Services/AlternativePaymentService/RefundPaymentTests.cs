using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.AlternativePaymentService
{
    partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.AlternativePaymentService))]
        public class When_a_native_refund_is_performend_for_a_payment
        {

            private static Sdk.Services.RefundService RefundService { get; set; }
            private static ApiRefundRequest RefundRequest { get; set; }
            private static ApiResult<ApiRefundResponse> RefundApiResult { get; set; }

            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                _alternativePaymentService = new Sdk.Services.AlternativePaymentService(HttpClient, BaseAddress);
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
                PaymentRequest = new AlternativePaymentRequest
                {
                    Amount = 980,
                    Currency = "DKK",
                    Description = "test capture SDK",
                    MethodID = 75,
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    MerchantTransactionID = MerchantTransactionID,
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            MerchantArticleID = "1231",
                            Name = "TEST",
                            Quantity = 1,
                            Price = 1000,
                            VAT = 1000,
                            Discount = 200,
                            Type = ArticleType.Product
                        }
                    },
                    BillingAddress = new Address()
                    {
                        HouseNumber = "",
                        Street = "Seffleberggate 56,1 mf",
                        ZipCode = "6800",
                        City = "Varde",
                        Country = "DK"
                    },
                    ShippingAddress = new Address
                    {
                        HouseNumber = "",
                        Street = "Seffleberggate 56,1 mf",
                        ZipCode = "6800",
                        City = "Varde",
                        Country = "DK"
                    },
                    Customer = new Customer
                    {
                        FirstName = "Testperson-dk",
                        LastName = "Approved",
                        Gender = "1",
                        Email = "youremail@email.com",
                        Phone = "20123456",
                        SocialSecurityNumber = "0801363945"
                    },
                    TokenLifetime = 10
                }.ToApiAlternativePaymentRequest();

                RefundRequest = new RefundRequest()
                {
                    Amount = PaymentRequest.Payment.Amount,
                    MerchantTransactionID = MerchantTransactionID
                }.ToApiRefundRequest();

            };

            private Because of = () => {
                RefundApiResult = BecauseAsync().GetAwaiter().GetResult();
            };

            private static async Task<ApiResult<ApiRefundResponse>> BecauseAsync()
            {
                var createPaymentResult = await _alternativePaymentService.CreatePaymentAsync(PaymentRequest);
                await Task.Delay(2000);
                var capturedPaymentResult = await _alternativePaymentService.CapturePaymentAsync(createPaymentResult.Value.Payment.ID.Value);
                await Task.Delay(2000);
                return await RefundService.CreateRefundAsync(capturedPaymentResult.Value.Payment.ID.Value, RefundRequest);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_ok_status_code = () => {
                RefundApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };

            private It should_have_the_same_merchant_transaction_id = () =>
            {
                RefundApiResult.Value.Refund.MerchantTransactionID.ShouldEqual(RefundRequest.Refund.MerchantTransactionID);
            };

            private It should_have_the_correct_amount = () =>
            {
                RefundApiResult.Value.Refund.Amount.ShouldEqual(RefundRequest.Refund.Amount);
            };

            private It should_have_the_same_currency = () =>
            {
                RefundApiResult.Value.Refund.Currency.ShouldEqual(PaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_status_id = () =>
            {
                RefundApiResult.Value.Refund.Status.ID.ShouldEqual(PaymentStatusDefinition.Open);
            };

            private It should_have_the_correct_status_info = () =>
            {
                RefundApiResult.Value.Refund.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Open));
            };
        }
    }
}
