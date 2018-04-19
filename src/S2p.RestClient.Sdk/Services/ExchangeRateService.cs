using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class ExchangeRateService : ServiceBase, IExchangeRateService
    {
        private const string ExcahngeRateRelativeUrl = "v1/exchangerates";

        public ExchangeRateService(HttpClient httpClient, Uri baseAddress) : base(httpClient, baseAddress) { }

        private Uri GetExcahngeRateUri(string fromCurrency, string toCurrency)
        {
            fromCurrency.ThrowIfNullOrWhiteSpace(nameof(fromCurrency));
            toCurrency.ThrowIfNullOrWhiteSpace(nameof(toCurrency));

            return new Uri(BaseAddress, $"{ExcahngeRateRelativeUrl}/{fromCurrency.UrlEncoded()}/{toCurrency.UrlEncoded()}");
        }

        public Task<ApiResult<ApiExchangeRateResponse>> GetExchangeRateAsync(string fromCurrency, string toCurrency, 
            CancellationToken cancellationToken)
        {
            fromCurrency.ThrowIfNullOrWhiteSpace(nameof(fromCurrency));
            toCurrency.ThrowIfNullOrWhiteSpace(nameof(toCurrency));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var uri = GetExcahngeRateUri(fromCurrency, toCurrency);
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return HttpClient.InvokeAsync<ApiExchangeRateResponse>(request, cancellationToken);
        }

        public Task<ApiResult<ApiExchangeRateResponse>> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            return GetExchangeRateAsync(fromCurrency, toCurrency, CancellationToken.None);
        }
    }
}
