using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class PayoutService : ServiceBase, IPayoutService
    {
        private const string PayoutPartialUrl = "/v1/payouts";

        public PayoutService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

        #region GetPayoutList

        private Uri GetPayoutUri()
        {
            return new Uri(BaseAddress, PayoutPartialUrl);
        }

        public Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync()
        {
            return GetPayoutListAsync(CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CardPayoutFilter filter,
            CancellationToken cancellationToken)
        {
            filter.ThrowIfNull(nameof(filter));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutUri();
            var queryStringUri = filter.ToQueryStringUri(uri);
            var request = new HttpRequestMessage(HttpMethod.Get, queryStringUri);
            return HttpClient.InvokeAsync<ApiCardPayoutListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutListResponse>> GetPayoutListAsync(CardPayoutFilter filter)
        {
            return GetPayoutListAsync(filter, CancellationToken.None);
        }

        #endregion

        #region GetPayoutStatus

        private Uri GetPayoutStatusUri(string globalPayPayoutId)
        {
            globalPayPayoutId.ThrowIfNullOrWhiteSpace(nameof(globalPayPayoutId));

            return new Uri(BaseAddress, $"{PayoutPartialUrl}/{globalPayPayoutId.UrlEncoded()}/status");
        }

        public Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(string globalPayPayoutId,
            CancellationToken cancellationToken)
        {
            globalPayPayoutId.ThrowIfNullOrWhiteSpace(nameof(globalPayPayoutId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutStatusUri(globalPayPayoutId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutStatusResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(string globalPayPayoutId)
        {
            return GetPayoutStatusAsync(globalPayPayoutId, CancellationToken.None);
        }

        #endregion

        #region GetPayout

        private Uri GetPayoutInfoUri(string globalPayPayoutId)
        {
            globalPayPayoutId.ThrowIfNullOrWhiteSpace(nameof(globalPayPayoutId));

            return new Uri(BaseAddress, $"{PayoutPartialUrl}/{globalPayPayoutId.UrlEncoded()}");
        }

        public Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(string globalPayPayoutId,
            CancellationToken cancellationToken)
        {
            globalPayPayoutId.ThrowIfNullOrWhiteSpace(nameof(globalPayPayoutId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutInfoUri(globalPayPayoutId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(string globalPayPayoutId)
        {
            return GetPayoutAsync(globalPayPayoutId, CancellationToken.None);
        }

        #endregion

        #region InitiatePayout

        public Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            CancellationToken cancellationToken)
        {
            payoutRequest.ThrowIfNull(nameof(payoutRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutUri();
            var request = payoutRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest)
        {
            return InitiatePayoutAsync(payoutRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            payoutRequest.ThrowIfNull(nameof(payoutRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutUri();
            var request = payoutRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            string idempotencyToken)
        {
            return InitiatePayoutAsync(payoutRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion
    }
}
