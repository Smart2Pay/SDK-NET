using System;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IPayoutService : IDisposable
    {
        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync();

        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CardPayoutFilter filter,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CardPayoutFilter filter);

        Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(string globalPayPayoutId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(string globalPayPayoutId);

        Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(string globalPayPayoutId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(string globalPayPayoutId);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            string idempotencyToken);
    }
}