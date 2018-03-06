using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class PaymentService : ServiceBase
    {
        private const string PaymentRelativeUrl = "v1/payments";
        private readonly Uri _paymentUri;

        public PaymentService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder)
        {
            _paymentUri = new Uri(HttpClient.BaseAddress, PaymentRelativeUrl);
        }

        #region GetPayment(s)

        private Uri GetPaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var encodedPaymentId = System.Net.WebUtility.UrlEncode(globalPayPaymentId);
            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{encodedPaymentId}");
            return uri;
        }

        public Task<ApiResult<RestPaymentResponse>> GetPaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> GetPaymentAsync(string globalPayPaymentId)
        {
            return GetPaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<RestPaymentResponseGet>> GetPaymentListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            var request = new HttpRequestMessage(HttpMethod.Get, _paymentUri);
            return HttpClient.InvokeAsync<RestPaymentResponseGet>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponseGet>> GetPaymentListAsync()
        {
            return GetPaymentListAsync(CancellationToken.None);
        }

        public Task<ApiResult<RestPaymentResponseGet>> GetPaymentListAsync(PaymentsFilter filter,
            CancellationToken cancellationToken)
        {
            filter.ThrowIfNull(nameof(filter));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var queryStringUri = filter.ToQueryStringUri(_paymentUri);
            var request = new HttpRequestMessage(HttpMethod.Get, queryStringUri);
            return HttpClient.InvokeAsync<RestPaymentResponseGet>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponseGet>> GetPaymentListAsync(PaymentsFilter filter)
        {
            return GetPaymentListAsync(filter, CancellationToken.None);
        }

        #endregion

        #region CreatePayment

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, _paymentUri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest)
        {
            return CreatePaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, _paymentUri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest,
            string idempotencyToken)
        {
            return CreatePaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CapturePayment

        private Uri GetCapturePaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var encodedPaymentId = System.Net.WebUtility.UrlEncode(globalPayPaymentId);
            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{encodedPaymentId}/capture");
            return uri;
        }

        public Task<ApiResult<RestPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId)
        {
            return CapturePaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<RestPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return CapturePaymentAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CancelPayment

        private Uri GetCancelPaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var encodedPaymentId = System.Net.WebUtility.UrlEncode(globalPayPaymentId);
            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{encodedPaymentId}/cancel");
            return uri;
        }

        public Task<ApiResult<RestPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId)
        {
            return CancelPaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<RestPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<RestPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return CancelPaymentAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion
    }
}
