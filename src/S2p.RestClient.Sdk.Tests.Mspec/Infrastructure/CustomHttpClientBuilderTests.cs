using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Machine.Specifications;
using Polly;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Authentication;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Resilience;

namespace S2p.RestClient.Sdk.Tests.Mspec.Infrastructure
{
    public partial class HttpClientBuilderTests
    {
        [Subject(typeof(HttpClientBuilder))]
        public class When_using_pipeline_extensibility_features
        {
            private static Moq.Mock<Func<AuthenticationConfiguration>> AuthenticationProvider;

            private static Moq.Mock<Func<HttpRequestMessage, object, IAsyncPolicy<HttpResponseMessage>>>
                ResilienceAsyncPolicyGenerator;

            private static object ResilienceConfiguration = new object();
            private static Moq.Mock<Func<string>> IdempotencyKeyGenerator;

            private static ConcurrentDictionary<string, int> CustomDelegatesOrderCollection =
                new ConcurrentDictionary<string, int>();

            private static Uri BaseAddress = new Uri(Url);

            private static HttpResponseMessage Response;

            private Establish context = () =>
            {
                AuthenticationProvider = new Moq.Mock<Func<AuthenticationConfiguration>>();
                AuthenticationProvider.Setup(func => func()).Returns(AuthenticationConfiguration);

                ResilienceAsyncPolicyGenerator =
                    new Moq.Mock<Func<HttpRequestMessage, object, IAsyncPolicy<HttpResponseMessage>>>();
                ResilienceAsyncPolicyGenerator
                    .Setup(func => func(Moq.It.IsAny<HttpRequestMessage>(), Moq.It.IsAny<object>()))
                    .Returns(Policy.Handle<HttpRequestException>()
                        .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.InternalServerError)
                        .RetryAsync(2));

                IdempotencyKeyGenerator = new Moq.Mock<Func<string>>();
                IdempotencyKeyGenerator.Setup(func => func()).Returns(Guid.NewGuid().ToString());

                Action<HttpRequestMessage> innerCustomHandler;
                Action<HttpRequestMessage> innerMostCustomHandler;
                Action<HttpRequestMessage> outerMostCustomHandler;
                Action<HttpRequestMessage> outerCustomHandler;
                int CustomDelegatesOrder = 0;

                outerMostCustomHandler = request =>
                {
                    CustomDelegatesOrder++;
                    CustomDelegatesOrderCollection["OuterMostCustomHandler"] = CustomDelegatesOrder;
                };

                outerCustomHandler = request =>
                {
                    CustomDelegatesOrder++;
                    CustomDelegatesOrderCollection["OuterCustomHandler"] = CustomDelegatesOrder;
                };

                var outerCustomHandlers = new List<DelegatingHandler>
                {
                    new MockableDelegatingHandler(outerCustomHandler),
                    new MockableDelegatingHandler(outerMostCustomHandler)
                };

                innerCustomHandler = request =>
                {
                    CustomDelegatesOrder++;
                    CustomDelegatesOrderCollection["InnerCustomHandler"] = CustomDelegatesOrder;
                };

                innerMostCustomHandler = request =>
                {
                    CustomDelegatesOrder++;
                    CustomDelegatesOrderCollection["InnerMostCustomHandler"] = CustomDelegatesOrder;
                };

                var innerCustomHandlers = new List<DelegatingHandler>
                {
                    new MockableDelegatingHandler(innerMostCustomHandler),
                    new MockableDelegatingHandler(innerCustomHandler)
                };

                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithAuthenticationProvider(AuthenticationProvider.Object)
                    .WithBaseAddress(BaseAddress)
                    .WithInnerCustomHandlers(innerCustomHandlers)
                    .WithOuterCustomHandlers(outerCustomHandlers)
                    .WithIdempotencyKeyGenerator(IdempotencyKeyGenerator.Object)
                    .WithResilienceAsyncPolicyGenerator(ResilienceAsyncPolicyGenerator.Object)
                    .WithResilienceConfiguration(ResilienceConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request =>
                        Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK))));

                HttpClient = HttpClientBuilder.Build();
            };

            private Because of = () => { Response = HttpClient.GetAsync(string.Empty).GetAwaiter().GetResult(); };

            private Cleanup after = () =>
            {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_have_ok_response = () => { Response.StatusCode.ShouldEqual(HttpStatusCode.OK); };

            private It should_use_custom_authentication_provider = () =>
            {
                AuthenticationProvider.Verify(func => func(), Moq.Times.Exactly(1));
            };

            private It should_have_correct_base_address = () => { HttpClient.BaseAddress.ToString().ShouldEqual(Url); };

            private It should_use_first_outer_custom_handler = () =>
            {
                CustomDelegatesOrderCollection["OuterMostCustomHandler"].ShouldEqual(1);
            };

            private It should_use_second_outer_custom_handler = () =>
            {
                CustomDelegatesOrderCollection["OuterCustomHandler"].ShouldEqual(2);
            };

            private It should_use_first_inner_custom_handler = () =>
            {
                CustomDelegatesOrderCollection["InnerCustomHandler"].ShouldEqual(3);
            };

            private It should_use_second_inner_custom_handler = () =>
            {
                CustomDelegatesOrderCollection["InnerMostCustomHandler"].ShouldEqual(4);
            };

            private It should_use_custom_idempotency_key_generator = () =>
            {
                IdempotencyKeyGenerator.Verify(func => func(), Moq.Times.Exactly(1));
            };

            private It should_use_custom_resilience_policy_with_custom_configuration = () =>
            {
                ResilienceAsyncPolicyGenerator.Verify(
                    func => func(Moq.It.IsAny<HttpRequestMessage>(), ResilienceConfiguration)
                    , Moq.Times.Exactly(1));
            };
        }

        [Subject("Idempotency")]
        public class When_idempotency_header_is_already_present
        {
            private static HttpRequestMessage Request = new HttpRequestMessage{ Method = HttpMethod.Get };
            private static ApiResult ApiResult;
            private static string IdempotencyToken = Guid.NewGuid().ToString();

            private Establish context = () =>
            {
                HttpClientBuilder = new HttpClientBuilder(() => AuthenticationConfiguration)
                    .WithPrimaryHandler(new MockableMessageHandler(request => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK))))
                    .WithBaseAddress(new Uri(Url));
                HttpClient = HttpClientBuilder.Build();
                
            };

            private Because of = () =>
            {
                ApiResult = HttpClient.Invoke(IdempotencyToken, Request).GetAwaiter().GetResult();
            };

            private Cleanup after = () =>
            {
                HttpClient.Dispose();
                DefaultPolicyProvider.PolicyCollection.Clear();
            };

            private It should_be_success = () =>
            {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_the_correct_http_status = () =>
            {
                ApiResult.Response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_the_provided_idempotency_token = () =>
            {
                Request.Headers.GetIdempotencyToken().ShouldEqual(IdempotencyToken);
            };

            private It should_not_pass_idempotency_token_between_oprations = () =>
            {
                HttpClient.DefaultRequestHeaders.HasIdempotencyHeader().ShouldBeFalse();
            };
        }
    }
}
