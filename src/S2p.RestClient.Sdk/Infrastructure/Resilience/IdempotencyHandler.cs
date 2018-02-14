using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.Resilience
{
    public class IdempotencyHandler : DelegatingHandler
    {
        private Func<string> _generator = ResilienceDefault.UniqueKeyGenerator;

        public IdempotencyHandler WithUniqueKeyGenerator(Func<string> generator)
        {
            _generator = generator.ValueIfNull(() => ResilienceDefault.UniqueKeyGenerator);
            return this;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.ApplyIf(r =>
            {
                var idempotencyToken = _generator();
                r.Headers.AddIdempotencyHeader(idempotencyToken);
            }, r => !r.Headers.HasIdempotencyHeader());

            return base.SendAsync(request, cancellationToken);
        }
    }
}
