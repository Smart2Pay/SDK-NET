using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IAlternativePaymentService
    {
        Task<ApiResult<ApiAlternativePaymentResponse>> GetPaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> GetPaymentAsync(long paymentId);
        Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync();

        Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync(AlternativePaymentsFilter filter,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync(AlternativePaymentsFilter filter);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId);

        Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId);

        Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken);
    }
}