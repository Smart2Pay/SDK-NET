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
        public class When_creating_a_partial_capture_with_empty_body_for_klarna_method
        {
            private const int CapturedAmount = 1960;

            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                _alternativePaymentService = new Sdk.Services.AlternativePaymentService(HttpClient, BaseAddress);
                PaymentRequest = new AlternativePaymentRequest
                {
                    Amount = 2940,
                    Currency = "GBP",
                    Description = DescriptionText,
                    MethodID = 1078,
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    MerchantTransactionID = MerchantTransactionID, //$"klpayments_00aa03-{DateTime.UtcNow.Ticks}",
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
                            Type = 5,
                            TaxType = 2
                        },
                        new Article
                        {
                            MerchantArticleID = "1232",
                            Name = "TEST",
                            Quantity = 2,
                            Price = 1000,
                            VAT = 1000,
                            Discount = 200,
                            Type = 5
                        }
                    },
                    BillingAddress = new Address
                    {
                        City = "London",
                        ZipCode = "W13 3BG",
                        State = "Iasi",
                        Street = "New Burlington St 113",
                        StreetNumber = "Apt 214",
                        Country = "GB"
                    },

                    ShippingAddress = new Address
                    {
                        City = "London",
                        ZipCode = "W13 3BG",
                        State = "Iasi",
                        Street = "New Burlington St 113",
                        StreetNumber = "Apt 214",
                        Country = "GB"
                    },
                    Customer = new Customer
                    {
                        Email = "youremail@email.com",
                        Phone = "+440745785615",
                        FirstName = "Doe",
                        LastName = "Test",
                    }
                }.ToApiAlternativePaymentRequest();

            };

            private Because of = () => {
                ApiResult = BecauseAsync().GetAwaiter().GetResult();
            };

            private static async Task<ApiResult<ApiAlternativePaymentResponse>> BecauseAsync()
            {
                var createPaymentResult = await _alternativePaymentService.CreatePaymentAsync(PaymentRequest);

                using (var page = new KlarnaPaymentPage(createPaymentResult.Value.Payment.RedirectURL))
                {
                    page.Load();
                    page.BuyButtonClick();
                }

                return await _alternativePaymentService.CapturePaymentAsync(createPaymentResult.Value.Payment.ID.Value, CapturedAmount);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_ok_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_the_correct_amount = () => {
                ApiResult.Value.Payment.Amount.ShouldEqual(PaymentRequest.Payment.Amount);
            };

            private It should_have_the_correct_captured_amount = () => {
                ApiResult.Value.Payment.CapturedAmount.ShouldEqual(CapturedAmount);
            };

            private It should_have_the_correct_currency = () => {
                ApiResult.Value.Payment.Currency.ShouldEqual(PaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_merchant_transaction_id = () => {
                ApiResult.Value.Payment.MerchantTransactionID.ShouldEqual(PaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_correct_payment_status = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(PaymentStatusDefinition.PartiallyCaptured);
            };
        }

        [Subject(typeof(Sdk.Services.AlternativePaymentService))]
        public class When_creating_a_partial_capture_with_body_for_klarna_method
        {
            private static ApiAlternativePaymentRequest PartialCaptureRequest;
            private const int CapturedAmount = 1960;

            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                _alternativePaymentService = new Sdk.Services.AlternativePaymentService(HttpClient, BaseAddress);
                PaymentRequest = new AlternativePaymentRequest
                {
                    Amount = 2940,
                    Currency = "GBP",
                    Description = DescriptionText,
                    MethodID = 1078,
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    MerchantTransactionID = MerchantTransactionID, //$"klpayments_00aa03-{DateTime.UtcNow.Ticks}",
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
                            Type = 5,
                            TaxType = 2
                        },
                        new Article
                        {
                            MerchantArticleID = "1232",
                            Name = "TEST",
                            Quantity = 2,
                            Price = 1000,
                            VAT = 1000,
                            Discount = 200,
                            Type = 5
                        }
                    },
                    BillingAddress = new Address
                    {
                        City = "London",
                        ZipCode = "W13 3BG",
                        State = "Iasi",
                        Street = "New Burlington St 113",
                        StreetNumber = "Apt 214",
                        Country = "GB"
                    },

                    ShippingAddress = new Address
                    {
                        City = "London",
                        ZipCode = "W13 3BG",
                        State = "Iasi",
                        Street = "New Burlington St 113",
                        StreetNumber = "Apt 214",
                        Country = "GB"
                    },
                    Customer = new Customer
                    {
                        Email = "youremail@email.com",
                        Phone = "+440745785615",
                        FirstName = "Doe",
                        LastName = "Test",
                    }
                }.ToApiAlternativePaymentRequest();

                PartialCaptureRequest = new AlternativePaymentRequest
                {
                    Articles = new List<Article>
                    {
                        new Article
                        {
                            MerchantArticleID = "1232",
                            Quantity = 2
                        }
                    }
                }.ToApiAlternativePaymentRequest();

            };

            private Because of = () => {
                ApiResult = BecauseAsync().GetAwaiter().GetResult();
            };

            private static async Task<ApiResult<ApiAlternativePaymentResponse>> BecauseAsync()
            {
                var createPaymentResult = await _alternativePaymentService.CreatePaymentAsync(PaymentRequest);

                using (var page = new KlarnaPaymentPage(createPaymentResult.Value.Payment.RedirectURL))
                {
                    page.Load();
                    page.BuyButtonClick();
                }

                return await _alternativePaymentService.CapturePaymentAsync(createPaymentResult.Value.Payment.ID.Value, CapturedAmount
                    , PartialCaptureRequest);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_ok_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_the_correct_amount = () => {
                ApiResult.Value.Payment.Amount.ShouldEqual(PaymentRequest.Payment.Amount);
            };

            private It should_have_the_correct_captured_amount = () => {
                ApiResult.Value.Payment.CapturedAmount.ShouldEqual(CapturedAmount);
            };

            private It should_have_the_correct_currency = () => {
                ApiResult.Value.Payment.Currency.ShouldEqual(PaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_merchant_transaction_id = () => {
                ApiResult.Value.Payment.MerchantTransactionID.ShouldEqual(PaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_correct_payment_status = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(PaymentStatusDefinition.PartiallyCaptured);
            };

        }
    }
}
