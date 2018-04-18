using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class PreapprovalService : ServiceBase, IPreapprovalService
    {
        public const string PreapprovalUrl = "/v1/preapprovals";

        public PreapprovalService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

        #region GetPreapproval

        private Uri GetPreapprovalUri(int preapprovalId)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));

            return new Uri(BaseAddress, $"{PreapprovalUrl}/{preapprovalId}");
        }

        private Uri GetPreapprovalUri()
        {
            return new Uri(BaseAddress, PreapprovalUrl);
        }

        private Uri GetPreapprovalPaymentsUri(int preapprovalId)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));

            return new Uri(BaseAddress, $"{PreapprovalUrl}/{preapprovalId}/payments");
        }

        public Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(int preapprovalId,
            CancellationToken cancellationToken)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(preapprovalId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(int preapprovalId)
        {
            return GetPreapprovalAsync(preapprovalId, CancellationToken.None);
        }

        public Task<ApiResult<ApiPreapprovalListResponse>> GetPreapprovalListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalListResponse>> GetPreapprovalListAsync()
        {
            return GetPreapprovalListAsync(CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPreapprovalPaymentsAsync(int preapprovalId,
            CancellationToken cancellationToken)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalPaymentsUri(preapprovalId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPreapprovalPaymentsAsync(int preapprovalId)
        {
            return GetPreapprovalPaymentsAsync(preapprovalId, CancellationToken.None);
        }

        #endregion

        #region CreatePreapproval

        public Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            CancellationToken cancellationToken)
        {
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = new Uri(BaseAddress, PreapprovalUrl);
            var request = preapprovalRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest)
        {
            return CreatePreapprovalAsync(preapprovalRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = new Uri(BaseAddress, PreapprovalUrl);
            var request = preapprovalRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> CreatePreapprovalAsync(ApiPreapprovalRequest preapprovalRequest,
            string idempotencyToken)
        {
            return CreatePreapprovalAsync(preapprovalRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region ChangePreapproval

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest, CancellationToken cancellationToken)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(preapprovalId);
            var request = preapprovalRequest.ToHttpRequest(Constants.HttpMethodPatch, uri);

            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest)
        {
            return ChangePreapprovalAsync(preapprovalId, preapprovalRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken, CancellationToken cancellationToken)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(preapprovalId);
            var request = preapprovalRequest.ToHttpRequest(Constants.HttpMethodPatch, uri);

            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(int preapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken)
        {
            return ChangePreapprovalAsync(preapprovalId, preapprovalRequest, idempotencyToken,
                CancellationToken.None);
        }

        #endregion

        #region ClosePreaproval

        public Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(int preapprovalId,
            CancellationToken cancellationToken)
        {
            preapprovalId.ThrowIfNotCondition(id => id > 0, nameof(preapprovalId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(preapprovalId);
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(int preapprovalId)
        {
            return ClosePreapprovalAsync(preapprovalId, CancellationToken.None);
        }

        #endregion
    }
}
