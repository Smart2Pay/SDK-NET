using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    public class ResilienceHandler : DelegatingHandler
    {
        private object _configuration = ResilienceDefault.Configuration;
        private Func<HttpRequestMessage, object, IAsyncPolicy<HttpResponseMessage>> _asyncPolicyGenerator = ResilienceDefault.PolicyProvider.GetAsyncPolicy;

        public ResilienceHandler WithAsyncPolicyGenerator(Func<HttpRequestMessage, object, IAsyncPolicy<HttpResponseMessage>> generator)
        {
            _asyncPolicyGenerator = generator.ValueIfNull(() => ResilienceDefault.PolicyProvider.GetAsyncPolicy);
            return this;
        }

        public ResilienceHandler WithConfiguration(object configuration)
        {
            _configuration = configuration.ValueIfNull(() => ResilienceDefault.Configuration);
            return this;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var policy = _asyncPolicyGenerator(request, _configuration);
            return policy.ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken);
        }
    }
}
