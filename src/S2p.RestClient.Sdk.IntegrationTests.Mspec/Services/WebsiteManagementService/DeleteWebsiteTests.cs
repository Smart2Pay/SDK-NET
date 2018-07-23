using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.WebsiteManagementService
{
    public partial class When_running_a_complete_website_management_flow
    {
        private static ApiResult<ApiWebsiteManagementResponse> DeleteApiResult;

        private It delete_should_have_ok_http_status = () => {
            DeleteApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
        };

        private It delete_should_have_the_correct_url = () => {
            DeleteApiResult.Value.MerchantSite.URL.ShouldEqual(ChangeApiResult.Value.MerchantSite.URL);
        };

        private It delete_should_have_the_correct_notification_url = () => {
            DeleteApiResult.Value.MerchantSite.NotificationURL.ShouldEqual(ChangeApiResult.Value.MerchantSite.NotificationURL);
        };

        private It delete_should_have_the_correct_alias = () => {
            DeleteApiResult.Value.MerchantSite.Alias.ShouldEqual(ChangeApiResult.Value.MerchantSite.Alias);
        };

        private It delete_should_have_the_correct_api_key = () => {
            DeleteApiResult.Value.MerchantSite.ApiKey.ShouldEqual(RegenerateApiKeyApiResult.Value.MerchantSite.ApiKey);
        };

        private It delete_should_have_the_correct_name = () => {
            DeleteApiResult.Value.MerchantSite.Details.Name.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Name);
        };

        private It delete_should_have_the_correct_country = () => {
            DeleteApiResult.Value.MerchantSite.Details.Country.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Country);
        };

        private It delete_should_have_the_correct_city = () => {
            DeleteApiResult.Value.MerchantSite.Details.City.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.City);
        };

        private It delete_should_have_the_correct_email = () => {
            DeleteApiResult.Value.MerchantSite.Details.Email.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Email);
        };

        private It delete_should_have_the_correct_address = () => {
            DeleteApiResult.Value.MerchantSite.Details.Address.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Address);
        };

        private It delete_should_have_the_correct_bank_name = () => {
            DeleteApiResult.Value.MerchantSite.Details.BankName.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.BankName);
        };

        private It delete_should_have_the_correct_account_iban = () => {
            DeleteApiResult.Value.MerchantSite.Details.AccountIBAN.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.AccountIBAN);
        };

        private It delete_should_have_the_correct_account_swift = () => {
            DeleteApiResult.Value.MerchantSite.Details.AccountSWIFT.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.AccountSWIFT);
        };

        private It delete_should_have_the_correct_bank_swift = () => {
            DeleteApiResult.Value.MerchantSite.Details.BankSWIFTID.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.BankSWIFTID);
        };

        private It delete_should_have_the_correct_bank_code = () => {
            DeleteApiResult.Value.MerchantSite.Details.BankCode.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.BankCode);
        };

        private It delete_should_have_the_correct_vat_number = () => {
            DeleteApiResult.Value.MerchantSite.Details.VATNumber.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.VATNumber);
        };

        private It delete_should_have_the_correct_regsistration_number = () => {
            DeleteApiResult.Value.MerchantSite.Details.RegistrationNumber.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.RegistrationNumber);
        };

        private It delete_should_have_the_correct_account_mcc = () => {
            DeleteApiResult.Value.MerchantSite.Details.MCC.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.MCC);
        };

        private It delete_should_have_the_correct_main_business = () => {
            DeleteApiResult.Value.MerchantSite.Details.MainBusiness.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.MainBusiness);
        };
    }
}
