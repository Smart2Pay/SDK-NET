using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.SampleNotification.WebApi.Controllers
{
    public class NotificationController : ApiController
    {
        private readonly INotificationProcessor _notificationProcessor;

        public NotificationController(INotificationProcessor notificationProcessor)
        {
            _notificationProcessor = notificationProcessor;
        }

        [HttpPost]
        [Route("api/notification/globalpay")]
        public async Task<IHttpActionResult> GlobalPay()
        {
            //do not declare any method parameters, you will neeed to read the request body as a string
            var notificationBody = await Request.Content.ReadAsStringAsync();

            //the notification processor will examine the json string and provide deserialization
            var httpResult = await _notificationProcessor.ProcessNotificationBodyAsync(notificationBody);

            return ResponseMessage(Request.CreateResponse(httpResult));
        }
    }
}
