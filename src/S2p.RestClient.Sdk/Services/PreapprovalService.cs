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

        private Uri GetPreapprovalUri(string globalPayPreapprovalId)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));

            return new Uri(BaseAddress, $"{PreapprovalUrl}/{globalPayPreapprovalId.UrlEncoded()}");
        }

        private Uri GetPreapprovalUri()
        {
            return new Uri(BaseAddress, PreapprovalUrl);
        }

        private Uri GetPreapprovalPaymentsUri(string globalPayPreapprovalId)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));

            return new Uri(BaseAddress, $"{PreapprovalUrl}/{globalPayPreapprovalId.UrlEncoded()}/payments");
        }

        public Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(string globalPayPreapprovalId,
            CancellationToken cancellationToken)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(globalPayPreapprovalId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> GetPreapprovalAsync(string globalPayPreapprovalId)
        {
            return GetPreapprovalAsync(globalPayPreapprovalId, CancellationToken.None);
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

        public Task<ApiResult<ApiPaymentListResponse>> GetPreapprovalPaymnetsAsync(string globalPayPreapprovalId,
            CancellationToken cancellationToken)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalPaymentsUri(globalPayPreapprovalId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPreapprovalPaymnetsAsync(string globalPayPreapprovalId)
        {
            return GetPreapprovalPaymnetsAsync(globalPayPreapprovalId, CancellationToken.None);
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

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest, CancellationToken cancellationToken)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(globalPayPreapprovalId);
            var request = preapprovalRequest.ToHttpRequest(Constants.HttpMethodPatch, uri);

            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest)
        {
            return ChangePreapprovalAsync(globalPayPreapprovalId, preapprovalRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken, CancellationToken cancellationToken)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));
            preapprovalRequest.ThrowIfNull(nameof(preapprovalRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(globalPayPreapprovalId);
            var request = preapprovalRequest.ToHttpRequest(Constants.HttpMethodPatch, uri);

            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ChangePreapprovalAsync(string globalPayPreapprovalId,
            ApiPreapprovalRequest preapprovalRequest, string idempotencyToken)
        {
            return ChangePreapprovalAsync(globalPayPreapprovalId, preapprovalRequest, idempotencyToken,
                CancellationToken.None);
        }

        #endregion

        #region ClosePreaproval

        public Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(string globalPayPreapprovalId,
            CancellationToken cancellationToken)
        {
            globalPayPreapprovalId.ThrowIfNullOrWhiteSpace(nameof(globalPayPreapprovalId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPreapprovalUri(globalPayPreapprovalId);
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            return HttpClient.InvokeAsync<ApiPreapprovalResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPreapprovalResponse>> ClosePreapprovalAsync(string globalPayPreapprovalId)
        {
            return ClosePreapprovalAsync(globalPayPreapprovalId, CancellationToken.None);
        }

        #endregion
    }
}
