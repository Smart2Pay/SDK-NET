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
        public class When_a_card_payout_notification_arrives
        {
            private static string NotificationBody;
            private static ApiCardPayoutResponse Notification;

            private Establish context = () => {
                NotificationBody = "{" +
                                   "  \"Payout\": {" +
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
                    CardPayoutNotificationCallback = async response => {
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

            private It should_have_correct_notification_id = () => { Notification.Payout.ID.ShouldEqual(77); };
        }
    }
}
