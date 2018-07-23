using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.WebsiteManagementService
{
    public partial class When_running_a_complete_website_management_flow
    {
        private static ApiResult<ApiWebsiteManagementListResponse> GetWebsiteInfoApiResult;

        private It get_info_should_have_ok_http_status = () => {
            GetWebsiteInfoApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
        };

        private It get_info_should_have_one_website = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites.Count.ShouldEqual(1);
        };

        private It get_info_should_have_the_correct_url = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].URL.ShouldEqual(ChangeApiResult.Value.MerchantSite.URL);
        };

        private It get_info_should_have_the_correct_notification_url = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].NotificationURL.ShouldEqual(ChangeApiResult.Value.MerchantSite.NotificationURL);
        };

        private It get_info_should_have_the_correct_alias = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Alias.ShouldEqual(ChangeApiResult.Value.MerchantSite.Alias);
        };

        private It get_info_should_have_the_correct_api_key = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].ApiKey.ShouldEqual(RegenerateApiKeyApiResult.Value.MerchantSite.ApiKey);
        };

        private It get_info_should_have_the_correct_name = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.Name.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Name);
        };

        private It get_info_should_have_the_correct_country = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.Country.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Country);
        };

        private It get_info_should_have_the_correct_city = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.City.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.City);
        };

        private It get_info_should_have_the_correct_email = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.Email.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Email);
        };

        private It get_info_should_have_the_correct_address = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.Address.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.Address);
        };

        private It get_info_should_have_the_correct_bank_name = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.BankName.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.BankName);
        };

        private It get_info_should_have_the_correct_account_iban = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.AccountIBAN.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.AccountIBAN);
        };

        private It get_info_should_have_the_correct_account_swift = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.AccountSWIFT.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.AccountSWIFT);
        };

        private It get_info_should_have_the_correct_bank_swift = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.BankSWIFTID.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.BankSWIFTID);
        };

        private It get_info_should_have_the_correct_bank_code = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.BankCode.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.BankCode);
        };

        private It get_info_should_have_the_correct_vat_number = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.VATNumber.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.VATNumber);
        };

        private It get_info_should_have_the_correct_regsistration_number = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.RegistrationNumber.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.RegistrationNumber);
        };

        private It get_info_should_have_the_correct_account_mcc = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.MCC.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.MCC);
        };

        private It get_info_should_have_the_correct_main_business = () => {
            GetWebsiteInfoApiResult.Value.MerchantSites[0].Details.MainBusiness.ShouldEqual(ChangeApiResult.Value.MerchantSite.Details.MainBusiness);
        };
    }
}
