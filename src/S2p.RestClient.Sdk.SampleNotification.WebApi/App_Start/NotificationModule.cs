using Ninject.Modules;
using S2p.RestClient.Sdk.Notifications;
using S2p.RestClient.Sdk.SampleNotification.WebApi.Models;

namespace S2p.RestClient.Sdk.SampleNotification.WebApi.App_Start
{
    public class NotificationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<INotificationProcessor>().To<NotificationProcessor>();
            Bind<INotificationCallback>().To<NotificationCallback>();
        }
    }
}