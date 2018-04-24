using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_a_preapproval_notification_arrives
        {
            private static string NotificationBody;
            private static ApiPreapprovalResponse Notification;

            private Establish context = () => {
                NotificationBody = "{" +
                                   "  \"Preapproval\": {" +
                                   "    \"ID\": 4217," +
                                   "    \"Created\": \"20170804065155\"," +
                                   "    \"MethodID\": 46," +
                                   "    \"SiteID\": 30201," +
                                   "    \"MerchantPreapprovalID\": \"s2ptest_g107\"," +
                                   "    \"RecurringPeriod\": 0," +
                                   "    \"PreapprovedMaximumAmount\": null," +
                                   "    \"Currency\": null," +
                                   "    \"ReturnURL\": \"http://demo.smart2pay.com/redirect.php\"," +
                                   "    \"Description\": \"1 year subscription\"," +
                                   "    \"Customer\": {" +
                                   "      \"ID\": 3627," +
                                   "      \"MerchantCustomerID\": null," +
                                   "      \"Email\": \"test_user_83022133@testuser.com\"," +
                                   "      \"FirstName\": \"John\"," +
                                   "      \"LastName\": \"Doe\"," +
                                   "      \"Gender\": null," +
                                   "      \"SocialSecurityNumber\": null," +
                                   "      \"Phone\": \"0765260000\"," +
                                   "      \"Company\": null," +
                                   "      \"DateOfBirth\": null" +
                                   "    }," +
                                   "    \"BillingAddress\": {" +
                                   "      \"ID\": 1257," +
                                   "      \"City\": \"Rio de Janeiro\"," +
                                   "      \"ZipCode\": \"23900-000\"," +
                                   "      \"State\": null," +
                                   "      \"Street\": \"Tonelero\"," +
                                   "      \"StreetNumber\": \"1\"," +
                                   "      \"HouseNumber\": null," +
                                   "      \"HouseExtension\": null," +
                                   "      \"Country\": \"BR\"" +
                                   "    }," +
                                   "    \"Status\": {" +
                                   "      \"ID\": 2," +
                                   "      \"Info\": \"Open\"," +
                                   "      \"Reasons\": null" +
                                   "    }," +
                                   "    \"RedirectURL\": null," +
                                   "    \"MethodOptionID\": 0" +
                                   "  }" +
                                   "}";

                NotificationProcessor = new NotificationProcessor();
                NotificationProcessor.PreapprovalNotificationEvent += (sender, response) => { Notification = response; };
            };

            private Because of = () => { Response = NotificationProcessor.ProcessNotificationBody(NotificationBody); };

            private It should_have_no_content_response = () => { Response.ShouldEqual(HttpStatusCode.NoContent); };

            private It should_have_correct_notification_id = () => { Notification.Preapproval.ID.ShouldEqual(4217); };
        }
    }
}
