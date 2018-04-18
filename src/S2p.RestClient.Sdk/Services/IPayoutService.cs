using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IPayoutService
    {
        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync();

        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CardPayoutFilter filter,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CardPayoutFilter filter);

        Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(long payoutId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(long payoutId);

        Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(long payoutId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(long payoutId);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            string idempotencyToken, CancellationToken cancellationToken);

        Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            string idempotencyToken);
    }
}