using System;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.WebsiteManagementService
{
    public partial class When_running_a_complete_website_management_flow
    {
        private static ApiResult<ApiWebsiteManagementResponse> ChangeApiResult;
        private static readonly Lazy<ApiWebsiteManagementRequest> ChangeRequest = new Lazy<ApiWebsiteManagementRequest>(
            () => new MerchantSite
                {
                    URL = "http://www.testupdate.com",
                    Active = true,
                    NotificationURL = "http://www.test.com/notifications_update.node",
                    Alias = "Test website update",
                    Details = new MerchantSiteDetails
                    {
                        Name = "Stichting Smart2Pay update",
                        Country = "PL",
                        City = "Warsaw update",
                        Email = "test@test.com",
                        Address = "BRINK 27C update",
                        BankName = "ING",
                        AccountIBAN = "PL641050008610000023537933350",
                        AccountSWIFT = "1000002353794650",
                        BankSWIFTID = "ABNDDEFFXXX",
                        BankCode = "BTRLRO22",
                        VATNumber = "34206701",
                        RegistrationNumber = "NL 813236460B01",
                        MCC = "7995",
                        MainBusiness = "gaming update"
                    }
                }.ToApiWebsiteManagementRequest());

        private It change_should_have_ok_http_status = () => {
            ChangeApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
        };

        private It change_should_have_the_updated_url = () => {
            ChangeApiResult.Value.MerchantSite.URL.ShouldEqual(ChangeRequest.Value.MerchantSite.URL);
        };

        private It change_should_have_the_updated_notification_url = () => {
            ChangeApiResult.Value.MerchantSite.NotificationURL.ShouldEqual(ChangeRequest.Value.MerchantSite.NotificationURL);
        };

        private It change_should_have_the_updated_alias = () => {
            ChangeApiResult.Value.MerchantSite.Alias.ShouldEqual(ChangeRequest.Value.MerchantSite.Alias);
        };

        private It change_should_have_updated_name = () => {
            ChangeApiResult.Value.MerchantSite.Details.Name.ShouldEqual(ChangeRequest.Value.MerchantSite.Details.Name);
        };

        private It change_should_have_updated_city = () => {
            ChangeApiResult.Value.MerchantSite.Details.City.ShouldEqual(ChangeRequest.Value.MerchantSite.Details.City);
        };

        private It change_should_have_updated_address = () => {
            ChangeApiResult.Value.MerchantSite.Details.Address.ShouldEqual(ChangeRequest.Value.MerchantSite.Details.Address);
        };

        private It change_should_have_updated_main_business = () => {
            ChangeApiResult.Value.MerchantSite.Details.MainBusiness.ShouldEqual(ChangeRequest.Value.MerchantSite.Details.MainBusiness);
        };
    }
}
