using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Entities.Validators;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Services
{
    public class CardPayoutService : ServiceBase, ICardPayoutService
    {
        private const string PayoutPartialUrl = "/v1/payouts";
        private readonly IValidator<CardPayoutRequest> _cardPayoutRequestValidator = new CardPayoutRequestValidator();

        public CardPayoutService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

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

        private Uri GetPayoutStatusUri(long payoutId)
        {
            payoutId.ThrowIfNotCondition(id => id > 0, nameof(payoutId));

            return new Uri(BaseAddress, $"{PayoutPartialUrl}/{payoutId}/status");
        }

        public Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(long payoutId,
            CancellationToken cancellationToken)
        {
            payoutId.ThrowIfNotCondition(id => id > 0, nameof(payoutId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutStatusUri(payoutId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutStatusResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutStatusResponse>> GetPayoutStatusAsync(long payoutId)
        {
            return GetPayoutStatusAsync(payoutId, CancellationToken.None);
        }

        #endregion

        #region GetPayout

        private Uri GetPayoutInfoUri(long payoutId)
        {
            payoutId.ThrowIfNotCondition(id => id > 0, nameof(payoutId));

            return new Uri(BaseAddress, $"{PayoutPartialUrl}/{payoutId}");
        }

        public Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(long payoutId,
            CancellationToken cancellationToken)
        {
            payoutId.ThrowIfNotCondition(id => id > 0, nameof(payoutId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPayoutInfoUri(payoutId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPayoutResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPayoutResponse>> GetPayoutAsync(long payoutId)
        {
            return GetPayoutAsync(payoutId, CancellationToken.None);
        }

        #endregion

        #region InitiatePayout

        public Task<ApiResult<ApiCardPayoutResponse>> InitiatePayoutAsync(ApiCardPayoutRequest payoutRequest,
            CancellationToken cancellationToken)
        {
            payoutRequest.ThrowIfNull(nameof(payoutRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            payoutRequest.Payout.ThrowIfNull(nameof(payoutRequest.Payout));

            var validationResult = _cardPayoutRequestValidator.Validate(payoutRequest.Payout);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiCardPayoutResponse>().ToAwaitable();
            }

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
            payoutRequest.Payout.ThrowIfNull(nameof(payoutRequest.Payout));

            var validationResult = _cardPayoutRequestValidator.Validate(payoutRequest.Payout);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiCardPayoutResponse>().ToAwaitable();
            }

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
