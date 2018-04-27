using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_an_uknown_notification_type_arrives
        {
            private static string NotificationBody;
            private static UnknownTypeNotification Notification;

            private Establish context = () => {
                NotificationBody = "{" +
                                   "  \"UnknownNotification\": {" +
                                   "    \"ID\": 77," +
                                   "    \"SiteID\": 1010," +
                                   "    \"Created\": \"20161003095114\"," +
                                   "    \"MerchantTransactionID\": \"test_g20\"," +
                                   "    \"Amount\": 1000," +
                                   "    \"Currency\": \"EUR\"," +
                                   "    \"Description\": \"payment product\"," +
                                   "    \"StatementDescriptor\": null," +
                                   "    \"Status\": {" +
                                   "      \"ID\": 2," +
                                   "      \"Info\": \"Success\"," +
                                   "      \"Reasons\": []" +
                                   "    }" +
                                   "  }" +
                                   "}";

                NotificationCallback = new DelegateNotificationCallback
                {
                    UnknownTypeNotificationCallback = async response => {
                        await Task.Delay(1);
                        Notification = response;
                        return true;
                    }
                };
                NotificationProcessor = new NotificationProcessor(NotificationCallback);
            };

            private Because of = () => {
                Response = NotificationProcessor.ProcessNotificationBodyAsync(NotificationBody).GetAwaiter().GetResult();
            };

            private It should_have_bad_request_response = () => {
                Response.ShouldEqual(HttpStatusCode.BadRequest);
            };

            private It should_have_the_correct_notification_body = () => {
                Notification.NotificationBody.ShouldEqual(NotificationBody);
            };
        }
    }
}
