using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class PaymentMethodService : ServiceBase, IPaymentMethodService
    {
        private const string PaymentMethodRelativeUrl = "/v1/methods";

        public PaymentMethodService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder) { }

        #region GetPaymentMethod

        private Uri GetPaymentMethodUri(string paymentMethodId)
        {
            paymentMethodId.ThrowIfNullOrWhiteSpace(nameof(paymentMethodId));

            return new Uri(HttpClient.BaseAddress, $"{PaymentMethodRelativeUrl}/{paymentMethodId.UrlEncoded()}");
        }

        public Task<ApiResult<ApiPaymentMethodResponse>> GetPaymentMethodAsync(string paymentMethodId,
            CancellationToken cancellationToken)
        {
            paymentMethodId.ThrowIfNullOrWhiteSpace(nameof(paymentMethodId));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentMethodUri(paymentMethodId);
            var request = new HttpRequestMessage(HttpMethod.Get,uri);
            return HttpClient.InvokeAsync<ApiPaymentMethodResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentMethodResponse>> GetPaymentMethodAsync(string paymentMethodId)
        {
            return GetPaymentMethodAsync(paymentMethodId, CancellationToken.None);
        }

        #endregion

        #region GetPaymentMethodList

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = new Uri(HttpClient.BaseAddress, PaymentMethodRelativeUrl);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentMethodListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync()
        {
            return GetPaymentMethodsListAsync(CancellationToken.None);
        }

        #endregion

        #region GetPaymentMethodListByCountry

        private Uri GetPaymentMethodListUri(string countryCode)
        {
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));

            return new Uri(HttpClient.BaseAddress, $"{PaymentMethodRelativeUrl}?country={countryCode.UrlEncoded()}");
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync(string countryCode,
            CancellationToken cancellationToken)
        {
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetPaymentMethodListUri(countryCode);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentMethodListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync(string countryCode)
        {
            return GetPaymentMethodsListAsync(countryCode, CancellationToken.None);
        }

        #endregion

        #region GetAssignedPaymentMethodList

        private Uri GetAssignedPaymentMethodListUri()
        {
            return new Uri(HttpClient.BaseAddress, $"{PaymentMethodRelativeUrl}/assigned");
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetAssignedPaymentMethodListUri();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentMethodListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync()
        {
            return GetAssignedPaymentMethodsListAsync(CancellationToken.None);
        }

        #endregion

        #region GetAssignedPaymentMethodListByCountry

        private Uri GetAssignedPaymentMethodListUri(string countryCode)
        {
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));

            return new Uri(HttpClient.BaseAddress, $"{PaymentMethodRelativeUrl}/assigned?country={countryCode.UrlEncoded()}");
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync(string countryCode,
            CancellationToken cancellationToken)
        {
            countryCode.ThrowIfNullOrWhiteSpace(nameof(countryCode));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetAssignedPaymentMethodListUri(countryCode);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiPaymentMethodListResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync(string countryCode)
        {
            return GetAssignedPaymentMethodsListAsync(countryCode, CancellationToken.None);
        }

        #endregion
    }
}
