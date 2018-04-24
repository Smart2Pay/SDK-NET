using System;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_an_exception_is_thrown_in_notification_handler
        {
            private static string NotificationBody;
            private static ApiCardPayoutResponse Notification;
            private static InvalidFormatNotification _exceptionInFormatNotificationErrorHandler;

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
                NotificationProcessor.CardPayoutNotificationEvent += (sender, response) => {
                    Notification = response;
                    throw new Exception("Exception in handler");
                };
                NotificationProcessor.InvalidFormatNotificationEvent += (sender, exception) => {
                    _exceptionInFormatNotificationErrorHandler = exception;
                };
            };

            private Because of = () => {
                Response = NotificationProcessor.ProcessNotificationBody(NotificationBody);
            };

            private It should_have_internal_server_error_response = () => {
                Response.ShouldEqual(HttpStatusCode.InternalServerError);
            };

            private It should_have_correct_notification_id = () => {
                Notification.Payout.ID.ShouldEqual(77);
            };

            private It should_not_raise_notification_error_event = () => {
                _exceptionInFormatNotificationErrorHandler.ShouldBeNull();
            };
        }
    }
}
