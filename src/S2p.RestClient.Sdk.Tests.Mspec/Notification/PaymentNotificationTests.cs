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
        public class When_a_payment_notification_arrives
        {
            private static string NotificationBody;
            private static ApiAlternativePaymentResponse Notification;

            private Establish context = () => {
                NotificationBody = "{" +
                                   "  \"Payment\": {" +
                                   "    \"ID\": 3005389," +
                                   "    \"SkinID\": null," +
                                   "    \"ClientIP\": null," +
                                   "    \"Created\": \"20170802145304\"," +
                                   "    \"MerchantTransactionID\": \"s2ptest_g28\"," +
                                   "    \"OriginatorTransactionID\": null," +
                                   "    \"Amount\": 100," +
                                   "    \"Currency\": \"EUR\"," +
                                   "    \"ReturnURL\": \"http://demo.smart2pay.com/redirect.php\"," +
                                   "    \"Description\": \"\"," +
                                   "    \"MethodID\": 2," +
                                   "    \"MethodOptionID\": null," +
                                   "    \"IncludeMethodIDs\": null," +
                                   "    \"ExcludeMethodIDs\": null," +
                                   "    \"PrioritizeMethodIDs\": null," +
                                   "    \"SiteID\": 30201," +
                                   "    \"NotificationDateTime\": null," +
                                   "    \"Customer\": null," +
                                   "    \"BillingAddress\": {" +
                                   "      \"ID\": 309," +
                                   "      \"City\": null," +
                                   "      \"ZipCode\": null," +
                                   "      \"State\": null," +
                                   "      \"Street\": null," +
                                   "      \"StreetNumber\": null," +
                                   "      \"HouseNumber\": null," +
                                   "      \"HouseExtension\": null," +
                                   "      \"Country\": \"NL\"" +
                                   "    }," +
                                   "    \"ShippingAddress\": null," +
                                   "    \"Articles\": null," +
                                   "    \"Details\": {" +
                                   "      \"AccountNumber\": \"NL53INGB0654422370\"," +
                                   "      \"AccountHolder\": \"Hr E G H Küppers en/of MW M.J. Küppers-Veeneman\"," +
                                   "      \"IBAN\": null," +
                                   "      \"BIC\": null," +
                                   "      \"PrepaidCard\": null," +
                                   "      \"PrepaidCardPIN\": null," +
                                   "      \"SerialNumbers\": null," +
                                   "      \"Wallet\": null," +
                                   "      \"ReferenceNumber\": null," +
                                   "      \"PayerCountry\": null," +
                                   "      \"PayerEmail\": null," +
                                   "      \"PayerPhone\": null" +
                                   "    }," +
                                   "    \"ReferenceDetails\": null," +
                                   "    \"CustomParameters\": null," +
                                   "    \"PreapprovalID\": null," +
                                   "    \"Status\": {" +
                                   "      \"ID\": 2," +
                                   "      \"Info\": \"Success\"," +
                                   "      \"Reasons\": null" +
                                   "    }," +
                                   "    \"MethodTransactionID\": null," +
                                   "    \"TokenLifetime\": 1," +
                                   "    \"Capture\": null," +
                                   "    \"PreapprovalDetails\": null," +
                                   "    \"RedirectURL\": \"https://apitest.smart2pay.com/Home?PaymentToken=AC9EBF3D44C234B4A4D258C07BB6D1B7.3005389\"" +
                                   "  }" +
                                   "}";

                NotificationCallback = new DelegateNotificationCallback
                {
                    PaymentNotificationCallback = async response => {
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

            private It should_have_correct_notification_id = () => { Notification.Payment.ID.ShouldEqual(3005389); };
        }
    }
}
