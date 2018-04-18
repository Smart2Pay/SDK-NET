using System;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IRefundService
    {
        Task<ApiResult<ApiRefundResponse>> GetRefundAsync(long paymentId, int refundId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> GetRefundAsync(long paymentId, int refundId);
        Task<ApiResult<ApiRefundListResponse>> GetRefundListAsync(long paymentId, CancellationToken cancellationToken);
        Task<ApiResult<ApiRefundListResponse>> GetRefundListAsync(long paymentId);

        Task<ApiResult<ApiCardRefundStatusResponse>> GetRefundStatusAsync(long paymentId, int refundId, 
            CancellationToken cancellationToken);
        Task<ApiResult<ApiCardRefundStatusResponse>> GetRefundStatusAsync(long paymentId, int refundId);
        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(long paymentId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(long paymentId);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(short paymentMethodId,
            string countryCode, string currency, CancellationToken cancellationToken);

        Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(short paymentMethodId,
            string countryCode,
            string currency);
    }
}