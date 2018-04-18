using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PaymentService
{
    partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.PaymentService))]
        public class When_capturing_a_payment
        {
            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentService = new Sdk.Services.PaymentService(HttpClient, BaseAddress);
                PaymentRequest = new PaymentRequest
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
                }.ToApiPaymentRequest();
            };

            private Because of = () => {
                ApiResult = BecauseAsync().GetAwaiter().GetResult();    
            };

            private static async Task<ApiResult<ApiPaymentResponse>> BecauseAsync()
            {
                var createPaymentResult = await PaymentService.CreatePaymentAsync(PaymentRequest);
                return await PaymentService.CapturePaymentAsync(createPaymentResult.Value.Payment.ID.Value);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_ok_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_the_same_merchant_transaction_id = () =>
            {
                ApiResult.Value.Payment.MerchantTransactionID.ShouldEqual(PaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_correct_amount = () =>
            {
                ApiResult.Value.Payment.Amount.ShouldEqual(PaymentRequest.Payment.Amount);
            };

            private It should_have_the_same_currency = () =>
            {
                ApiResult.Value.Payment.Currency.ShouldEqual(PaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_method_id = () =>
            {
                ApiResult.Value.Payment.MethodID.ShouldEqual(PaymentRequest.Payment.MethodID);
            };

            private It should_have_the_correct_country = () =>
            {
                ApiResult.Value.Payment.BillingAddress.Country.ShouldEqual(
                    PaymentRequest.Payment.BillingAddress.Country);
            };

            private It should_have_the_correct_status_id = () =>
            {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(PaymentStatusDefinition.Success);
            };

            private It should_have_the_correct_status_info = () =>
            {
                ApiResult.Value.Payment.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Success));
            };
        }
    }
}
