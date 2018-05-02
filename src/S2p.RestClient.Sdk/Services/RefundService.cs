using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Entities.Validators;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Helper;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Services
{
    public class RefundService : ServiceBase, IRefundService
    {
        private const string RefundUrlFormat  = "/v1/payments/{0}/refunds";
        private const string RefundTypesUrlFormat = "/v1/refunds/types/{0}/{1}/{2}";
        private readonly IValidator<RefundRequest> _refundRequestValidator = new RefundRequestValidator();
        
        public RefundService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

        #region GetRefund

        private Uri GetRefundsUri(long paymentId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));

            var uri = new Uri(BaseAddress, string.Format(RefundUrlFormat, paymentId));
            return uri;
        }

        private Uri GetRefundStatusUri(long paymentId, int refundId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            refundId.ThrowIfNotCondition(id => id > 0, nameof(refundId));
            return new Uri(string.Concat(GetRefundsUri(paymentId).AbsoluteUri, "/", refundId, "/", "status"));
        }

        private Uri GetRefundsUri(long paymentId, int refundId)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            refundId.ThrowIfNotCondition(id => id > 0, nameof(refundId));

            return new Uri(string.Concat(GetRefundsUri(paymentId).AbsoluteUri, "/", refundId));
        }

        public Task<ApiResult<ApiRefundResponse>> GetRefundAsync(long paymentId, int refundId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            refundId.ThrowIfNotCondition(id => id > 0, nameof(refundId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundsUri(paymentId, refundId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundResponse>> GetRefundAsync(long paymentId, int refundId)
        {
            return GetRefundAsync(paymentId, refundId, CancellationToken.None);
        }

        public Task<ApiResult<ApiRefundListResponse>> GetRefundListAsync(long paymentId, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundsUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundListResponse>> GetRefundListAsync(long paymentId)
        {
            return GetRefundListAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiCardRefundStatusResponse>> GetRefundStatusAsync(long paymentId, int refundId, 
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            refundId.ThrowIfNotCondition(id => id > 0, nameof(refundId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundStatusUri(paymentId, refundId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiCardRefundStatusResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiCardRefundStatusResponse>> GetRefundStatusAsync(long paymentId, int refundId)
        {
            return GetRefundStatusAsync(paymentId, refundId, CancellationToken.None);
        }

        #endregion

        #region CreateRefund

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            refundRequest.ThrowIfNull(nameof(refundRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            refundRequest.Refund.ThrowIfNull(nameof(refundRequest.Refund));

            var validationResult = _refundRequestValidator.Validate(refundRequest.Refund);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiRefundResponse>().ToAwaitable();
            }

            var uri = GetRefundsUri(paymentId);
            var request = refundRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiRefundResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest)
        {
            return CreateRefundAsync(paymentId, refundRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            refundRequest.ThrowIfNull(nameof(refundRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));
            refundRequest.Refund.ThrowIfNull(nameof(refundRequest.Refund));

            var validationResult = _refundRequestValidator.Validate(refundRequest.Refund);
            if (!validationResult.IsValid)
            {
                return validationResult.ToValidationException().ToApiResult<ApiRefundResponse>().ToAwaitable();
            }

            var uri = GetRefundsUri(paymentId);
            var request = refundRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiRefundResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(long paymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken)
        {
            return CreateRefundAsync(paymentId, refundRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region RefundTypes

        private Uri GetRefundTypesUri(long paymentId)
        {
            return new Uri(string.Concat(GetRefundsUri(paymentId).AbsoluteUri, "/", "types"));
        }

        private Uri GetRefundTypesUri(short paymentMethodId, string countryCode,
            string currency)
        {
            paymentMethodId.ThrowIfNotCondition(id => id > 0, nameof(paymentMethodId));
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));
            currency.ThrowIfNullOrWhiteSpace(nameof(currency));

            return new Uri(BaseAddress,
                string.Format(RefundTypesUrlFormat, paymentMethodId, countryCode.UrlEncoded()
                    , currency.UrlEncoded()));
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(long paymentId,
            CancellationToken cancellationToken)
        {
            paymentId.ThrowIfNotCondition(id => id > 0, nameof(paymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundTypesUri(paymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundTypeListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(long paymentId)
        {
            return GetRefundTypesAsync(paymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(short paymentMethodId,
            string countryCode, string currency, CancellationToken cancellationToken)
        {
            paymentMethodId.ThrowIfNotCondition(id => id > 0, nameof(paymentMethodId));
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));
            countryCode.ThrowIfNotCondition(Country.Exists, nameof(countryCode));
            currency.ThrowIfNullOrWhiteSpace(nameof(currency));
            currency.ThrowIfNotCondition(Currency.Exists, nameof(currency));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundTypesUri(paymentMethodId, countryCode, currency);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundTypeListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(short paymentMethodId,
            string countryCode,
            string currency)
        {
            return GetRefundTypesAsync(paymentMethodId, countryCode, currency, CancellationToken.None);
        }
        
        #endregion
    }
}
