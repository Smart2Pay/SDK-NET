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
        public class When_getting_details_for_a_specific_payment
        {
            private static ApiResult<ApiPaymentResponse> CreatePaymentResult { get; set; }

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
                CreatePaymentResult = await PaymentService.CreatePaymentAsync(PaymentRequest);
                return await PaymentService.GetPaymentAsync(CreatePaymentResult.Value.Payment.ID.Value);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_ok_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
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

            private It should_have_the_correct_method_id = () => {
                ApiResult.Value.Payment.MethodID.ShouldEqual(PaymentRequest.Payment.MethodID);
            };

            private It should_have_the_correct_email = () => {
                ApiResult.Value.Payment.Customer.Email.ShouldEqual(PaymentRequest.Payment.Customer.Email);
            };

            private It should_have_the_correct_country = () => {
                ApiResult.Value.Payment.BillingAddress.Country.ShouldEqual(
                    PaymentRequest.Payment.BillingAddress.Country);
            };

            private It should_have_the_correct_return_url = () => {
                ApiResult.Value.Payment.ReturnURL.ShouldEqual(PaymentRequest.Payment.ReturnURL);
            };

            private It should_have_the_correct_redirect_url = () => {
                var url = ApiResult.Value.Payment.RedirectURL;
                url.Substring(0, url.IndexOf('=')).ShouldEqual("https://apitest.smart2pay.com/Home?PaymentToken");
            };

            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(PaymentStatusDefinition.Authorized);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Payment.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Authorized));
            };

            private It should_have_correct_billing_address_id = () => {
                ApiResult.Value.Payment.BillingAddress.ID.ShouldEqual(CreatePaymentResult.Value.Payment.BillingAddress.ID);
            };

            private It should_have_correct_shipping_address_id = () => {
                ApiResult.Value.Payment.BillingAddress.ID.ShouldEqual(CreatePaymentResult.Value.Payment.BillingAddress.ID);
            };

            private It should_have_correct_customer_id = () => {
                ApiResult.Value.Payment.Customer.ID.ShouldEqual(CreatePaymentResult.Value.Payment.Customer.ID);
            };

            private It should_have_correct_article_id = () => {
                ApiResult.Value.Payment.Articles[0].MerchantArticleID.ShouldEqual(CreatePaymentResult.Value.Payment.Articles[0].MerchantArticleID);
            };
        }
    }
}
