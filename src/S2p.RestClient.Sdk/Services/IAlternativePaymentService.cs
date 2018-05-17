using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IAlternativePaymentService
    {
        Task<ApiResult<ApiPaymentResponse>> GetPaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> GetPaymentAsync(long paymentId);
        Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync();

        Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync(PaymentsFilter filter,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync(PaymentsFilter filter);

        Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest);

        Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken);

        Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(long paymentId);

        Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(long paymentId);

        Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken);

        Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest);

        Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken);
    }
}