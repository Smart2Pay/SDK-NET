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
    public class AlternativePaymentService : ServiceBase, IAlternativePaymentService
    {
        private const string PaymentRelativeUrl = "v1/payments";
        private readonly IValidator<AlternativePaymentRequest> _paymentRequestValidator = new AlternativePaymentRequestValidator();

        public AlternativePaymentService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

        #region GetPayment(s)

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

        public Task<ApiResult<ApiAlternativePaymentResponse>> GetPaymentAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> GetPaymentAsync(long paymentId)
        {
            return GetPaymentAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            var uri = GetPaymentUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync()
        {
            return GetPaymentListAsync(CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync(AlternativePaymentsFilter filter,
            CancellationToken cancellationToken)
        {
            filter.ThrowIfNull(nameof(filter));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentUri();
            var queryStringUri = filter.ToQueryStringUri(uri);
            var request = new HttpRequestMessage(HttpMethod.Get, queryStringUri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentListResponse>> GetPaymentListAsync(AlternativePaymentsFilter filter)
        {
            return GetPaymentListAsync(filter, CancellationToken.None);
        }

        #endregion

        #region CreatePayment

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.ThrowIfNull(nameof(paymentRequest.Payment));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var validationResult = _paymentRequestValidator.Validate(paymentRequest.Payment);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiAlternativePaymentResponse>().ToAwaitable();
            }

            var uri = GetPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest)
        {
            return CreatePaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.ThrowIfNull(nameof(paymentRequest.Payment));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var validationResult = _paymentRequestValidator.Validate(paymentRequest.Payment);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiAlternativePaymentResponse>().ToAwaitable();
            }

            var uri = GetPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreatePaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken)
        {
            return CreatePaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region CapturePayment

        private Uri GetCapturePaymentUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress, $"{PaymentRelativeUrl}/{paymentId}/capture");
            return uri;
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId)
        {
            return CapturePaymentAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
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

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId, long amount,
            ApiAlternativePaymentRequest paymentRequest, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(paymentId, amount);
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId, long amount, ApiAlternativePaymentRequest paymentRequest)
        {
            return CapturePaymentAsync(paymentId, amount, paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId, long amount, 
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(paymentId, amount);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId, long amount)
        {
            return CapturePaymentAsync(paymentId, amount, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(paymentId, amount);
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, ApiAlternativePaymentRequest paymentRequest, string idempotencyToken)
        {
            return CapturePaymentAsync(paymentId, amount, paymentRequest, idempotencyToken, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
            long amount, string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            amount.ThrowIfNotCondition(a => a > 0, nameof(amount));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCapturePaymentPartialUri(paymentId, amount);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CapturePaymentAsync(long paymentId,
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

        public Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId)
        {
            return CancelPaymentAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetCancelPaymentUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CancelPaymentAsync(long paymentId,
            string idempotencyToken)
        {
            return CancelPaymentAsync(paymentId, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region RecurringPayment

        private Uri GetRecurrentPaymentUri()
        {
            return new Uri(BaseAddress, $"{PaymentRelativeUrl}/recurrent");
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.ThrowIfNull(nameof(paymentRequest.Payment));
            paymentRequest.Payment.PreapprovalID.ThrowIfNull(nameof(paymentRequest.Payment.PreapprovalID));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var validationResult = _paymentRequestValidator.Validate(paymentRequest.Payment);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiAlternativePaymentResponse>().ToAwaitable();
            }

            var uri = GetRecurrentPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest)
        {
            return CreateRecurrentPaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            paymentRequest.Payment.ThrowIfNull(nameof(paymentRequest.Payment));
            paymentRequest.Payment.PreapprovalID.ThrowIfNull(nameof(paymentRequest.Payment.PreapprovalID));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var validationResult = _paymentRequestValidator.Validate(paymentRequest.Payment);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiAlternativePaymentResponse>().ToAwaitable();
            }

            var uri = GetRecurrentPaymentUri();
            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiAlternativePaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiAlternativePaymentResponse>> CreateRecurrentPaymentAsync(ApiAlternativePaymentRequest paymentRequest,
            string idempotencyToken)
        {
            return CreateRecurrentPaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }
        #endregion
    }
}
