using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.AlternativePaymentService
{
    partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.AlternativePaymentService))]
        public class When_creating_a_payment_with_wrong_country
        {
            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                _alternativePaymentService = new Sdk.Services.AlternativePaymentService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                PaymentRequest = new PaymentRequest
                {
                    MerchantTransactionID = MerchantTransactionID,
                    Amount = 11,
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
                        Country = "CNN"
                    }
                }.ToApiPaymentRequest();

                ApiResult = _alternativePaymentService.CreatePaymentAsync(PaymentRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_fail = () => {
                ApiResult.IsSuccess.ShouldBeFalse();
            };

            private It should_have_validation_exception = () => {
                ApiResult.Exception.ShouldBeOfExactType<ValidationException>();
            };

            private It shoudl_have_correct_error_message = () => {
                ApiResult.Exception.Message.ShouldEqual("Address-Country:Invalid Country;");
            };
        }
    }
}
