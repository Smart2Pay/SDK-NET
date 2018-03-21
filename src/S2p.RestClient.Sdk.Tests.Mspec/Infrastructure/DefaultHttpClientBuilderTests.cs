using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Resilience;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public partial class HttpClientBuilderTests
    {
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;

        private static AuthenticationConfiguration AuthenticationConfiguration = new AuthenticationConfiguration
        {
            ApiKey = "ApiKey",
            SiteId = 1
        };

        private static string Url = "https://www.smart2pay.com/";

        [Subject(typeof(HttpClientBuilder))]
        public class When_using_default_builder_options_with_resilience
        {
            private static HttpRequestMessage Request;
            private static HttpResponseMessage Response;

            private Establish context = () => {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request => {
                        Request = request;
                        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted));
                    }));
                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () => { Response = HttpClient.GetAsync(new Uri(Url)).GetAwaiter().GetResult(); };

            private Cleanup after = () => {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_provide_default_timeout = () => {
                HttpClient.Timeout.ShouldEqual(TimeSpan.FromSeconds(100));
            };

            private It should_have_the_correct_response = () => {
                Response.StatusCode.ShouldEqual(HttpStatusCode.Accepted);
            };

            private It should_have_authorization_header = () => { Request.Headers.Authorization.ShouldNotBeNull(); };

            private It should_have_basic_authorization_header = () => {
                Request.Headers.Authorization.Scheme.ShouldEqual("Basic");
            };

            private It should_have_correct_credentials_in_authorization_header = () => {
                var bytesArray = Convert.FromBase64String(Request.Headers.Authorization.Parameter);
                var authDataString = Encoding.UTF8.GetString(bytesArray);
                var authDataArray = authDataString.Split(':');
                authDataArray.Length.ShouldEqual(2);
                authDataArray[0].ShouldEqual(AuthenticationConfiguration.SiteId.ToString());
                authDataArray[1].ShouldEqual(AuthenticationConfiguration.ApiKey);
            };

            private It should_have_request_id_header = () => { Request.Headers.HasIdempotencyHeader().ShouldBeTrue(); };

            private It should_have_guid_as_request_id = () => {
                var idempotencyToken = Request.Headers.GetIdempotencyToken();
                Guid output;
                Guid.TryParse(idempotencyToken, out output).ShouldBeTrue();
            };

            private It should_have_one_resilience_policy = () => {
                DefaultPolicyProvider.PolicyCollection.Count.ShouldEqual(1);
            };

            private It should_have_policy_for_correct_url = () => {
                DefaultPolicyProvider.PolicyCollection[Url].ShouldNotBeNull();
            };

            private It should_have_retry_with_circuit_breaker_policy = () => {
                var policy = DefaultPolicyProvider.PolicyCollection[Url];
                var result = policy.GetDefaultPolicies();
                var retryPolicy = result.Item1;
                var circuitBreakerPolicy = result.Item2;

                retryPolicy.ShouldNotBeNull();
                circuitBreakerPolicy.ShouldNotBeNull();
            };
        }

        [Subject(typeof(HttpClientBuilder))]
        public class When_using_default_builder_options_without_resilience
        {
            private static HttpRequestMessage Request;
            private static HttpResponseMessage Response;

            private Establish context = () => {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration) {IsResilient = false}
                    .WithPrimaryHandler(new MockableMessageHandler(request => {
                        Request = request;
                        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Accepted));
                    }));
                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () => { Response = HttpClient.GetAsync(new Uri(Url)).GetAwaiter().GetResult(); };

            private Cleanup after = () => {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_have_zero_resilience_policies = () => {
                DefaultPolicyProvider.PolicyCollection.Count.ShouldEqual(0);
            };
        }
    }
}

