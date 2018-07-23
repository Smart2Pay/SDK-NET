using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class WebsiteManagementService : ServiceBase, IWebsiteManagementService
    {
        private const string WebsiteManagementRealtiveUrl = "v1/merchantsites";

        public WebsiteManagementService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

        #region GetWebsite

        private Uri GetWebsiteUri()
        {
            return new Uri(BaseAddress, WebsiteManagementRealtiveUrl);
        }

        private Uri GetWebsiteUri(int siteId)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));

            return new Uri(BaseAddress, $"{WebsiteManagementRealtiveUrl}/{siteId}");
        }

        public Task<ApiResult<ApiWebsiteManagementListResponse>> GetWebsiteAsync(int siteId,
            CancellationToken cancellationToken)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetWebsiteUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementListResponse>> GetWebsiteAsync(int siteId)
        {
            return GetWebsiteAsync(siteId, CancellationToken.None);
        }

        #endregion

        #region CreateWebsite

        public Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest, 
            CancellationToken cancellationToken)
        {
            websiteManagementRequest.ThrowIfNull(nameof(websiteManagementRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetWebsiteUri();
            var request = websiteManagementRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest)
        {
            return CreateWebsiteAsync(websiteManagementRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey, CancellationToken cancellationToken)
        {
            websiteManagementRequest.ThrowIfNull(nameof(websiteManagementRequest));
            idempotencyKey.ThrowIfNullOrWhiteSpace(nameof(idempotencyKey));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetWebsiteUri();
            var request = websiteManagementRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(idempotencyKey, request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> CreateWebsiteAsync(ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey)
        {
            return CreateWebsiteAsync(websiteManagementRequest, idempotencyKey, CancellationToken.None);
        }

        #endregion

        #region ChangeWebsite
        
        public Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest,
            CancellationToken cancellationToken)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));
            websiteManagementRequest.ThrowIfNull(nameof(websiteManagementRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetWebsiteUri(siteId);
            var request = websiteManagementRequest.ToHttpRequest(Constants.HttpMethodPatch, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest)
        {
            return ChangeWebsiteAsync(siteId, websiteManagementRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey, CancellationToken cancellationToken)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));
            websiteManagementRequest.ThrowIfNull(nameof(websiteManagementRequest));
            idempotencyKey.ThrowIfNullOrWhiteSpace(nameof(idempotencyKey));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetWebsiteUri(siteId);
            var request = websiteManagementRequest.ToHttpRequest(Constants.HttpMethodPatch, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(idempotencyKey, request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> ChangeWebsiteAsync(int siteId, ApiWebsiteManagementRequest websiteManagementRequest,
            string idempotencyKey)
        {
            return ChangeWebsiteAsync(siteId, websiteManagementRequest, idempotencyKey, CancellationToken.None);
        }

        #endregion

        #region RegenerateWebsiteApiKey

        private Uri GetRegenerateWebsiteApiKeyUri(int siteId)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));

            return new Uri(BaseAddress, $"{WebsiteManagementRealtiveUrl}/{siteId}/regenerateapikey");
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId,
            CancellationToken cancellationToken)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRegenerateWebsiteApiKeyUri(siteId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId)
        {
            return RegenerateWebsiteApiKeyAsync(siteId, CancellationToken.None);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId,
            string idempotencyKey, CancellationToken cancellationToken)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));
            idempotencyKey.ThrowIfNullOrWhiteSpace(nameof(idempotencyKey));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRegenerateWebsiteApiKeyUri(siteId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(idempotencyKey, request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> RegenerateWebsiteApiKeyAsync(int siteId,
            string idempotencyKey)
        {
            return RegenerateWebsiteApiKeyAsync(siteId, idempotencyKey, CancellationToken.None);
        }

        #endregion

        #region DeleteWebsite

        public Task<ApiResult<ApiWebsiteManagementResponse>> DeleteWebsiteAsync(int siteId,
            CancellationToken cancellationToken)
        {
            siteId.ThrowIfNotCondition(id => id > 0, nameof(siteId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetWebsiteUri(siteId);
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return HttpClient.InvokeAsync<ApiWebsiteManagementResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiWebsiteManagementResponse>> DeleteWebsiteAsync(int siteId)
        {
            return DeleteWebsiteAsync(siteId, CancellationToken.None);
        }

        #endregion

    }
}
