using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface ICardPaymentService
    {
        Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(string globalPayPaymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(string globalPayPaymentId);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId, int amount,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId, int amount);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            int amount, string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            int amount, string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId,
            string idempotencyToken);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId,
            string idempotencyToken);
    }
}