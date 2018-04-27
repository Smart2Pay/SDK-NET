using System.Net;
using System.Threading.Tasks;

namespace S2p.RestClient.Sdk.Notifications
{
    public interface INotificationProcessor
    {
        Task<HttpStatusCode> ProcessNotificationBodyAsync(string notificationBody);
    }
}
