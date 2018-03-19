using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class PaymentService : ServiceBase, IPaymentService
    {
        private const string PaymentRelativeUrl = "v1/payments";

        public PaymentService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder) { }

        #region GetPayment(s)

        private Uri GetPaymentUri()
        {
            return new Uri(HttpClient.BaseAddress, PaymentRelativeUrl);
        }

        private Uri GetPaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}");
            return uri;
        }

        public Task<ApiResult<ApiPaymentResponse>> GetPaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> GetPaymentAsync(string globalPayPaymentId)
        {
            return GetPaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            var uri = GetPaymentUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync()
        {
            return GetPaymentListAsync(CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync(PaymentsFilter filter,
            CancellationToken cancellationToken)
        {
            filter.ThrowIfNull(nameof(filter));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var queryStringUri = filter.ToQueryStringUri(uri);
            var request = new HttpRequestMessage(HttpMethod.Get, queryStringUri);
            return HttpClient.InvokeAsync<ApiPaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentListResponse>> GetPaymentListAsync(PaymentsFilter filter)
        {
            return GetPaymentListAsync(filter, CancellationToken.None);
        }

        #endregion

        #region CreatePayment

        public Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest)
        {
            return CreatePaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CreatePaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken)
        {
            return CreatePaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CapturePayment

        private Uri GetCapturePaymentUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/{globalPayPaymentId.UrlEncoded()}/capture");
            return uri;
        }

        public Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId)
        {
            return CapturePaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CapturePaymentAsync(string globalPayPaymentId,
            string idempotencyToken)
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

        public Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId)
        {
            return CancelPaymentAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CancelPaymentAsync(string globalPayPaymentId,
            string idempotencyToken)
        {
            return CancelPaymentAsync(globalPayPaymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region RecurringPayment

        private Uri GetRecurrentPaymentUri()
        {
            return new Uri(HttpClient.BaseAddress, $"{PaymentRelativeUrl}/recurrent");
        }

        public Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.PreapprovalID.ThrowIfNull(nameof(paymentRequest.Payment.PreapprovalID));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRecurrentPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest)
        {
            return CreateRecurrentPaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.PreapprovalID.ThrowIfNull(nameof(paymentRequest.Payment.PreapprovalID));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRecurrentPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentResponse>> CreateRecurrentPaymentAsync(ApiPaymentRequest paymentRequest,
            string idempotencyToken)
        {
            return CreateRecurrentPaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }
        #endregion
    }
}
