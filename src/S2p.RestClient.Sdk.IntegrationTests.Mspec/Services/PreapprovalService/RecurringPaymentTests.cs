using System;
using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PreapprovallService
{
    partial class PreapprovalServiceTests
    {

        [Subject(typeof(Sdk.Services.PreapprovalService))]
        public class When_creating_a_recurring_payment
        {

            private static ApiResult<ApiPaymentResponse> CreatePaymentResponse;
            private static Sdk.Services.PaymentService PaymentService;
            private static ApiPaymentRequest PaymentRequest;
            private static RecurringPaymentTestData TestData;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PreapprovalService = new PreapprovalService(HttpClient, BaseAddress);
                PaymentService = new Sdk.Services.PaymentService(HttpClient, BaseAddress);
                PaymentRequest = new PaymentRequest
                {
                    PreapprovalID = 9311,
                    MerchantTransactionID = Guid.NewGuid().ToString(),
                    Amount = 100,
                    Currency = "BRL",
                    MethodID = 46,
                    Description = "Recurring payment test SDK",
                    ReturnURL = "http://demo.smart2pay.com/redirect.php",
                    TokenLifetime = 10,
                    Customer = new Customer
                    {
                        Email = "test_user_29302802@testuser.com"
                    },
                    BillingAddress = new Address
                    {
                        Country = "BR"
                    }
                }.ToApiPaymentRequest();
            };

            private Because of = () => {
                TestData = BecauseAsync().GetAwaiter().GetResult();
            };

            private static async Task<RecurringPaymentTestData> BecauseAsync()
            {
                var recurringPaymentTestData = new RecurringPaymentTestData();
                recurringPaymentTestData.Before = await PreapprovalService.GetPreapprovalPaymentsAsync(PaymentRequest.Payment.PreapprovalID.Value);
                await Task.Delay(2000);
                recurringPaymentTestData.PaymentResponse = await PaymentService.CreateRecurrentPaymentAsync(PaymentRequest);
                await Task.Delay(1000);
                recurringPaymentTestData.After = await PreapprovalService.GetPreapprovalPaymentsAsync(PaymentRequest.Payment.PreapprovalID.Value);
                return recurringPaymentTestData;
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { TestData.PaymentResponse.IsSuccess.ShouldBeTrue(); };

            private It should_have_created_http_status = () => {
                TestData.PaymentResponse.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };

            private It should_have_not_null_id = () => { TestData.PaymentResponse.Value.Payment.ID.ShouldNotBeNull(); };

            private It should_have_correct_merchant_transaction_id = () => {
                TestData.PaymentResponse.Value.Payment.MerchantTransactionID.ShouldEqual(PaymentRequest.Payment.MerchantTransactionID);
            };

            private It should_have_correct_site_id = () => {
                TestData.PaymentResponse.Value.Payment.SiteID.ShouldEqual(ServiceTestsConstants.PaymentAuthenticationConfiguration.SiteId);
            };


            private It should_have_correct_preapproval_id = () => {
                TestData.PaymentResponse.Value.Payment.PreapprovalID.ShouldEqual(PaymentRequest.Payment.PreapprovalID);
            };

            private It should_have_the_correct_status_id = () => {
                TestData.PaymentResponse.Value.Payment.Status.ID.ShouldEqual(PaymentStatusDefinition.Success);
            };

            private It should_have_the_correct_status_info = () => {
                TestData.PaymentResponse.Value.Payment.Status.Info.ShouldEqual(nameof(PaymentStatusDefinition.Success));
            };

            private It should_increase_preapproval_payments_count = () => {
                TestData.After.Value.TotalCount.ShouldEqual(TestData.Before.Value.TotalCount + 1);
            };


            public class RecurringPaymentTestData
            {
                public ApiResult<ApiPaymentListResponse> Before { get; set; }
                public ApiResult<ApiPaymentListResponse> After { get; set; }
                public ApiResult<ApiPaymentResponse> PaymentResponse { get; set; }
            }
        }
    }
}
