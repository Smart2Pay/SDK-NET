using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public class MockableDelegatingHandler : DelegatingHandler
    {
        private readonly Action<HttpRequestMessage> _mockRequestHandler;

        public MockableDelegatingHandler(Action<HttpRequestMessage> mockRequestHandler)
        {
            mockRequestHandler.ThrowIfNull(nameof(mockRequestHandler));
            _mockRequestHandler = mockRequestHandler;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _mockRequestHandler(request);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
