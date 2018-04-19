using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IExchangeRateService
    {
        Task<ApiResult<ApiExchangeRateResponse>> GetExchangeRateAsync(string fromCurrency, string toCurrency,
            CancellationToken cancellationToken);
        Task<ApiResult<ApiExchangeRateResponse>> GetExchangeRateAsync(string fromCurrency, string toCurrency);
    }
}
