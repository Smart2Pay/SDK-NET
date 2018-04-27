using System;
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
        public class When_an_exception_is_thrown_in_notification_callback
        {
            private static string NotificationBody;
            private static ApiCardPayoutResponse Notification;
            private static InvalidFormatNotification ExceptionNotification;

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
                        throw new Exception("Exception in handler");
                    },
                    InvalidFormatNotificationCallback = async response => {
                        await Task.Delay(1);
                        ExceptionNotification = response;
                        return true;
                    }
                };
                NotificationProcessor = new NotificationProcessor(NotificationCallback);
            };

            private Because of = () => {
                Response = NotificationProcessor.ProcessNotificationBodyAsync(NotificationBody).GetAwaiter().GetResult();
            };

            private It should_have_internal_server_error_response = () => {
                Response.ShouldEqual(HttpStatusCode.InternalServerError);
            };

            private It should_have_correct_notification_id = () => {
                Notification.Payout.ID.ShouldEqual(77);
            };

            private It should_not_call_invalid_format_callback = () => {
                ExceptionNotification.ShouldBeNull();
            };
        }
    }
}
