using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.AlternativePaymentService
{
    public partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.AlternativePaymentService))]
        public class When_creating_a_payment_for_bank_tranfer
        {
            
            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                _alternativePaymentService = new Sdk.Services.AlternativePaymentService(HttpClient, BaseAddress);
                PaymentRequest = new PaymentRequest
                {
                    MerchantTransactionID = MerchantTransactionID,
                    Amount = 200,
                    Currency = "EUR",
                    MethodID = 1,
                    Description = DescriptionText,
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    TokenLifetime = 10,
                    Customer = new Customer
                    {
                        FirstName = "SDK",
                        LastName = "Test"
                    },
                    BillingAddress = new Address
                    {
                        Country = "AT"
                    },
                    Details = new PaymentCustomerDetails()
                    {
                        ReferenceNumber = MerchantTransactionID
                    }
                }.ToApiPaymentRequest();
            };

            private Because of = () => {
                ApiResult = _alternativePaymentService.CreatePaymentAsync(PaymentRequest).GetAwaiter().GetResult();
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

            private It should_have_the_correct_first_name = () => {
                ApiResult.Value.Payment.Customer.FirstName.ShouldEqual(PaymentRequest.Payment.Customer.FirstName);
            };

            private It should_have_the_correct_last_name = () => {
                ApiResult.Value.Payment.Customer.LastName.ShouldEqual(PaymentRequest.Payment.Customer.LastName);
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

            private It should_have_the_correct_reference_number = () => {
                ApiResult.Value.Payment.ReferenceDetails.ReferenceNumber.ShouldEqual(PaymentRequest.Payment.Details.ReferenceNumber);
            };

        }
    }
}
