using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Infrastructure.Resilience;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public class RetryTests
    {
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;

        private static AuthenticationConfiguration AuthenticationConfiguration = new AuthenticationConfiguration
        {
            ApiKey = "ApiKey",
            SiteId = 1
        };

        private static string Url = "https://www.smart2pay.com/";

        [Subject("Retry")]
        public class When_first_request_successes
        {
            private static int TryCount = 0;
            private static HttpResponseMessage Response;

            private Establish context = () =>
            {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request =>
                    {
                        TryCount++;
                        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                    }));
                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () =>
            {
                Response = HttpClient.GetAsync(new Uri(Url)).GetAwaiter().GetResult();
            };

            private Cleanup after = () =>
            {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_have_success_response = () => { Response.StatusCode.ShouldEqual(HttpStatusCode.OK); };
            private It should_try_1_time = () => { TryCount.ShouldEqual(1); };
        }

        [Subject("Retry")]
        public class When_two_requests_fail_and_the_third_successes
        {
            private static int TryCount = 0;
            private static HttpResponseMessage Response;
            private const int MinimumWaitingTimeInSeconds = 6;
            private static int WaitingTimeInSeconds = 0;

            private Establish context = () =>
            {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request =>
                    {
                        TryCount++;
                        if (TryCount < ResilienceDefault.Configuration.Retry.Count)
                        {
                            throw new HttpRequestException();
                        }

                        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                    }));
                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    Response = HttpClient.GetAsync(new Uri(Url)).GetAwaiter().GetResult();
                }
                finally
                {
                    stopwatch.Stop();
                    WaitingTimeInSeconds = stopwatch.Elapsed.Seconds;
                }
            };

            private Cleanup after = () =>
            {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_have_success_response = () => { Response.StatusCode.ShouldEqual(HttpStatusCode.OK); };
            private It should_try_3_times = () => { TryCount.ShouldEqual(3); };

            private It should_take_more_than_minimum_waiting_time = () =>
            {
                (MinimumWaitingTimeInSeconds <= WaitingTimeInSeconds).ShouldBeTrue();
            };
        }

        [Subject("Retry")]
        public class When_all_requests_fail
        {
            private static int TryCount = 0;
            private static HttpResponseMessage Response;
            private const int MinimumWaitingTimeInSeconds = 14;
            private static int WaitingTimeInSeconds = 0;
            private static Exception ResultException;

            private Establish context = () =>
            {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request =>
                    {
                        TryCount++;
                        throw new HttpRequestException();

                    }));
                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    Response = HttpClient.GetAsync(new Uri(Url)).GetAwaiter().GetResult();
                }
                catch (Exception e)
                {
                    ResultException = e;
                }
                finally
                {
                    stopwatch.Stop();
                    WaitingTimeInSeconds = stopwatch.Elapsed.Seconds;
                }
            };

            private Cleanup after = () =>
            {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_have_http_exception_response = () => { (ResultException is HttpRequestException).ShouldBeTrue(); };
            private It should_try_4_times = () => { TryCount.ShouldEqual(4); };

            private It should_take_more_than_minimum_waiting_time = () =>
            {
                (MinimumWaitingTimeInSeconds <= WaitingTimeInSeconds).ShouldBeTrue();
            };
        }
    }
}



