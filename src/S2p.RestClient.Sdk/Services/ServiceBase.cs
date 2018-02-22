using System.Net.Http;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class ServiceBase : DisposableBase
    {
        private HttpClient _httpClient;

        public ServiceBase(IHttpClientBuilder htppClientBuilder)
        {
            htppClientBuilder.ThrowIfNull(nameof(htppClientBuilder));
            _httpClient = htppClientBuilder.Build();
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
