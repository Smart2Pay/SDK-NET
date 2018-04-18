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

        public CardPaymentService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) {  }

        #region PaymentStatus

        private Uri GetPaymentStatusUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            return new Uri(BaseAddress, $"{PaymentRelativeUrl}/{paymentId}/status");
        }

        public Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentStatusUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentStatusResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentStatusResponse>> GetPaymentStatusAsync(long paymentId)
        {
            return GetPaymentStatusAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> GetPaymentAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> GetPaymentAsync(long paymentId)
        {
            return GetPaymentAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            var uri = GetPaymentUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync()
        {
            return GetPaymentListAsync(CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CardPaymentsFilter filter,
            CancellationToken cancellationToken)
        {
            filter.ThrowIfNull(nameof(filter));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var queryStringUri = filter.ToQueryStringUri(uri);
            var request = new HttpRequestMessage(HttpMethod.Get, queryStringUri);
            return HttpClient.InvokeAsync<ApiCardPaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentListResponse>> GetPaymentListAsync(CardPaymentsFilter filter)
        {
            return GetPaymentListAsync(filter, CancellationToken.None);
        }

        #endregion

        #region InitiatePayment

        private Uri GetPaymentUri()
        {
            return new Uri(BaseAddress, PaymentRelativeUrl);
        }

        private Uri GetPaymentUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress, $"{PaymentRelativeUrl}/{paymentId}");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> InitiatePaymentAsync(ApiCardPaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
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

        private Uri GetCapturePaymentUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress, $"{PaymentRelativeUrl}/{paymentId}/capture");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId)
        {
            return CapturePaymentAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken)
        {
            return CapturePaymentAsync(paymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CapturePaymentPartial

        private Uri GetCapturePaymentPartialUri(long paymentId, long amount)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));

            return new Uri(BaseAddress, $"{PaymentRelativeUrl}/{paymentId}/capture?amount={amount}");
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(paymentId, amount);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId, long amount)
        {
            return CapturePaymentAsync(paymentId, amount, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(paymentId, amount);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, string idempotencyToken)
        {
            return CapturePaymentAsync(paymentId, amount, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CancelPayment

        private Uri GetCancelPaymentUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress, $"{PaymentRelativeUrl}/{paymentId}/cancel");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId)
        {
            return CancelPaymentAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken)
        {
            return CancelPaymentAsync(paymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region AcceptChallenge

        private Uri GetAcceptChallengeUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress,
                $"{PaymentRelativeUrl}/{paymentId}/challenge/accept");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetAcceptChallengeUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId)
        {
            return AcceptChallengeAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetAcceptChallengeUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> AcceptChallengeAsync(long paymentId,
            string idempotencyToken)
        {
            return AcceptChallengeAsync(paymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region RejectChallenge

        private Uri GetRejectChallengeUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress,
                $"{PaymentRelativeUrl}/{paymentId}/challenge/reject");
            return uri;
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRejectChallengeUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId)
        {
            return RejectChallengeAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRejectChallengeUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiCardPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiCardPaymentResponse>> RejectChallengeAsync(long paymentId,
            string idempotencyToken)
        {
            return RejectChallengeAsync(paymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion
    }
}
