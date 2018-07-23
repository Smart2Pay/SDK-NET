using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IWebsiteManagementService
    {
        Task<ApiResult<ApiWebsiteManagementListResponse>> GetWebsiteAsync(int siteId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementListResponse>> GetWebsiteAsync(int siteId);

        Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest, 
            CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest);

        Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey, CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey);

        Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest);

        Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey, CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey);

        Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId);

        Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId,
            string idempotencyKey, CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId,
            string idempotencyKey);

        Task<ApiResult<ApiWebsiteManagementResponse>> DeleteWebsiteAsync(int siteId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiWebsiteManagementResponse>> DeleteWebsiteAsync(int siteId);
    }
}