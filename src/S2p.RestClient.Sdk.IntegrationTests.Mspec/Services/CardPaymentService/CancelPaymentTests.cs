using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    partial class CardPaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_a_card_payment_is_cancelled
        {
            private static ApiResult<ApiCardPaymentResponse> CancelResult;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                CardPaymentRequest = new CardPaymentRequest
                {
                    MerchantTransactionID = MerchantTransactionID,
                    Amount = 9000,
                    Currency = "USD",
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    Description = DescriptionText,
                    StatementDescriptor = "bank statement message",
                    Card = new CardDetailsRequest
                    {
                        HolderName = "John Doe",
                        Number = "4111111111111111",
                        ExpirationMonth = "02",
                        ExpirationYear = "2022",
                        SecurityCode = "312"
                    },
                    BillingAddress = new Address
                    {
                        City = "Iasi",
                        ZipCode = "7000-49",
                        State = "Iasi",
                        Street = "Sf Lazar",
                        StreetNumber = "37",
                        HouseNumber = "5A",
                        HouseExtension = "-",
                        Country = "BR"
                    },
                    Capture = false,
                    Retry = false,
                    GenerateCreditCardToken = false,
                    PaymentTokenLifetime = 5
                }.ToApiCardPaymentRequest();
            };

            private Because of = () => {
                CancelResult = BecauseAsync().GetAwaiter().GetResult();
            };

            private static async Task<ApiResult<ApiCardPaymentResponse>> BecauseAsync()
            {
                ApiResult = await CardPaymentService.InitiatePaymentAsync(CardPaymentRequest);
                return await CardPaymentService.CancelPaymentAsync(ApiResult.Value.Payment.ID.Value);
            }

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

            private It should_have_the_correct_status_id = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Authorized);
            };

            private It should_have_the_correct_status_info = () => {
                ApiResult.Value.Payment.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Authorized));
            };

            private It should_have_correct_status_id_after_cancel = () => {
                CancelResult.Value.Payment.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Cancelled);
            };

            private It should_have_the_correct_status_info_after_cancel = () => {
                CancelResult.Value.Payment.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Cancelled));
            };

            private It should_have_ok_status_code_for_cancel = () => {
                CancelResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

        }
    }
}
