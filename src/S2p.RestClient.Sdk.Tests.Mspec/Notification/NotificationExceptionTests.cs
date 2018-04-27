using System.Net;
using System.Threading.Tasks;
using Machine.Specifications;
using Newtonsoft.Json;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_a_notification_with_invalid_format_arrives
        {
            private static string NotificationBody;
            private static InvalidFormatNotification InvalidFormatNotification;

            private Establish context = () => {
                NotificationBody = "WrongFormat";

                NotificationCallback = new DelegateNotificationCallback
                {
                    InvalidFormatNotificationCallback = async response => {
                        await Task.Delay(1);
                        InvalidFormatNotification = response;
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

            private It should_have_correct_exception_type = () => {
                InvalidFormatNotification.Exception.ShouldBeOfExactType<JsonReaderException>();
            };

            private It should_have_the_corect_notification_body = () => {
                InvalidFormatNotification.NotificationBody.ShouldEqual(NotificationBody);
            };
        }
    }
}
