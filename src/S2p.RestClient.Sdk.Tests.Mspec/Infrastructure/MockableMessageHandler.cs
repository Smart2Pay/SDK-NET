using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public class MockableMessageHandler : HttpMessageHandler
    {
        private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _doSendAsync;

        public MockableMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> doSendAsync)
        {
            doSendAsync.ThrowIfNull("Null send async delegate");
            _doSendAsync = doSendAsync;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return _doSendAsync(request);
        }
    }
}

