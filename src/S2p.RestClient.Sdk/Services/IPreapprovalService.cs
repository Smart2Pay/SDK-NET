using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IPreapprovalService
    {
        Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(int preapprovalId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(int preapprovalId);
        Task<ApiResult<ApiPreapprovalListResponse>> GetPreapprovalListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiPreapprovalListResponse>> GetPreapprovalListAsync();

        Task<ApiResult<ApiAlternativePaymentListResponse>> GetPreapprovalPaymentsAsync(int preapprovalId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiAlternativePaymentListResponse>> GetPreapprovalPaymentsAsync(int preapprovalId);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            string idempotencyToken);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest, CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken);

        Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(int preapprovalId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(int preapprovalId);
    }
}