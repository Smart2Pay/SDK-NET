using System;
using System.Net;
using System.Net.Http;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.ExchangeRateService
{
    public class ExchangeRateServiceTests
    {
        private static IExchangeRateService ExchangeRateService;
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;
        private static readonly Uri BaseAddress = new Uri(ServiceTestsConstants.PaymentSystemBaseUrl);
        private static ApiResult<ApiExchangeRateResponse> ApiResult;

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.PaymentSystemAuthenticationConfiguration);
        }

        [Subject(typeof(Sdk.Services.ExchangeRateService))]
        public class When_requesting_an_exchange_rate
        {
            private const string FromCurrency = "EUR";
            private const string ToCurrency = "USD";

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                ExchangeRateService = new Sdk.Services.ExchangeRateService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = ExchangeRateService.GetExchangeRateAsync(FromCurrency, ToCurrency).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_correct_from_currency = () => {
                ApiResult.Value.ExchangeRate.From.ShouldEqual(FromCurrency);
            };

            private It should_have_correct_to_currency = () => {
                ApiResult.Value.ExchangeRate.To.ShouldEqual(ToCurrency);
            };

            private It should_have_correct_formatted_date = () => {
                ApiResult.Value.ExchangeRate.DateTime.ToGlobalPayDateTime().ShouldNotBeNull();
            };

            private It should_have_correct_exchange_rate = () => {
                ApiResult.Value.ExchangeRate.Rate.ShouldBeGreaterThan(decimal.Zero);
            };
        }
    }
}
