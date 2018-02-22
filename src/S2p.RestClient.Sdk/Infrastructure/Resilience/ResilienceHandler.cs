using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Logging;

namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    public class ResilienceHandler : DelegatingHandler
    {
        private static ILoggerAdapter Logger => LoggingDefault.AdapterFactory.Get(typeof(ResilienceHandler).FullName);

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
            var contextData = new ConcurrentDictionary<string, object>();
            contextData.GetOrAdd(Constants.RequestContextKey, request);

            Logger.LogInfo(
                $"[{request.Headers.GetIdempotencyToken()}];ready to send request to uri {request.RequestUri};");

            return policy.ExecuteAsync((context, cancel) => base.SendAsync(request, cancel), contextData, cancellationToken);
        }
    }
}
