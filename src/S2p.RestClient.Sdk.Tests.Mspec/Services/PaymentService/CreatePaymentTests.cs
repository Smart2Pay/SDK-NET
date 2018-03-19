using System;
using System.Globalization;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.Tests.Mspec.Services
{
    public partial class PaymentServiceTests
    {
        private static IPaymentService PaymentService;

        private static ApiResult<ApiPaymentResponse> ApiResult;
        private static string MerchantTransactionID => Guid.NewGuid().ToString();
        private static ApiPaymentRequest PaymentRequest;
        private static IHttpClientBuilder HttpClientBuilder;

        private static void InitializeHttpBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.AuthenticationConfiguration)
                .WithBaseAddress(new Uri(ServiceTestsConstants.BaseUrl));
        }


        [Subject(typeof(PaymentService))]
        public class When_creating_a_payment
        {
            private Establish context = () => {
                InitializeHttpBuilder();
                PaymentService = new PaymentService(HttpClientBuilder);
            };

            private Because of = () => {
                PaymentRequest = new ApiPaymentRequest
                {
                    Payment = new PaymentRequest
                    {
                        MerchantTransactionID = MerchantTransactionID,
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
                            Country = "CN"
                        }
                    }
                };

                ApiResult = PaymentService.CreatePaymentAsync(PaymentRequest).GetAwaiter().GetResult();
            };

            private It should_have_created_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
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

            private It should_have_the_correct_qr_code_url = () => {
                var url = ApiResult.Value.Payment.ReferenceDetails.QRCodeURL;
                url.Substring(0, url.IndexOf('=')).ShouldEqual("weixin://wxpay/bizpayurl?pr");
            };
        }
    }
}
