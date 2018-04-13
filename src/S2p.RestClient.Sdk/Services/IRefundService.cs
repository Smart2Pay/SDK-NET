using System;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IRefundService
    {
        Task<ApiResult<ApiRefundResponse>> GetRefundAsync(string globalPayPaymentId, string globalPayRefundId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> GetRefundAsync(string globalPayPaymentId, string globalPayRefundId);
        Task<ApiResult<ApiRefundListResponse>> GetRefundListAsync(string globalPayPaymentId, CancellationToken cancellationToken);
        Task<ApiResult<ApiRefundListResponse>> GetRefundListAsync(string globalPayPaymentId);

        Task<ApiResult<ApiCardRefundStatusResponse>> GetRefundStatusAsync(string globalPayPaymentId, string globalPayRefundId, 
            CancellationToken cancellationToken);
        Task<ApiResult<ApiCardRefundStatusResponse>> GetRefundStatusAsync(string globalPayPaymentId, string globalPayRefundId);
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

        Uri BaseAddress { get; }
    }
}