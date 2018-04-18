using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface ICardPaymentService
    {
        Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(long paymentId);

        Task<ApiResult<ApiCardPaymentResponse>> GetPaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> GetPaymentAsync(long paymentId);
        Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync();

        Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CardPaymentsFilter filter,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CardPaymentsFilter filter);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId,
            string idempotencyToken);
    }
}