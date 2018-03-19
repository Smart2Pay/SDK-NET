using System;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IRefundService : IDisposable
    {
        Task<ApiResult<ApiRefundResponse>> GetRefundAsync(string globalPayPaymentId, string globalPayRefundId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> GetRefundAsync(string globalPayPaymentId, string globalPayRefundId);
        Task<ApiResult<ApiRefundListResponse>> GetRefundAsync(string globalPayPaymentId, CancellationToken cancellationToken);
        Task<ApiResult<ApiRefundListResponse>> GetRefundAsync(string globalPayPaymentId);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentId);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentMethodId,
            string countryCode, string currency, CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentMethodId,
            string countryCode,
            string currency);
    }
}
