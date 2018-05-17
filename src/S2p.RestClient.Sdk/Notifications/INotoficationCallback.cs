using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;

namespace S2p.RestClient.Sdk.Notifications
{
    public interface INotificationCallback
    {
        Task<bool> AlternativePaymentNotificationCallbackAsync(ApiAlternativePaymentResponse alternativePaymentNotification);
        Task<bool> CardPaymentNotificationCallbackAsync(ApiCardPaymentResponse cardPaymentNotification);
        Task<bool> RefundNotificationCallbackAsync(ApiRefundResponse refundNotification);
        Task<bool> PreapprovalNotificationCallbackAsync(ApiPreapprovalResponse preapprovalNotification);
        Task<bool> CardPayoutNotificationCallbackAsync(ApiCardPayoutResponse cardPayoutNotification);
        Task<bool> DisputeNotificationCallbackAsync(ApiDisputeResponse disputeNotification);
        Task<bool> InvalidFormatNotificationCallbackAsync(InvalidFormatNotification invalidFormatNotification);
        Task<bool> UnknownTypeNotificationCallbackAsync(UnknownTypeNotification unknownTypeNotification);
    }
}
