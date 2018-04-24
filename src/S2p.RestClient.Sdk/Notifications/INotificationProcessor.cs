using System;
using System.Net;
using S2p.RestClient.Sdk.Entities;

namespace S2p.RestClient.Sdk.Notifications
{
    public interface INotificationProcessor
    {
        HttpStatusCode ProcessNotificationBody(string notificationBody);

        event EventHandler<ApiPaymentResponse> PaymentNotificationEvent;
        event EventHandler<ApiCardPaymentResponse> CardPaymentNotificationEvent;
        event EventHandler<ApiRefundResponse> RefundNotificationEvent;
        event EventHandler<ApiPreapprovalResponse> PreapprovalNotificationEvent;
        event EventHandler<ApiCardPayoutResponse> CardPayoutNotificationEvent;
        event EventHandler<ApiDisputeResponse> DisputeNotificationEvent;
        event EventHandler<InvalidFormatNotification> InvalidFormatNotificationEvent;
        event EventHandler<UnknownTypeNotification> UnknownTypeNotificationEvent;
    }
}
