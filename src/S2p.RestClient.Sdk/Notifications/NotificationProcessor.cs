using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Notifications
{
    public class NotificationProcessor : INotificationProcessor
    {
        private readonly IReadOnlyDictionary<Type, object> _callbackDictionary;

        public NotificationProcessor(INotificationCallback notificationCallback)
        {
            notificationCallback.ThrowIfNull(nameof(notificationCallback));

            _callbackDictionary = new Dictionary<Type, object>
            {
                {typeof(ApiAlternativePaymentResponse),
                    new Func<ApiAlternativePaymentResponse, Task<bool>>(notificationCallback.AlternativePaymentNotificationCallbackAsync)},
                {typeof(ApiCardPaymentResponse),
                    new Func<ApiCardPaymentResponse, Task<bool>>(notificationCallback.CardPaymentNotificationCallbackAsync)},
                {typeof(ApiRefundResponse),
                    new Func<ApiRefundResponse, Task<bool>>(notificationCallback.RefundNotificationCallbackAsync)},
                {typeof(ApiPreapprovalResponse),
                    new Func<ApiPreapprovalResponse, Task<bool>>(notificationCallback.PreapprovalNotificationCallbackAsync)},
                {typeof(ApiCardPayoutResponse),
                    new Func<ApiCardPayoutResponse, Task<bool>>(notificationCallback.CardPayoutNotificationCallbackAsync)},
                {typeof(ApiDisputeResponse),
                    new Func<ApiDisputeResponse, Task<bool>>(notificationCallback.DisputeNotificationCallbackAsync)},
                {typeof(UnknownTypeNotification),
                    new Func<UnknownTypeNotification, Task<bool>>(notificationCallback.UnknownTypeNotificationCallbackAsync)},
                {typeof(InvalidFormatNotification),
                    new Func<InvalidFormatNotification, Task<bool>>(notificationCallback.InvalidFormatNotificationCallbackAsync)},
            };
        }

        public async Task<HttpStatusCode> ProcessNotificationBodyAsync(string notificationBody)
        {
            try
            {
                var notification = JObject.Parse(notificationBody);
                var notificationType = notification.First.Path;

                bool notificationResult;
                switch (notificationType)
                {
                    case NotificationType.Payment:
                        notificationResult =  await OnPaymentNotification(notification, notificationBody);
                        break;
                    case NotificationType.Refund:
                        notificationResult = await OnNotification<ApiRefundResponse>(notificationBody);
                        break;
                    case NotificationType.Dispute:
                        notificationResult = await OnNotification<ApiDisputeResponse>(notificationBody);
                        break;
                    case NotificationType.Payout:
                        notificationResult = await OnNotification<ApiCardPayoutResponse>(notificationBody);
                        break;
                    case NotificationType.Preapproval:
                        notificationResult = await OnNotification<ApiPreapprovalResponse>(notificationBody);
                        break;
                    default:
                        await OnNotification(new UnknownTypeNotification {NotificationBody = notificationBody});
                        return HttpStatusCode.BadRequest;
                }

                return notificationResult ? HttpStatusCode.NoContent : HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                var invalidFormatNotification = new InvalidFormatNotification
                {
                    Exception = ex,
                    NotificationBody = notificationBody
                };
                await OnNotification(invalidFormatNotification);
                return HttpStatusCode.BadRequest;
            }
        }

        private async Task<bool> OnNotification<T>(T notification)
        {
            if (!_callbackDictionary.ContainsKey(typeof(T))) { return false; }
            var callback = (_callbackDictionary[typeof(T)] as Func<T, Task<bool>>).ValueIfNull(() => n => false.ToAwaitable());

            try
            {
                return await callback(notification);
            }
            catch
            {
                return false;
            }
        }

        private Task<bool> OnNotification<T>(string notificationBody)
        {
            var notification = JsonConvert.DeserializeObject<T>(notificationBody);
            return OnNotification(notification);
        }

        private Task<bool> OnPaymentNotification(JObject notification, string notificationBody)
        {
            const string methodIdText = "MethodID";

            var methodId = notification[NotificationType.Payment][methodIdText].Value<int>();
            return methodId == CardConstants.MethodId 
                ? OnNotification<ApiCardPaymentResponse>(notificationBody) 
                : OnNotification<ApiAlternativePaymentResponse>(notificationBody);
        }
    }
}
