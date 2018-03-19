using System;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IPreapprovalService : IDisposable
    {
        Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(string globalPayPreapprovalId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(string globalPayPreapprovalId);
        Task<ApiResult<ApiPreapprovalListResponse>> GetPreapprovalListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiPreapprovalListResponse>> GetPreapprovalListAsync();

        Task<ApiResult<ApiPaymentListResponse>> GetPreapprovalPaymnetsAsync(string globalPayPreapprovalId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentListResponse>> GetPreapprovalPaymnetsAsync(string globalPayPreapprovalId);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            string idempotencyToken);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest, CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken);

        Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(string globalPayPreapprovalId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(string globalPayPreapprovalId);
    }
}