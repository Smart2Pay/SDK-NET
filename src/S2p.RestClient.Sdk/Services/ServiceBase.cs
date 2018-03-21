using System;
using System.Net.Http;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class ServiceBase
    {
        protected HttpClient HttpClient { get; }
        public Uri BaseAddress { get; }

        public ServiceBase(HttpClient httpClient, Uri baseAddress)
        {
            httpClient.ThrowIfNull(nameof(httpClient));
            baseAddress.ThrowIfNull(nameof(baseAddress));

            HttpClient = httpClient;
            BaseAddress = baseAddress;
        }
    }
}
