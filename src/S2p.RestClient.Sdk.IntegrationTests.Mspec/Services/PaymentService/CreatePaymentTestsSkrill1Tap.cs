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

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PaymentService
{
    public partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.PaymentService))]
        public class When_creating_a_payment_for_skrill_1tap
        {
            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentService = new Sdk.Services.PaymentService(HttpClient, BaseAddress);
                PaymentRequest = new ApiPaymentRequest
                {
                    Payment = new PaymentRequest
                    {
                        MerchantTransactionID = MerchantTransactionID,
                        Amount = 200,
                        Currency = "EUR",
                        MethodID = 78,
                        Description = DescriptionText,
                        ReturnURL = "http://demo.smart2pay.com/redirect.php",
                        TokenLifetime = 10,
                        Customer = new Customer
                        {
                            Email = "skrill_user_test@smart2pay.com"
                        },
                        BillingAddress = new Address
                        {
                            Country = "DE"
                        },
                        PreapprovalDetails = new PreapprovalDetails{
                        PreapprovedMaximumAmount = 500,
                        MerchantPreapprovalID = MerchantTransactionID,
                        PreapprovalDescription = "Skrill 1tap payment SDK"
                    }

                }
                };
            };

            private Because of = () => {
                ApiResult = PaymentService.CreatePaymentAsync(PaymentRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

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

            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(PaymentStatusDefinition.Open);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Payment.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Open));
            };

            private It should_have_the_correct_preapproved_maximum_amount = () => {
                ApiResult.Value.Payment.PreapprovalDetails.PreapprovedMaximumAmount.ShouldEqual(PaymentRequest.Payment.PreapprovalDetails.PreapprovedMaximumAmount);
            };

            private It should_have_the_correct_merchant_preapproval_id = () => {
                ApiResult.Value.Payment.PreapprovalDetails.MerchantPreapprovalID.ShouldEqual(PaymentRequest.Payment.PreapprovalDetails.MerchantPreapprovalID);
            };

        }
    }
}
