using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PayoutService
{
    public class PayoutServiceTests
    {
        private static IPayoutService PayoutService;
        private static IHttpClientBuilder HttpClientBuilder;
        private static HttpClient HttpClient;
        private static readonly Uri BaseAddress = new Uri(ServiceTestsConstants.PayoutBaseUrl);

        private static void InitializeClientBuilder()
        {
            HttpClientBuilder = new HttpClientBuilder(() => ServiceTestsConstants.PayoutAuthenticationConfiguration);
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_initiating_payout_with_billing_address
        {
            protected static ApiResult<ApiCardPayoutResponse> ApiResult;
            private static ApiCardPayoutRequest PayoutRequest;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                PayoutRequest = new CardPayoutRequest
                {
                    MerchantTransactionID = Guid.NewGuid().ToString(),
                    Amount = 1000,
                    Currency = "USD",
                    Description = "Card SDK Test Payout",
                    BillingAddress = new Address
                    {
                        City = "Iasi",
                        ZipCode = "7000-49",
                        State = "Iasi",
                        Street = "Sf Lazar",
                        StreetNumber = "37",
                        HouseNumber = "5A",
                        HouseExtension = "",
                        Country = "RO"
                    },
                    Card = new CardDetailsRequest
                    {
                        HolderName = "John Doe",
                        Number = "4111111111111111",
                        ExpirationMonth = "02",
                        ExpirationYear = "2020"
                    }
                }.ToApiCardPayoutRequest();
            };

            private Because of = () => {
                ApiResult = PayoutService.InitiatePayoutAsync(PayoutRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_created_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.Payout.ID.ShouldNotBeNull(); };

            private It should_have_correct_merchant_transaction_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldEqual(PayoutRequest.Payout.MerchantTransactionID);
            };

            private It should_have_correct_amount = () => {
                ApiResult.Value.Payout.Amount.ShouldEqual(PayoutRequest.Payout.Amount);
            };

            private It should_have_correct_currency = () => {
                ApiResult.Value.Payout.Currency.ShouldEqual(PayoutRequest.Payout.Currency);
            };

            private It should_have_not_null_billing_address = () => {
                ApiResult.Value.Payout.BillingAddress.ShouldNotBeNull();
            };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.Payout.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Success);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Payout.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Success));
            };
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_initiating_payout_without_billing_address
        {
            protected static ApiResult<ApiCardPayoutResponse> ApiResult;
            private static ApiCardPayoutRequest PayoutRequest;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                PayoutRequest = new CardPayoutRequest
                {
                    MerchantTransactionID = Guid.NewGuid().ToString(),
                    Amount = 1000,
                    Currency = "EUR",
                    Description = "Card SDK Test Payout",
                    Card = new CardDetailsRequest
                    {
                        HolderName = "John Doe",
                        Number = "4548812049400004",
                        ExpirationMonth = "02",
                        ExpirationYear = "2020"
                    }
                }.ToApiCardPayoutRequest();
            };

            private Because of = () => {
                ApiResult = PayoutService.InitiatePayoutAsync(PayoutRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_created_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.Created);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.Payout.ID.ShouldNotBeNull(); };

            private It should_have_correct_merchant_transaction_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldEqual(PayoutRequest.Payout.MerchantTransactionID);
            };

            private It should_have_correct_amount = () => {
                ApiResult.Value.Payout.Amount.ShouldEqual(PayoutRequest.Payout.Amount);
            };

            private It should_have_correct_currency = () => {
                ApiResult.Value.Payout.Currency.ShouldEqual(PayoutRequest.Payout.Currency);
            };

            private It should_have_null_billing_address = () => {
                ApiResult.Value.Payout.BillingAddress.ShouldBeNull();
            };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.Payout.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Success);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Payout.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Success));
            };
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_quering_about_success_payout_status
        {
            protected static ApiResult<ApiCardPayoutStatusResponse> ApiResult;
            private static long payoutId = 1252;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PayoutService.GetPayoutStatusAsync(payoutId).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.PayoutStatus.ID.ShouldNotBeNull(); };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.PayoutStatus.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Success);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.PayoutStatus.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Success));
            };

            private It should_have_empty_reasons = () => {
                ApiResult.Value.PayoutStatus.Status.Reasons.Count.ShouldEqual(0);
            };

        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_quering_about_failed_payout_status
        {
            protected static ApiResult<ApiCardPayoutStatusResponse> ApiResult;
            private static long payoutId = 1267;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PayoutService.GetPayoutStatusAsync(payoutId).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_true = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.PayoutStatus.ID.ShouldNotBeNull(); };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.PayoutStatus.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Failed);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.PayoutStatus.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Failed));
            };

            private It should_have_at_least_reasons = () => {
                ApiResult.Value.PayoutStatus.Status.Reasons.Count.ShouldBeGreaterThanOrEqualTo(1);
            };

        }


        public class When_initiating_two_payouts_with_the_same_merchant_transaction_id
        {
            protected static ApiResult<ApiCardPayoutResponse> ApiResult;
            private static ApiCardPayoutRequest PayoutRequest;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                PayoutRequest = new CardPayoutRequest
                {
                    MerchantTransactionID = Guid.NewGuid().ToString(),
                    Amount = 1000,
                    Currency = "EUR",
                    Description = "Card SDK Test Payout",
                    Card = new CardDetailsRequest
                    {
                        HolderName = "John Doe",
                        Number = "4111111111111111",
                        ExpirationMonth = "02",
                        ExpirationYear = "2020"
                    }
                }.ToApiCardPayoutRequest();
            };

            private Because of = () => { ApiResult = BecauseAsync().GetAwaiter().GetResult(); };

            private static async Task<ApiResult<ApiCardPayoutResponse>> BecauseAsync()
            {
                await PayoutService.InitiatePayoutAsync(PayoutRequest);
                return await PayoutService.InitiatePayoutAsync(PayoutRequest);
            }

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_failed = () => { ApiResult.IsSuccess.ShouldBeFalse(); };

            private It should_have_created_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
            };

            private It should_have_not_null_invalid_request_id = () => {
                string.IsNullOrWhiteSpace(ApiResult.Value.Payout.InvalidRequestID).ShouldBeFalse();
            };

            private It should_have_null_id = () => { ApiResult.Value.Payout.ID.ShouldBeNull(); };

            private It should_have_correct_merchant_transaction_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldEqual(PayoutRequest.Payout.MerchantTransactionID);
            };

            private It should_have_correct_amount = () => {
                ApiResult.Value.Payout.Amount.ShouldEqual(PayoutRequest.Payout.Amount);
            };

            private It should_have_correct_currency = () => {
                ApiResult.Value.Payout.Currency.ShouldEqual(PayoutRequest.Payout.Currency);
            };

            private It should_have_null_billing_address = () => {
                ApiResult.Value.Payout.BillingAddress.ShouldBeNull();
            };

            private It should_have_null_status_id = () => { ApiResult.Value.Payout.Status.ID.ShouldBeNull(); };

            private It should_have_null_status_info = () => { ApiResult.Value.Payout.Status.Info.ShouldBeNull(); };

            private It should_have_non_empty_reason_list = () => {
                ApiResult.Value.Payout.Status.Reasons.Count.ShouldBeGreaterThan(0);
            };

            private It should_have_reason_code_duplicate = () => {
                ApiResult.Value.Payout.Status.Reasons.Where(r => r.Code == CardReturnCodeId.DuplicateKey)
                    .FirstOrDefault().ShouldNotBeNull();
            };

            private It should_have_correct_reason_info = () => {
                const string expectedInfo = "Key is duplicated";
                ApiResult.Value.Payout.Status.Reasons.First(r => r.Code == CardReturnCodeId.DuplicateKey).Info
                    .ShouldEqual(expectedInfo);

            };
        }

        public class When_initiating_payout_fails
        {
            protected static ApiResult<ApiCardPayoutResponse> ApiResult;
            private static ApiCardPayoutRequest PayoutRequest;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                PayoutRequest = new CardPayoutRequest
                {
                    MerchantTransactionID = Guid.NewGuid().ToString(),
                    Amount = 1000,
                    Currency = "EUR",
                    Description = "Card SDK Failed Test Payout",
                    Card = new CardDetailsRequest
                    {
                        HolderName = "John Doe",
                        Number = "4111111111111111",
                        ExpirationMonth = "02",
                        ExpirationYear = "2018"
                    }
                }.ToApiCardPayoutRequest();
            };

            private Because of = () => {
                ApiResult = PayoutService.InitiatePayoutAsync(PayoutRequest).GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_failed = () => { ApiResult.IsSuccess.ShouldBeFalse(); };

            private It should_have_bad_request_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.Payout.ID.ShouldNotBeNull(); };

            private It should_have_correct_merchant_transaction_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldEqual(PayoutRequest.Payout.MerchantTransactionID);
            };

            private It should_have_correct_amount = () => {
                ApiResult.Value.Payout.Amount.ShouldEqual(PayoutRequest.Payout.Amount);
            };

            private It should_have_correct_currency = () => {
                ApiResult.Value.Payout.Currency.ShouldEqual(PayoutRequest.Payout.Currency);
            };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.Payout.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Failed);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Payout.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Failed));
            };
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_quering_about_success_payout_information
        {
            protected static ApiResult<ApiCardPayoutResponse> ApiResult;
            private static long payoutId = 1247;
            private const string merchantTransactionId = "701428ed-cf24-4c06-8a39-f7c40809e7a4";
            private const long amount = 1000;
            private const string currency = "EUR";

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
            };

            private Because of = () => { ApiResult = PayoutService.GetPayoutAsync(payoutId).GetAwaiter().GetResult(); };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.Payout.ID.ShouldNotBeNull(); };

            private It should_have_correct_site_id = () => {
                ApiResult.Value.Payout.SiteID.ShouldEqual(
                    ServiceTestsConstants.PayoutAuthenticationConfiguration.SiteId);
            };

            private It should_have_not_null_merchant_transactiono_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldNotBeNull();
            };

            private It should_have_correct_merchant_transactiono_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldEqual(merchantTransactionId);
            };

            private It should_have_not_null_amount = () => { ApiResult.Value.Payout.Amount.ShouldNotBeNull(); };

            private It should_have_correct_amount = () => { ApiResult.Value.Payout.Amount.ShouldEqual(amount); };
            private It should_have_not_null_currency = () => { ApiResult.Value.Payout.Currency.ShouldNotBeNull(); };

            private It should_have_correct_currency = () => { ApiResult.Value.Payout.Currency.ShouldEqual(currency); };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.Payout.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Success);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Payout.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Success));
            };

            private It should_have_empty_reasons = () => {
                ApiResult.Value.Payout.Status.Reasons.Count.ShouldEqual(0);
            };



        }

        public class When_quering_about_failed_payout_information
        {
            protected static ApiResult<ApiCardPayoutResponse> ApiResult;
            private static long payoutId = 1267;
            private const string merchantTransactionId = "5b493baf-b792-4fd1-9d33-82b685f3e055";
            private const long amount = 1000;
            private const string currency = "EUR";

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
            };

            private Because of = () => { ApiResult = PayoutService.GetPayoutAsync(payoutId).GetAwaiter().GetResult(); };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_not_null_id = () => { ApiResult.Value.Payout.ID.ShouldNotBeNull(); };

            private It should_have_not_null_merchant_transactiono_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldNotBeNull();
            };

            private It should_have_correct_merchant_transactiono_id = () => {
                ApiResult.Value.Payout.MerchantTransactionID.ShouldEqual(merchantTransactionId);
            };

            private It should_have_not_null_amount = () => { ApiResult.Value.Payout.Amount.ShouldNotBeNull(); };

            private It should_have_correct_amount = () => { ApiResult.Value.Payout.Amount.ShouldEqual(amount); };
            private It should_have_not_null_currency = () => { ApiResult.Value.Payout.Currency.ShouldNotBeNull(); };

            private It should_have_correct_currency = () => { ApiResult.Value.Payout.Currency.ShouldEqual(currency); };

            private It should_have_correct_status_id = () => {
                ApiResult.Value.Payout.Status.ID.ShouldEqual(CardPaymentStatusDefinition.Failed);
            };

            private It should_have_correct_status_info = () => {
                ApiResult.Value.Payout.Status.Info.ShouldEqual(nameof(CardPaymentStatusDefinition.Failed));
            };

            private It should_have_at_least_ones_reasons = () => {
                ApiResult.Value.Payout.Status.Reasons.Count.ShouldBeGreaterThanOrEqualTo(1);
            };

            private It should_have_correct_reason_info = () => {
                const string expectedInfo = "-";
                ApiResult.Value.Payout.Status.Reasons
                    .First(r => r.Code == CardReturnCodeId.InvalidCardDetailsCardExpired).Info
                    .ShouldEqual(expectedInfo);

            };
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;


            private Establish context = () => {
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync().GetAwaiter().GetResult();
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_be_successful = () => { ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };


            private It should_have_non_empty_payouts_list = () => { ApiResult.Value.Payouts.Count.ShouldBeGreaterThan(0); };

            private It should_have_correct_site_id = () => {
                ApiResult.Value.Payouts.Count(p => p.SiteID == null ||
                                                   p.SiteID.Value != ServiceTestsConstants
                                                       .PayoutAuthenticationConfiguration.SiteId)
                    .ShouldEqual(0);
            };

            private It should_have_not_null_payout_ids = () => {
                ApiResult.Value.Payouts.Count(p => p.ID == null || p.ID<=0).ShouldEqual(0);
            };
            private It should_have_not_null_merchant_transaction_ids = () => {
                ApiResult.Value.Payouts.Count(p => string.IsNullOrWhiteSpace(p.MerchantTransactionID)).ShouldEqual(0);
            };
            private It should_have_not_null_amounts = () => {
                ApiResult.Value.Payouts.Count(p => p.Amount == null || p.Amount <= 0).ShouldEqual(0);
            };
            private It should_have_not_null_currency = () => {
                ApiResult.Value.Payouts.Count(p => string.IsNullOrWhiteSpace(p.Currency)).ShouldEqual(0);
            };
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_limit
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 2;
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter {limit = Limit};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData { ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_date
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 36;
            private static readonly DateTime StartDate = new DateTime(2018,3,29);
            private static readonly DateTime EndDate = new DateTime(2018,4,1);
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter { startDate = StartDate, endDate = EndDate};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData {ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter};
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_status_id
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 6;
            private static readonly int StatusID = 4;
            private static readonly DateTime StartDate = new DateTime(2018, 3, 29);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 1);           
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter { startDate = StartDate, endDate = EndDate, statusID = StatusID};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData { ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_merchant_transaction_id
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 1;
            private const string MerchantTransactionId = "7dfd7dd2-0b1f-4356-8964-fbfca6e81679";
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter { merchantTransactionID = MerchantTransactionId};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData { ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_currency
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 2;
            private const string Currency = "USD";
            private static readonly DateTime StartDate = new DateTime(2018, 3, 29);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 1);
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter { startDate = StartDate, endDate = EndDate, currency = Currency};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData { ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_country
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 2;
            private const string Country = "US";
            private static readonly DateTime StartDate = new DateTime(2018, 3, 29);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 1);
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter { country = Country, startDate = StartDate, endDate = EndDate};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData { ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }

        [Subject(typeof(Sdk.Services.PayoutService))]
        public class When_requesting_payout_list_filtered_by_country_with_zero_payouts
        {
            private static ApiResult<ApiCardPayoutListResponse> ApiResult;
            private const int Limit = 0;
            private const string Country = "AX";
            private static readonly DateTime StartDate = new DateTime(2018, 3, 29);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 1);
            private static CardPayoutFilter CardFilter;
            protected static PayoutFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeClientBuilder();
                HttpClient = HttpClientBuilder.Build();
                PayoutService = new Sdk.Services.PayoutService(HttpClient, BaseAddress);
                CardFilter = new CardPayoutFilter { country = Country, startDate = StartDate, endDate = EndDate};
            };

            private Because of = () => {
                ApiResult = PayoutServiceTests.PayoutService.GetPayoutListAsync(CardFilter).GetAwaiter().GetResult();
                Data = new PayoutFilteredBehaviourData { ApiResult = ApiResult, Limit = Limit, CardFilter = CardFilter };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PayoutFilteredBehavior> a_list_of_filtered_payouts_response;
        }
        public class PayoutFilteredBehaviourData
        {
            public ApiResult<ApiCardPayoutListResponse> ApiResult { get; set; }
            public int Limit { get; set; }
            public CardPayoutFilter CardFilter { get; set; }

        }

        [Behaviors]
        public class PayoutFilteredBehavior
        {
            protected static PayoutFilteredBehaviourData Data;

            private It should_be_successful = () => { Data.ApiResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                Data.ApiResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_correct_number_of_payouts_in_list = () => { Data.ApiResult.Value.Payouts.Count.ShouldEqual(Data.Limit); };

            private It should_have_correct_site_id = () => {
                Data.ApiResult.Value.Payouts.Count(p => p.SiteID == null ||
                                                   p.SiteID.Value != ServiceTestsConstants
                                                       .PayoutAuthenticationConfiguration.SiteId)
                    .ShouldEqual(0);
            };

            private It should_have_not_null_payout_ids = () => {
                Data.ApiResult.Value.Payouts.Count(p => p.ID == null || p.ID <= 0).ShouldEqual(0);
            };
            private It should_have_not_null_merchant_transaction_ids = () => {
                Data.ApiResult.Value.Payouts.Count(p => string.IsNullOrWhiteSpace(p.MerchantTransactionID)).ShouldEqual(0);
            };
            private It should_have_not_null_amounts = () => {
                Data.ApiResult.Value.Payouts.Count(p => p.Amount == null || p.Amount <= 0).ShouldEqual(0);
            };
            private It should_have_not_null_currency = () => {
                Data.ApiResult.Value.Payouts.Count(p => string.IsNullOrWhiteSpace(p.Currency)).ShouldEqual(0);
            };
        }
    }
}