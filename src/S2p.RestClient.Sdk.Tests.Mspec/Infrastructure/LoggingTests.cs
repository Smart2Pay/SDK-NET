using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Infrastructure.Logging;
using S2p.RestClient.Sdk.Infrastructure.Resilience;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public class LoggingTests
    {
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;

        private static AuthenticationConfiguration AuthenticationConfiguration = new AuthenticationConfiguration
        {
            ApiKey = "ApiKey",
            SiteId = 1
        };

        private static string Url = "https://www.smart2pay.com/";

        [Subject("Logging")]
        public class When_using_default_adapter
        {
            private It should_use_null_adapter = () =>
            {
                LoggingDefault.AdapterFactory.Get(string.Empty).GetType().ShouldEqual(typeof(NullLoggerAdapter));
            };
        }

        [Subject("Logging")]
        public class When_two_requests_fail_and_the_third_successes
        {
            public static int TryCount = 0;
            private static HttpResponseMessage Response;
            private static Moq.Mock<ILoggerAdapter> ResilienceHandlerLogger = new Moq.Mock<ILoggerAdapter>();
            private static Moq.Mock<ILoggerAdapter> ResiliencePolicyLogger = new Moq.Mock<ILoggerAdapter>();

            private Establish context = () =>
            {
                ResilienceHandlerLogger.Setup(logger => logger.LogWarn(Moq.It.IsAny<object>()));
                ResiliencePolicyLogger.Setup(logger => logger.LogInfo(Moq.It.IsAny<object>()));

                var loggerDictionary = new ConcurrentDictionary<string, ILoggerAdapter>();
                loggerDictionary.GetOrAdd(typeof(ResilienceHandler).FullName, ResilienceHandlerLogger.Object);
                loggerDictionary.GetOrAdd(typeof(DefaultPolicyProvider).FullName, ResiliencePolicyLogger.Object);
                LoggingDefault.AdapterFactory.RegisterAdapterFactory(loggerType => loggerDictionary[loggerType]);


                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithIdempotencyKeyGenerator(() => "6da98b57-f7e2-480d-9e73-ba346deaf863")
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

            private Because of = () => { Response = HttpClient.GetAsync(new Uri(Url)).GetAwaiter().GetResult(); };

            private Cleanup after = () =>
            {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
                LoggingDefault.AdapterFactory.RegisterAdapterFactory(null);
            };

            private It should_call_log_warn_2_times = () =>
            {
                ResiliencePolicyLogger.Verify(logger => logger.LogWarn(Moq.It.IsAny<object>()),
                    Moq.Times.Exactly(2));
            };

            private It should_call_log_warn_for_first_retry = () =>
            {
                ResiliencePolicyLogger.Verify(logger => logger.LogWarn(Moq.It.Is<object>(message =>
                        string.Compare(message.ToString(),
                            "[6da98b57-f7e2-480d-9e73-ba346deaf863];request to uri https://www.smart2pay.com/ failed;exception of type 'system.net.http.httprequestexception' was thrown.;retry 1;",
                            StringComparison.InvariantCulture) == 0)),
                    Moq.Times.Exactly(1));
            };

            private It should_call_log_warn_for_second_retry = () =>
            {
                ResiliencePolicyLogger.Verify(logger => logger.LogWarn(Moq.It.Is<object>(message =>
                        string.Compare(message.ToString(),
                            "[6da98b57-f7e2-480d-9e73-ba346deaf863];request to uri https://www.smart2pay.com/ failed;exception of type 'system.net.http.httprequestexception' was thrown.;retry 2;",
                            StringComparison.InvariantCulture) == 0)),
                    Moq.Times.Exactly(1));
            };

            private It should_call_log_info_once = () =>
            {
                ResilienceHandlerLogger.Verify(logger => logger.LogInfo(Moq.It.IsAny<object>()),
                    Moq.Times.Exactly(1));
            };

            private It should_call_log_info_with_correct_text = () =>
            {
                ResilienceHandlerLogger.Verify(logger => logger.LogInfo(Moq.It.Is<object>(message =>
                        string.Compare(message.ToString(),
                            "[6da98b57-f7e2-480d-9e73-ba346deaf863];ready to send request to uri https://www.smart2pay.com/;",
                            StringComparison.InvariantCulture) == 0)),
                    Moq.Times.Exactly(1));
            };
        }
    }
}
