using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Notifications
{
    public class NotificationProcessor : INotificationProcessor
    {
        #region event EventHandler<ApiPaymentResponse> PaymentNotificationEvent

        public event EventHandler<ApiPaymentResponse> PaymentNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiPaymentResponse)] = 
                        (EventHandler<ApiPaymentResponse>)_notificationTypes[typeof(ApiPaymentResponse)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiPaymentResponse)] =
                        (EventHandler<ApiPaymentResponse>)_notificationTypes[typeof(ApiPaymentResponse)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<ApiCardPaymentResponse> CardPaymentNotificationEvent

        public event EventHandler<ApiCardPaymentResponse> CardPaymentNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiCardPaymentResponse)] =
                        (EventHandler<ApiCardPaymentResponse>) _notificationTypes[typeof(ApiCardPaymentResponse)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiCardPaymentResponse)] =
                        (EventHandler<ApiCardPaymentResponse>) _notificationTypes[typeof(ApiCardPaymentResponse)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<ApiRefundResponse> RefundNotificationEvent

        public event EventHandler<ApiRefundResponse> RefundNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiRefundResponse)] =
                        (EventHandler<ApiRefundResponse>)_notificationTypes[typeof(ApiRefundResponse)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiRefundResponse)] =
                        (EventHandler<ApiRefundResponse>)_notificationTypes[typeof(ApiRefundResponse)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<ApiPreapprovalResponse> PreapprovalNotificationEvent

        public event EventHandler<ApiPreapprovalResponse> PreapprovalNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiPreapprovalResponse)] =
                        (EventHandler<ApiPreapprovalResponse>)_notificationTypes[typeof(ApiPreapprovalResponse)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiPreapprovalResponse)] =
                        (EventHandler<ApiPreapprovalResponse>)_notificationTypes[typeof(ApiPreapprovalResponse)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<ApiCardPayoutResponse> CardPayoutNotificationEvent

        public event EventHandler<ApiCardPayoutResponse> CardPayoutNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiCardPayoutResponse)] =
                        (EventHandler<ApiCardPayoutResponse>)_notificationTypes[typeof(ApiCardPayoutResponse)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiCardPayoutResponse)] =
                        (EventHandler<ApiCardPayoutResponse>)_notificationTypes[typeof(ApiCardPayoutResponse)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<ApiDisputeResponse> DisputeNotificationEvent

        public event EventHandler<ApiDisputeResponse> DisputeNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiDisputeResponse)] =
                        (EventHandler<ApiDisputeResponse>)_notificationTypes[typeof(ApiDisputeResponse)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(ApiDisputeResponse)] =
                        (EventHandler<ApiDisputeResponse>)_notificationTypes[typeof(ApiDisputeResponse)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<InvalidFormatNotification> InvalidFormatNotificationEvent

        public event EventHandler<InvalidFormatNotification> InvalidFormatNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(InvalidFormatNotification)] =
                        (EventHandler<InvalidFormatNotification>)_notificationTypes[typeof(InvalidFormatNotification)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(InvalidFormatNotification)] =
                        (EventHandler<InvalidFormatNotification>)_notificationTypes[typeof(InvalidFormatNotification)] - value;
                }
            }
        }

        #endregion

        #region event EventHandler<UnknownTypeNotification> UnknownTypeNotificationEvent
        
        public event EventHandler<UnknownTypeNotification> UnknownTypeNotificationEvent
        {
            add
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(UnknownTypeNotification)] =
                        (EventHandler<UnknownTypeNotification>)_notificationTypes[typeof(UnknownTypeNotification)] + value;
                }
            }
            remove
            {
                lock (_lock)
                {
                    _notificationTypes[typeof(UnknownTypeNotification)] =
                        (EventHandler<UnknownTypeNotification>)_notificationTypes[typeof(UnknownTypeNotification)] - value;
                }
            }
        }

        #endregion

        private readonly Dictionary<Type, Delegate> _notificationTypes;
        private readonly object _lock = new object();

        public NotificationProcessor()
        {
            _notificationTypes = new Dictionary<Type, Delegate>
            {
                {typeof(ApiPaymentResponse), null},
                {typeof(ApiCardPaymentResponse), null},
                {typeof(ApiRefundResponse), null },
                {typeof(ApiPreapprovalResponse), null },
                {typeof(ApiCardPayoutResponse), null },
                {typeof(ApiDisputeResponse), null },
                {typeof(UnknownTypeNotification), null },
                {typeof(InvalidFormatNotification), null }
            };
        }

        public HttpStatusCode ProcessNotificationBody(string notificationBody)
        {
            try
            {
                var jObject = JObject.Parse(notificationBody);
                var notificationType = jObject.First.Path;

                bool notificationResult;
                switch (notificationType)
                {
                    case NotificationType.Payment:
                        notificationResult = OnPaymentNotification(jObject, notificationBody);
                        break;
                    case NotificationType.Refund:
                        notificationResult = OnNotification<ApiRefundResponse>(notificationBody);
                        break;
                    case NotificationType.Dispute:
                        notificationResult = OnNotification<ApiDisputeResponse>(notificationBody);
                        break;
                    case NotificationType.Payout:
                        notificationResult = OnNotification<ApiCardPayoutResponse>(notificationBody);
                        break;
                    case NotificationType.Preapproval:
                        notificationResult = OnNotification<ApiPreapprovalResponse>(notificationBody);
                        break;
                    default:
                        OnNotification(new UnknownTypeNotification {NotificationBody = notificationBody});
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
                OnNotification(invalidFormatNotification);
                return HttpStatusCode.BadRequest;
            }
        }

        private bool OnNotification<T>(T eventArgs)
        {
            Delegate[] invocationList;
            lock (_lock)
            {
                if (!_notificationTypes.ContainsKey(typeof(T))) {return false;}
                var eventHandler = _notificationTypes[typeof(T)] as EventHandler<T>;
                invocationList = eventHandler?.GetInvocationList();
            }

            var resultList = new List<bool>();
            foreach (var invocation in invocationList.ValueIfNull(() => new Delegate[0]))
            {
                var handler = invocation as EventHandler<T>;
                try
                {
                    handler?.Invoke(this, eventArgs);
                    resultList.Add(true);
                }
                catch
                {
                    resultList.Add(false);
                }
            }

            var result = resultList.Count != 0 && resultList.TrueForAll(r => r);
            return result;
        }

        private bool OnNotification<T>(string notificationBody)
        {
            var notification = JsonConvert.DeserializeObject<T>(notificationBody);
            return OnNotification(notification);
        }

        private bool OnPaymentNotification(JObject jObject, string notificationBody)
        {
            const string methodIdText = "MethodID";

            var methodId = jObject[NotificationType.Payment][methodIdText].Value<int>();
            return methodId == CardConstants.MethodId 
                ? OnNotification<ApiCardPaymentResponse>(notificationBody) 
                : OnNotification<ApiPaymentResponse>(notificationBody);
        }
    }
}
