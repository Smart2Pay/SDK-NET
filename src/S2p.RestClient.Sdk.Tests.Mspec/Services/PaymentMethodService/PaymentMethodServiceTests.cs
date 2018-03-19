using System;
using System.Globalization;
using System.Net;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.Tests.Mspec.Services
{
    public class PaymentMethodServiceTests
    {
        private static IPaymentMethodService PaymentMethodService;
        private static IHttpClientBuilder HttpClientBuilder;
        private const string CountryCode = "DE";

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.AuthenticationConfiguration)
                .WithBaseAddress(new Uri(ServiceTestsConstants.BaseUrl));
        }

        [Subject(typeof(PaymentMethodService))]
        public class When_requesting_payment_method_by_id
        {
            private static ApiResult<ApiPaymentMethodResponse> ApiResult;
            private const string MethodId = "46";

            private Establish context = () => {
                InitializeClientBuilder();
                PaymentMethodService = new PaymentMethodService(HttpClientBuilder);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetPaymentMethodAsync(MethodId).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                PaymentMethodService.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_non_empty_methods_list = () => {
                ApiResult.Value.Method.ID.ToString(CultureInfo.InvariantCulture).ShouldEqual(MethodId);
            };
        }

        [Subject(typeof(PaymentMethodService))]
        public class When_requesting_payment_methods
        {
            private static ApiResult<ApiPaymentMethodListResponse> ApiResult;

            private Establish context = () => {
                InitializeClientBuilder();
                PaymentMethodService = new PaymentMethodService(HttpClientBuilder);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetPaymentMethodsListAsync().GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                PaymentMethodService.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_non_empty_methods_list = () => {
                ApiResult.Value.Methods.Count.ShouldBeGreaterThan(0);
            };
        }

        [Subject(typeof(PaymentMethodService))]
        public class When_requesting_payment_methods_by_country
        {
            private static ApiResult<ApiPaymentMethodListResponse> ApiResult;

            private Establish context = () => {
                InitializeClientBuilder();
                PaymentMethodService = new PaymentMethodService(HttpClientBuilder);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetPaymentMethodsListAsync(CountryCode).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                PaymentMethodService.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_non_empty_methods_list = () => {
                ApiResult.Value.Methods.Count.ShouldBeGreaterThan(0);
            };
        }

        [Subject(typeof(PaymentMethodService))]
        public class When_requesting_assigned_payment_methods
        {
            private static ApiResult<ApiPaymentMethodListResponse> ApiResult;

            private Establish context = () => {
                InitializeClientBuilder();
                PaymentMethodService = new PaymentMethodService(HttpClientBuilder);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetAssignedPaymentMethodsListAsync().GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                PaymentMethodService.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_non_empty_methods_list = () => {
                ApiResult.Value.Methods.Count.ShouldBeGreaterThan(0);
            };
        }

        [Subject(typeof(PaymentMethodService))]
        public class When_requesting_assigned_payment_methods_by_country
        {
            private static ApiResult<ApiPaymentMethodListResponse> ApiResult;

            private Establish context = () => {
                InitializeClientBuilder();
                PaymentMethodService = new PaymentMethodService(HttpClientBuilder);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetAssignedPaymentMethodsListAsync(CountryCode).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                PaymentMethodService.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_non_empty_methods_list = () => {
                ApiResult.Value.Methods.Count.ShouldBeGreaterThan(0);
            };
        }
    }
}
