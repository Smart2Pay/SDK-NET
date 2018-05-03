using Microsoft.Extensions.DependencyInjection;
using S2p.RestClient.Sdk.Notifications;
using S2p.RestClient.Sdk.SampleNotification.NetCore.Models;

namespace S2p.RestClient.Sdk.SampleNotification.NetCore.Infrastructure
{
    public static class NotificationModule
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<INotificationProcessor, NotificationProcessor>();
            services.AddTransient<INotificationCallback, NotificationCallback>();
        }
    }
}
