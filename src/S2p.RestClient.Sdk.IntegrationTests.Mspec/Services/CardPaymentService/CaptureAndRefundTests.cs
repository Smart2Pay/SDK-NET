using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    partial class CardPaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_a_card_payment_is_captured_and_then_refunded
        {
            private static ApiResult<ApiCardPaymentResponse> CaptureResult;
            private static ApiResult<ApiRefundResponse> RefundResult;
            private static IRefundService RefundService;
            private static ApiRefundRequest RefundRequest;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                RefundService = new Sdk.Services.RefundService(HttpClient, BaseAddress);
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                CardPaymentRequest = new ApiCardPaymentRequest
                {
                    Payment = new CardPaymentRequest
                    {
                        MerchantTransactionID = MerchantTransactionID,
                        Amount = 9000,
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
                        Capture = false,
                        Retry = false,
                        GenerateCreditCardToken = false,
                        PaymentTokenLifetime = 5
                    }
                };
                RefundRequest = new ApiRefundRequest
                {
                    Refund = new RefundRequest()
                    {
                        Amount = CardPaymentRequest.Payment.Amount,
                        MerchantTransactionID = MerchantTransactionID
                    }
                };
            };

            private Because of = () => {
                RefundResult = BecauseAsync().GetAwaiter().GetResult();
            };

            private static async Task<ApiResult<ApiRefundResponse>> BecauseAsync()
            {
                ApiResult = await CardPaymentService.InitiatePaymentAsync(CardPaymentRequest);
                CaptureResult = await CardPaymentService.CapturePaymentAsync(ApiResult.Value.Payment.ID.Value);
                return await RefundService.CreateRefundAsync(ApiResult.Value.Payment.ID.Value, RefundRequest);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_ok_status_code_for_capture = () => {
                CaptureResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_created_status_code_for_refund = () => {
                RefundResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };


            private It should_have_the_same_merchant_transaction_id = () => {
                CaptureResult.Value.Payment.MerchantTransactionID.ShouldEqual(CardPaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_the_same_refund_merchant_transaction_id = () => {
                RefundResult.Value.Refund.MerchantTransactionID.ShouldEqual(RefundRequest.Refund.MerchantTransactionID);
            };

            private It should_have_the_correct_amount = () => {
                CaptureResult.Value.Payment.Amount.ShouldEqual(CardPaymentRequest.Payment.Amount);
            };

            private It should_have_the_correct_refund_amount = () => {
                RefundResult.Value.Refund.Amount.ShouldEqual(RefundRequest.Refund.Amount);
            };

            private It should_have_the_same_currency = () => {
                CaptureResult.Value.Payment.Currency.ShouldEqual(CardPaymentRequest.Payment.Currency);
            };

            private It should_have_the_same_currency_for_refund = () => {
                RefundResult.Value.Refund.Currency.ShouldEqual(CardPaymentRequest.Payment.Currency);
            };

            private It should_have_the_correct_status_id_after_initiate = () => {
                ApiResult.Value.Payment.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Authorized);
            };

            private It should_have_the_correct_status_info_after_initiate = () => {
                ApiResult.Value.Payment.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Authorized));
            };

            private It should_have_the_correct_status_id_after_capture = () => {
                CaptureResult.Value.Payment.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Captured);
            };

            private It should_have_the_correct_status_info_after_capture = () => {
                CaptureResult.Value.Payment.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Captured));
            };

            private It should_have_correct_status_id_after_refund = () => {
                RefundResult.Value.Refund.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Success);
            };

            private It should_have_the_correct_status_info_after_refund = () => {
                RefundResult.Value.Refund.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Success));
            };
        }
    }
}
