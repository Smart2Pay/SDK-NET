using System.Net.Http;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class ServiceBase : DisposableBase
    {
        private HttpClient _httpClient;

        protected HttpClient HttpClient
        {
            get
            {
                CheckIfDisposed();
                return _httpClient;
            }
        }

        public ServiceBase(IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder.ThrowIfNull(nameof(httpClientBuilder));
            _httpClient = httpClientBuilder.Build();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }
    }
}
