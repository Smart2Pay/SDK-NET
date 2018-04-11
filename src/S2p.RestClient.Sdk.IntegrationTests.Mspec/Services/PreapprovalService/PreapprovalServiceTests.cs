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

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services
{
    public partial class PreapprovalServiceTests
    {
        private static IPreapprovalService PreapprovalService;
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;
        private static readonly Uri BaseAddress = new Uri(ServiceTestsConstants.PaymentBaseUrl);
        private const string DescriptionText = "SDK Test Preapproval";
        private static string MerchantTransactionId => Guid.NewGuid().ToString();

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.PaymentAuthenticationConfiguration);
        }

        [Subject(typeof(Sdk.Services.PreapprovalService))]
        public class When_creating_a_preapproval
        {
            protected static ApiResult<ApiPreapprovalResponse> ApiResult;
            private static ApiPreapprovalRequest PreapprovalRequest;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PreapprovalService = new Sdk.Services.PreapprovalService(HttpClient, BaseAddress);
                PreapprovalRequest = new ApiPreapprovalRequest
                {
                    Preapproval = new PreapprovalRequest()
                    {
                        MerchantPreapprovalID = Guid.NewGuid().ToString(),
                        Description = DescriptionText,
                        ReturnURL = "http://demo.smart2pay.com/redirect.php",
                        MethodID = 46,
                        Customer = new Customer
                        {
                           Email = $"test_user_29302802{Guid.NewGuid().ToString()}@testuser.com"
                        },
                        BillingAddress = new Address
                        {
                            Country = "BR"
                        }
                    }
                };
            };

            private Because of = () => {
                ApiResult = PreapprovalService.CreatePreapprovalAsync(PreapprovalRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_created_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.Preapproval.ID.ShouldNotBeNull(); };

            private It should_have_correct_merchant_preapproval_id = () => {
                ApiResult.Value.Preapproval.MerchantPreapprovalID.ShouldEqual(PreapprovalRequest.Preapproval.MerchantPreapprovalID);
            };

            private It should_have_correct_site_id = () => {
                ApiResult.Value.Preapproval.SiteID.ShouldEqual(ServiceTestsConstants.PaymentAuthenticationConfiguration.SiteId);
            };

            private It should_have_correct_method_id = () => {
                ApiResult.Value.Preapproval.MethodID.ShouldEqual(PreapprovalRequest.Preapproval.MethodID);
            };

            private It should_have_correct_return_url = () => {
                ApiResult.Value.Preapproval.ReturnURL.ShouldEqual(PreapprovalRequest.Preapproval.ReturnURL);
            };

            private It should_have_customer_id_not_null = () => {
                ApiResult.Value.Preapproval.Customer.ID.ShouldNotBeNull();
            };

            private It should_have_correct_customer_email = () => {
                ApiResult.Value.Preapproval.Customer.Email.ShouldEqual(PreapprovalRequest.Preapproval.Customer.Email);
            };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.Preapproval.Status.ID.ShouldEqual(PreapprovalStatusDefinition.Pending);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Preapproval.Status.Info.ShouldEqual(nameof(PreapprovalStatusDefinition.Pending));
            };

            private It should_have_redirect_url_not_null = () => {
                ApiResult.Value.Preapproval.RedirectURL.ShouldNotBeNull();
            };

            private It should_have_correct_currency = () => {
                ApiResult.Value.Preapproval.BillingAddress.Country.ShouldEqual(PreapprovalRequest.Preapproval.BillingAddress.Country);
            };
        }
    }
}
