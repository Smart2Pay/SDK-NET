using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_a_dispute_notification_arrives
        {
            private static string NotificationBody;
            private static ApiDisputeResponse Notification;

            private Establish context = () => {
                NotificationBody = "{" +
                                   "  \"Dispute\": {" +
                                   "    \"ID\": 14," +
                                   "    \"SiteID\": 1000," +
                                   "    \"Created\": \"20180405114938\"," +
                                   "    \"PaymentID\": 169144," +
                                   "    \"MethodID\": 32," +
                                   "    \"Amount\": \"100\"," +
                                   "    \"Currency\": \"EUR\"," +
                                   "    \"Status\": {" +
                                   "      \"ID\": 1," +
                                   "      \"Info\": \"Open\"," +
                                   "      \"Reasons\": null" +
                                   "    }" +
                                   "  }" +
                                   "}";

                NotificationCallback = new DelegateNotificationCallback
                {
                    DisputeNotificationCallback = async response => {
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

            private It should_have_no_content_response = () => { Response.ShouldEqual(HttpStatusCode.NoContent); };

            private It should_have_correct_notification_id = () => { Notification.Dispute.ID.ShouldEqual(14); };
        }
    }
}
