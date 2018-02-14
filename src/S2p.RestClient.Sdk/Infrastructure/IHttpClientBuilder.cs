using System.Net.Http;

namespace S2p.RestClient.Sdk.Infrastructure
{
    public interface IHttpClientBuilder
    {
        HttpClient Build();
    }
}
