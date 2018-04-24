using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public partial class NotificationTests
    {
        [Subject(typeof(NotificationProcessor))]
        public class When_a_card_payment_notification_arrives
        {
            private static string NotificationBody;
            private static ApiCardPaymentResponse Notification;

            private Establish context = () => {
                NotificationBody = "{" +
                                   "  \"Payment\": {" +
                                   "    \"ID\": 202242," +
                                   "    \"ClientIP\": null," +
                                   "    \"SkinID\": null," +
                                   "    \"Created\": \"20161205093117\"," +
                                   "    \"MerchantTransactionID\": \"s2ptest_h12\"," +
                                   "    \"OriginatorTransactionID\": \"100_a\"," +
                                   "    \"Amount\": 2000," +
                                   "    \"Currency\": \"EUR\"," +
                                   "    \"CapturedAmount\": 2000," +
                                   "    \"ReturnURL\": \"http://demo.smart2pay.com/redirect.php\"," +
                                   "    \"Description\": \"payment product\"," +
                                   "    \"StatementDescriptor\": \"bank statement message\"," +
                                   "    \"MethodID\": 6," +
                                   "    \"MethodOptionID\": null," +
                                   "    \"SiteID\": 1010," +
                                   "    \"NotificationDateTime\": null," +
                                   "    \"Customer\": {" +
                                   "      \"ID\": 115," +
                                   "      \"MerchantCustomerID\": \"null\"," +
                                   "      \"Email\": \"customer@test.com\"," +
                                   "      \"FirstName\": \"John\"," +
                                   "      \"LastName\": \"Doe\"," +
                                   "      \"Gender\": \"1\"," +
                                   "      \"SocialSecurityNumber\": \"45908-28324\"," +
                                   "      \"Phone\": \"0744-783322\"," +
                                   "      \"Company\": \"S2P\"" +
                                   "    }," +
                                   "    \"BillingAddress\": {" +
                                   "      \"ID\": 253," +
                                   "      \"City\": \"Iasi\"," +
                                   "      \"ZipCode\": \"7000-49\"," +
                                   "      \"State\": \"Iasi\"," +
                                   "      \"Street\": \"Sf Lazar\"," +
                                   "      \"StreetNumber\": \"37\"," +
                                   "      \"HouseNumber\": \"5A\"," +
                                   "      \"HouseExtension\": \"-\"," +
                                   "      \"Country\": \"RO\"" +
                                   "    }," +
                                   "    \"ShippingAddress\": {" +
                                   "      \"ID\": 87," +
                                   "      \"City\": \"Iasi\"," +
                                   "      \"ZipCode\": \"700049\"," +
                                   "      \"State\": \"Iasi\"," +
                                   "      \"Street\": \"Sf Lazar\"," +
                                   "      \"StreetNumber\": \"37\"," +
                                   "      \"HouseNumber\": \"-\"," +
                                   "      \"HouseExtension\": \"-\"," +
                                   "      \"Country\": \"RO\"" +
                                   "    }," +
                                   "    \"Articles\": null," +
                                   "    \"Card\": {" +
                                   "      \"HolderName\": \"John Doe\"," +
                                   "      \"Number\": \"VISA-1111\"," +
                                   "      \"ExpirationMonth\": \"02\"," +
                                   "      \"ExpirationYear\": \"2018\"" +
                                   "    }," +
                                   "    \"CreditCardToken\": {" +
                                   "      \"Value\": \"6BEBF42B0E43D3BFD360DFB5EFF9D96D\"" +
                                   "    }," +
                                   "    \"Status\": {" +
                                   "      \"ID\": 11," +
                                   "      \"Info\": \"Captured\"," +
                                   "      \"Reasons\": []" +
                                   "    }," +
                                   "    \"MethodTransactionID\": null," +
                                   "    \"PaymentTokenLifetime\": null," +
                                   "    \"Capture\": true," +
                                   "    \"Retry\": true," +
                                   "    \"RedirectURL\": null," +
                                   "    \"3DSecure\": null" +
                                   "  }" +
                                   "}";

                NotificationProcessor = new NotificationProcessor();
                NotificationProcessor.CardPaymentNotificationEvent += (sender, response) => { Notification = response; };
            };

            private Because of = () => { Response = NotificationProcessor.ProcessNotificationBody(NotificationBody); };

            private It should_have_no_content_response = () => { Response.ShouldEqual(HttpStatusCode.NoContent); };

            private It should_have_correct_notification_id = () => { Notification.Payment.ID.ShouldEqual(202242); };
        }
    }
}
