using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.Authentication
{
    public class AuthenticationHandler : DelegatingHandler
    {
        public const string  AuthenticationType = "Basic";

        private readonly Func<AuthenticationConfiguration> _authenticationProvider;

        public AuthenticationHandler(Func<AuthenticationConfiguration> authenticationProvider)
        {
            authenticationProvider.ThrowIfNull("Cannot use null authentication provider");
            _authenticationProvider = authenticationProvider;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authConfig = _authenticationProvider();
            var token = authConfig.ToAuthenticationToken();
            request.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationType, token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
