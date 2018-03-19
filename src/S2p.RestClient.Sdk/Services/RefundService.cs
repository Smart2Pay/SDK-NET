using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class RefundService : ServiceBase, IRefundService
    {
        private const string RefundUrlFormat = "/v1/payments/{0}/refunds";
        private const string RefundTypesFormat = "/v1/refunds/types/{0}/{1}/{2}";

        public RefundService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder)
        {
        }

        #region GetRefund

        private Uri GetRefundsUri(string globalPayPaymentId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));

            var uri = new Uri(HttpClient.BaseAddress, string.Format(RefundUrlFormat, globalPayPaymentId.UrlEncoded()));
            return uri;
        }

        private Uri GetRefundsUri(string globalPayPaymentId, string globalPayRefundId)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            globalPayRefundId.ThrowIfNullOrWhiteSpace(nameof(globalPayRefundId));

            return new Uri(GetRefundsUri(globalPayPaymentId), globalPayRefundId.UrlEncoded());
        }

        public Task<ApiResult<ApiRefundResponse>> GetRefundAsync(string globalPayPaymentId, string globalPayRefundId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            globalPayRefundId.ThrowIfNullOrWhiteSpace(nameof(globalPayRefundId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundsUri(globalPayPaymentId, globalPayRefundId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundResponse>> GetRefundAsync(string globalPayPaymentId, string globalPayRefundId)
        {
            return GetRefundAsync(globalPayPaymentId, globalPayRefundId, CancellationToken.None);
        }

        public Task<ApiResult<ApiRefundListResponse>> GetRefundAsync(string globalPayPaymentId, CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundsUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundListResponse>> GetRefundAsync(string globalPayPaymentId)
        {
            return GetRefundAsync(globalPayPaymentId, CancellationToken.None);
        }

        #endregion

        #region CreateRefund

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            refundRequest.ThrowIfNull(nameof(refundRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundsUri(globalPayPaymentId);
            var request = refundRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiRefundResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest)
        {
            return CreateRefundAsync(globalPayPaymentId, refundRequest, CancellationToken.None);
        }

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken, CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            refundRequest.ThrowIfNull(nameof(refundRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundsUri(globalPayPaymentId);
            var request = refundRequest.ToHttpRequest(HttpMethod.Post, uri);
            return HttpClient.InvokeAsync<ApiRefundResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundResponse>> CreateRefundAsync(string globalPayPaymentId,
            ApiRefundRequest refundRequest,
            string idempotencyToken)
        {
            return CreateRefundAsync(globalPayPaymentId, refundRequest, idempotencyToken, CancellationToken.None);
        }

        #endregion

        #region RefundTypes

        private Uri GetRefundTypesUri(string globalPayPaymentId)
        {
            return new Uri(GetRefundsUri(globalPayPaymentId), "/types");
        }

        private Uri GetRefundTypesUri(string globalPayPaymentMethodId, string countryCode,
            string currency)
        {
            globalPayPaymentMethodId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentMethodId));
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));
            currency.ThrowIfNullOrWhiteSpace(nameof(currency));

            return new Uri(HttpClient.BaseAddress,
                string.Format(RefundTypesFormat, globalPayPaymentMethodId.UrlEncoded(), countryCode.UrlEncoded()
                    , currency.UrlEncoded()));
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentId,
            CancellationToken cancellationToken)
        {
            globalPayPaymentId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundTypesUri(globalPayPaymentId);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundTypeListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentId)
        {
            return GetRefundTypesAsync(globalPayPaymentId, CancellationToken.None);
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentMethodId,
            string countryCode, string currency, CancellationToken cancellationToken)
        {
            globalPayPaymentMethodId.ThrowIfNullOrWhiteSpace(nameof(globalPayPaymentMethodId));
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));
            currency.ThrowIfNullOrWhiteSpace(nameof(currency));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetRefundTypesUri(globalPayPaymentMethodId, countryCode, currency);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiRefundTypeListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiRefundTypeListResponse>> GetRefundTypesAsync(string globalPayPaymentMethodId,
            string countryCode,
            string currency)
        {
            return GetRefundTypesAsync(globalPayPaymentMethodId, countryCode, currency, CancellationToken.None);
        }

        #endregion
    }
}
