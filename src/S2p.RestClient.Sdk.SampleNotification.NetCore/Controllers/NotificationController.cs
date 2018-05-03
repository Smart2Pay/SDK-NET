using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using S2p.RestClient.Sdk.Notifications;
using S2p.RestClient.Sdk.SampleNotification.NetCore.Infrastructure;

namespace S2p.RestClient.Sdk.SampleNotification.NetCore.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationProcessor _notificationProcessor;

        public NotificationController(INotificationProcessor notificationProcessor)
        {
            _notificationProcessor = notificationProcessor;
        }

        [HttpPost]
        [Route("api/notification/globalpay")]
        public async Task<IActionResult> GlobalPay()
        {
            //do not declare any method parameters, you will neeed to read the request body as a string
            var notificationBody = await Request.ReadBodyAsStringAsync();

            //the notification processor will examine the json string and provide deserialization
            var httpResult = await _notificationProcessor.ProcessNotificationBodyAsync(notificationBody);

            return StatusCode((int)httpResult);
        }
    }
}
