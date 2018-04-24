using System;

namespace S2p.RestClient.Sdk.Notifications
{
    public class InvalidFormatNotification
    {
        public Exception Exception { get; internal set; }
        public string NotificationBody { get; internal set; }
    }
}
