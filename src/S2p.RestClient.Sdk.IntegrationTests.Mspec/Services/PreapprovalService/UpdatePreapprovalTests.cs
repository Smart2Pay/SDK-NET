using System;
using System.Globalization;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services
{
    partial class PreapprovalServiceTests
    {
        private static string UpdatedDescriptionText => $"SDK Test Preapproval {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";

        [Subject(typeof(Sdk.Services.PreapprovalService))]
        public class When_updating_an_existing_preapproval
        {
            protected static ApiResult<ApiPreapprovalResponse> ApiResult;
            private static ApiPreapprovalRequest PreapprovalRequest;
            private static int PreapprovalId = 9311;
            private const string MerchantPreapprovalId = "jmt3593334";

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PreapprovalService = new Sdk.Services.PreapprovalService(HttpClient, BaseAddress);
                PreapprovalRequest = new ApiPreapprovalRequest
                {
                    Preapproval = new PreapprovalRequest()
                    {
                        MerchantPreapprovalID = MerchantPreapprovalId,
                        Description = UpdatedDescriptionText,
                        ReturnURL = "http://demo.smart2pay.com/redirect.php",
                        MethodID = 46,
                        Customer = new Customer
                        {
                            Email = "test_user_29302802@testuser.com"
                        },
                        BillingAddress = new Address
                        {
                            Country = "BR"
                        }
                    }
                };
            };

            private Because of = () => {
                ApiResult = PreapprovalService.ChangePreapprovalAsync(PreapprovalId,PreapprovalRequest).GetAwaiter().GetResult();
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
                ApiResult.Value.Preapproval.Status.ID.ShouldEqual(PreapprovalStatusDefinition.Open);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Preapproval.Status.Info.ShouldEqual(nameof(PreapprovalStatusDefinition.Open));
            };

            private It should_have_redirect_url_not_null = () => {
                ApiResult.Value.Preapproval.RedirectURL.ShouldNotBeNull();
            };

            private It should_have_correct_currency = () => {
                ApiResult.Value.Preapproval.BillingAddress.Country.ShouldEqual(PreapprovalRequest.Preapproval.BillingAddress.Country);
            };

            private It should_have_correct_description = () => {
                ApiResult.Value.Preapproval.Description.ShouldEqual(PreapprovalRequest.Preapproval.Description);
            };
        }
    }
}
