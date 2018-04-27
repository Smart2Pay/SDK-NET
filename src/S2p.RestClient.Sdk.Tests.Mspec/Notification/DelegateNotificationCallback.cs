using System;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Notifications;

namespace S2p.RestClient.Sdk.Tests.Mspec.Notification
{
    public class DelegateNotificationCallback : INotificationCallback
    {
        public Func<ApiCardPaymentResponse, Task<bool>> CardPaymentNotificationCallback { get; set; }
        public Task<bool> CardPaymentNotificationCallbackAsync(ApiCardPaymentResponse cardPaymentNotification)
        {
            return CardPaymentNotificationCallback(cardPaymentNotification);
        }

        public Func<ApiCardPayoutResponse, Task<bool>> CardPayoutNotificationCallback { get; set; }
        public Task<bool> CardPayoutNotificationCallbackAsync(ApiCardPayoutResponse cardPayoutNotification)
        {
            return CardPayoutNotificationCallback(cardPayoutNotification);
        }

        public Func<ApiDisputeResponse, Task<bool>> DisputeNotificationCallback { get; set; }
        public Task<bool> DisputeNotificationCallbackAsync(ApiDisputeResponse disputeNotification)
        {
            return DisputeNotificationCallback(disputeNotification);
        }

        public Func<InvalidFormatNotification, Task<bool>> InvalidFormatNotificationCallback { get; set; }
        public Task<bool> InvalidFormatNotificationCallbackAsync(InvalidFormatNotification invalidFormatNotification)
        {
            return InvalidFormatNotificationCallback(invalidFormatNotification);
        }

        public Func<ApiPaymentResponse, Task<bool>> PaymentNotificationCallback { get; set; }
        public Task<bool> PaymentNotificationCallbackAsync(ApiPaymentResponse paymentNotification)
        {
            return PaymentNotificationCallback(paymentNotification);
        }

        public Func<ApiPreapprovalResponse, Task<bool>> PreapprovalNotificationCallback { get; set; }
        public Task<bool> PreapprovalNotificationCallbackAsync(ApiPreapprovalResponse preapprovalNotification)
        {
            return PreapprovalNotificationCallback(preapprovalNotification);
        }

        public Func<ApiRefundResponse, Task<bool>> RefundNotificationCallback { get; set; }
        public Task<bool> RefundNotificationCallbackAsync(ApiRefundResponse refundNotification)
        {
            return RefundNotificationCallback(refundNotification);
        }

        public Func<UnknownTypeNotification, Task<bool>> UnknownTypeNotificationCallback { get; set; }
        public Task<bool> UnknownTypeNotificationCallbackAsync(UnknownTypeNotification unknownTypeNotification)
        {
            return UnknownTypeNotificationCallback(unknownTypeNotification);
        }
    }
}
