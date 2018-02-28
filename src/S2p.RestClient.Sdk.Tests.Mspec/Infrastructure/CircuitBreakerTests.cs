using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Machine.Specifications;
using Polly.CircuitBreaker;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Resilience;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public class CircuitBreakerTests
    {
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;

        private static AuthenticationConfiguration AuthenticationConfiguration = new AuthenticationConfiguration
        {
            ApiKey = "ApiKey",
            SiteId = 1
        };

        private static string Url1 = "https://www.smart2pay1.com/";
        private static string Url2 = "https://wwww.smart2pay2.com/";

        [Subject("CircuitBreaker")]
        public class When_multiple_failure_on_first_url_but_not_on_the_second
        {
            private static CircuitState CircuitBreaker1OpenState;
            private static CircuitState CircuitBreaker1HalfOpenState;
            private static CircuitState CircuitBreaker2ClosedState;

            private Establish context = () => {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request => {
                        if (string.Compare(request.RequestUri.GetBaseUri().ToString(), Url1,
                                StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            throw new HttpRequestException();
                        }

                        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                    }));
                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () => {
                for (var i = 0; i < 6; i++)
                {
                    try
                    {
                        var result2 = HttpClient.GetAsync(new Uri(Url2)).GetAwaiter().GetResult();
                        var result1 = HttpClient.GetAsync(new Uri(Url1)).GetAwaiter().GetResult();
                    }
                    catch
                    {
                    }
                }

                var circuitBreaker1 = DefaultPolicyProvider.PolicyCollection[Url1].GetDefaultPolicies().Item2;
                var circuitBreaker2 = DefaultPolicyProvider.PolicyCollection[Url2].GetDefaultPolicies().Item2;

                CircuitBreaker1OpenState = circuitBreaker1.CircuitState;
                CircuitBreaker2ClosedState = circuitBreaker2.CircuitState;
                Thread.Sleep(ResilienceDefault.Configuration.CircuitBreaker.DurationOfBreak);
                CircuitBreaker1HalfOpenState = circuitBreaker1.CircuitState;
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It failed_circuit_breaker_should_be_open = () => {
                CircuitBreaker1OpenState.ShouldEqual(CircuitState.Open);
            };

            private It failed_circuit_breaker_should_be_half_open_after_some_time = () => {
                CircuitBreaker1HalfOpenState.ShouldEqual(CircuitState.HalfOpen);
            };

            private It success_circuit_breaker_should_be_closed = () => {
                CircuitBreaker2ClosedState.ShouldEqual(CircuitState.Closed);
            };
        }
    }
}
