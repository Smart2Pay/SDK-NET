using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_no_event_handler_is_registered_when_a_notification_arrives
        {
            private static string NotificationBody;

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

                NotificationProcessor = new NotificationProcessor();
            };

            private Because of = () => { Response = NotificationProcessor.ProcessNotificationBody(NotificationBody); };

            private It should_have_internal_server_error_response = () => { Response.ShouldEqual(HttpStatusCode.InternalServerError); };
        }
    }
}
