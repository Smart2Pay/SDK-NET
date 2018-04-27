using System;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    public partial class CardPaymentServiceTests
    {
      
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_creating_a_card_payment_with_3d_secure
        {
            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                CardPaymentRequest = new CardPaymentRequest
                {
                    MerchantTransactionID = MerchantTransactionID,
                    Amount = 10002,
                    Currency = "EUR",
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    Description = DescriptionText,
                    StatementDescriptor = "bank statement message",
                    Card = new CardDetailsRequest
                    {
                        HolderName = "John Doe",
                        Number = "4548812049400004",
                        ExpirationMonth = "02",
                        ExpirationYear = "2022",
                        SecurityCode = "312"
                    },
                    Capture = true,
                    Retry = true,
                    GenerateCreditCardToken = false,
                    PaymentTokenLifetime = 5,
                    ThreeDSecureCheck = true
                }.ToApiCardPaymentRequest();
            };

            private Because of = () => {
                ApiResult = CardPaymentService.InitiatePaymentAsync(CardPaymentRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_created_status_code = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };

            private It should_have_the_same_merchant_transaction_id = () => {
                ApiResult.Value.Payment.MerchantTransactionID.ShouldEqual(CardPaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_correct_amount = () => {
                ApiResult.Value.Payment.Amount.ShouldEqual(CardPaymentRequest.Payment.Amount);
            };

            private It should_have_the_same_currency = () => {
                ApiResult.Value.Payment.Currency.ShouldEqual(CardPaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_method_id = () => {
                ApiResult.Value.Payment.MethodID.ShouldEqual(CreditCardMethodID);
            };

            private It should_have_not_null_redirect_url = () => {
                String.IsNullOrWhiteSpace(ApiResult.Value.Payment.RedirectURL).ShouldBeFalse();
            };


            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Open);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Payment.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Open));
            };

            private It should_have_the_correct_card_holder_name = () => {
                ApiResult.Value.Payment.Card.HolderName.ShouldEqual(CardPaymentRequest.Payment.Card.HolderName);
            };

            private It should_have_the_correct_card_number = () => {
                string requestCardNumber = CardPaymentRequest.Payment.Card.Number;
                string responseCardNumber = ApiResult.Value.Payment.Card.Number;
                responseCardNumber.Substring(responseCardNumber.Length - 4).ShouldEqual(requestCardNumber.Substring(requestCardNumber.Length - 4));
            };

            private It should_have_the_correct_expiration_month = () => {
                ApiResult.Value.Payment.Card.ExpirationMonth.ShouldEqual(CardPaymentRequest.Payment.Card.ExpirationMonth);
            };

            private It should_have_the_correct_expiration_year = () => {
                ApiResult.Value.Payment.Card.ExpirationYear.ShouldEqual(CardPaymentRequest.Payment.Card.ExpirationYear);
            };
        }
    }
}
