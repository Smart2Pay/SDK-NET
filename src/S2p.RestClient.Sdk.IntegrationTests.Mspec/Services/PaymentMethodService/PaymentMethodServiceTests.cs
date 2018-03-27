using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PaymentMethodService
{
    public class PaymentMethodServiceTests
    {
        private static IPaymentMethodService PaymentMethodService;
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;
        private static Uri BaseAddress = new Uri(ServiceTestsConstants.BaseUrl);
        private const string CountryCode = "DE";

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.AuthenticationConfiguration);
        }

        [Subject(typeof(Sdk.Services.PaymentMethodService))]
        public class When_requesting_payment_method_by_id
        {
            private static ApiResult<ApiPaymentMethodResponse> ApiResult;
            private const string MethodId   = "46";
            private const string MethodName = "MercadoPago";


            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentMethodService = new Sdk.Services.PaymentMethodService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetPaymentMethodAsync(MethodId).GetAwaiter().GetResult();
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
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

            private It should_have_non_empty_currency_list = () => {
                ApiResult.Value.Method.Currencies.Count.ShouldBeGreaterThan(0);
            };

            private It should_have_non_empty_countries_list = () => {
                ApiResult.Value.Method.Countries.Count.ShouldBeGreaterThan(0);
            };


            private It should_have_the_correct_method_name = () => {
                ApiResult.Value.Method.DisplayName.ShouldEqual(MethodName);
            };

            private It should_have_four_payin_validators = () => {
                ApiResult.Value.Method.ValidatorsPayin.Count.ShouldEqual(4);
            };

            private It should_have_two_recurrent_validators = () => {
                ApiResult.Value.Method.ValidatorsRecurrent.Count.ShouldEqual(2);
            };

            private It should_have_customer_email_payin_validator = () => {
                const string customerEmailText = "Customer.Email";
                var customerEmailValidator = ApiResult.Value.Method.ValidatorsPayin.Where(v => v.Source == customerEmailText).FirstOrDefault();

                customerEmailValidator.ShouldNotBeNull();
                customerEmailValidator.Regex.ShouldNotBeNull();
                customerEmailValidator.Required.ShouldBeTrue();
            };

            private It should_have_customer_name_payin_validator = () => {
                const string text = "Customer.FirstName Customer.LastName";
                var validator = ApiResult.Value.Method.ValidatorsPayin.Where(v => v.Source == text).FirstOrDefault();

                validator.ShouldNotBeNull();
                validator.Regex.ShouldNotBeNull();
                validator.Required.ShouldBeTrue();
            };

            private It should_have_customer_SSN_payin_validator = () => {
                const string text = "Customer.SocialSecurityNumber";
                var validator = ApiResult.Value.Method.ValidatorsPayin.Where(v => v.Source == text).FirstOrDefault();

                validator.ShouldNotBeNull();
                validator.Regex.ShouldNotBeNull();
                validator.Required.ShouldBeTrue();
            };

            private It should_have_billing_address_country_payin_validator = () => {
                const string text = "BillingAddress.Country";
                var validator = ApiResult.Value.Method.ValidatorsPayin.Where(v => v.Source == text).FirstOrDefault();

                validator.ShouldNotBeNull();
                validator.Regex.ShouldNotBeNull();
                validator.Required.ShouldBeFalse();
            };

            private It should_have_customer_email_recurrent_validator = () => {
                const string customerEmailText = "Customer.Email";
                var customerEmailValidator = ApiResult.Value.Method.ValidatorsPayin.Where(v => v.Source == customerEmailText).FirstOrDefault();

                customerEmailValidator.ShouldNotBeNull();
                customerEmailValidator.Regex.ShouldNotBeNull();
                customerEmailValidator.Required.ShouldBeTrue();
            };

            private It should_have_billing_address_country_recurrent_validator = () => {
                const string text = "BillingAddress.Country";
                var validator = ApiResult.Value.Method.ValidatorsPayin.Where(v => v.Source == text).FirstOrDefault();

                validator.ShouldNotBeNull();
                validator.Regex.ShouldNotBeNull();
                validator.Required.ShouldBeFalse();
            };

            private It should_have_method_description = () => {
                string.IsNullOrWhiteSpace(ApiResult.Value.Method.Description).ShouldBeFalse();
            };

            private It should_have_method_logo = () => {
                const string name = "mercadopago.gif";
                ApiResult.Value.Method.LogoURL.Contains(name).ShouldBeTrue();
            };
        }

        [Subject(typeof(Sdk.Services.PaymentMethodService))]
        public class When_requesting_payment_methods
        {
            const int ExpectedMethodNumber = 149;
            protected static ApiResult<ApiPaymentMethodListResponse> ApiResult;
            protected static Tuple<ApiResult<ApiPaymentMethodListResponse>, int> TupleResult;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentMethodService = new Sdk.Services.PaymentMethodService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetPaymentMethodsListAsync().GetAwaiter().GetResult();
                TupleResult = Tuple.Create(ApiResult, ExpectedMethodNumber);
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            Behaves_like<MethodListBehavior> a_list_of_payment_methods_response;
        }

        [Subject(typeof(Sdk.Services.PaymentMethodService))]
        public class When_requesting_payment_methods_by_country
        {
            const int ExpectedMethodNumber = 17;
            protected static ApiResult<ApiPaymentMethodListResponse> ApiResult;
            protected static Tuple<ApiResult<ApiPaymentMethodListResponse>, int> TupleResult;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentMethodService = new Sdk.Services.PaymentMethodService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetPaymentMethodsListAsync(CountryCode).GetAwaiter().GetResult();
                TupleResult = Tuple.Create(ApiResult, ExpectedMethodNumber);
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            Behaves_like<MethodListBehavior> a_list_of_payment_methods_response;
        }

        [Subject(typeof(Sdk.Services.PaymentMethodService))]
        public class When_requesting_assigned_payment_methods
        {
            const int ExpectedMethodNumber = 141;
            protected static Tuple<ApiResult<ApiPaymentMethodListResponse>, int> TupleResult;
            protected static ApiResult<ApiPaymentMethodListResponse> ApiResult;

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentMethodService = new Sdk.Services.PaymentMethodService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetAssignedPaymentMethodsListAsync().GetAwaiter().GetResult();
                TupleResult = Tuple.Create(ApiResult, ExpectedMethodNumber);
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            Behaves_like<MethodListBehavior> a_list_of_payment_methods_response;
        }

        [Subject(typeof(Sdk.Services.PaymentMethodService))]
        public class When_requesting_assigned_payment_methods_by_country
        {
            const int ExpectedMethodNumber = 13;
            protected static ApiResult<ApiPaymentMethodListResponse> ApiResult;
            protected static Tuple<ApiResult<ApiPaymentMethodListResponse>, int> TupleResult; 

            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PaymentMethodService = new Sdk.Services.PaymentMethodService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PaymentMethodService.GetAssignedPaymentMethodsListAsync(CountryCode).GetAwaiter().GetResult();
                TupleResult = Tuple.Create(ApiResult, ExpectedMethodNumber);
            };

            private Cleanup after = () => {
                HttpClient.Dispose();
            };

            private It should_be_successful = () => {
                ApiResult.IsSuccess.ShouldBeTrue();
            };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            Behaves_like<MethodListBehavior> a_list_of_payment_methods_response;
        }

        [Behaviors]
        public class MethodListBehavior
        {
            protected static Tuple<ApiResult<ApiPaymentMethodListResponse>, int> TupleResult;

            private It should_have_expected_number_of_methods = () => {
                TupleResult.Item1.Value.Methods.Count.ShouldEqual(TupleResult.Item2);
            };

            private It should_have_distinct_and_positive_method_ids = () => {
                TupleResult.Item1.Value.Methods.Select(m => m.ID).Where(i => i > 0).Distinct().Count().ShouldEqual(TupleResult.Item2);
            };
        }
    }
}
