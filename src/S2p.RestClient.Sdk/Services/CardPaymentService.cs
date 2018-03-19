using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class CardPaymentService : ServiceBase, ICardPaymentService
    {
        private const string PaymentRelativeUrl = "v1/payments";

        public CardPaymentService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder) {  }

        #region PaymentStatus

        private Uri GetPaymentStatusUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            return new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{globalPayPaymentId}/status");
        }

        public Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentStatusUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentStatusResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(string globalPayPaymentId)
        {
            return GetPaymentStatusAsync(globalPayPaymentId, CancellationToken.None);
        }

        #endregion

        #region InitiatePayment

        private Uri GetPaymentUri()
        {
            return new Uri(HttpClient.BaseAddress, PaymentRelativeUrl);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.Card.ThrowIfNull(nameof(paymentRequest.Payment.Card));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest)
        {
            return InitiatePaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.Card.ThrowIfNull(nameof(paymentRequest.Payment.Card));
            idempotencyToken.ThrowIfNull(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            string idempotencyToken)
        {
            return InitiatePaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CapturePayment

        private Uri GetCapturePaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}/capture");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId)
        {
            return CapturePaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return CapturePaymentAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CapturePaymentPartial

        private Uri GetCapturePaymentPartialUri(string globalPayPaymentId, int amount)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));

            return new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}/capture?amount={amount}");
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId, int amount,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(globalPayPaymentId, amount);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId, int amount)
        {
            return CapturePaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            int amount, string idempotencyToken, CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(globalPayPaymentId, amount);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            int amount, string idempotencyToken)
        {
            return CapturePaymentAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CancelPayment

        private Uri GetCancelPaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}/cancel");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId)
        {
            return CancelPaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return CancelPaymentAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region AcceptChallenge

        private Uri GetAcceptChallengeUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress,
                $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}/challenge/accept");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetAcceptChallengeUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId)
        {
            return AcceptChallengeAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetAcceptChallengeUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return AcceptChallengeAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region RejectChallenge

        private Uri GetRejectChallengeUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress,
                $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}/challenge/reject");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRejectChallengeUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId)
        {
            return RejectChallengeAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRejectChallengeUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return RejectChallengeAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion
    }
}
