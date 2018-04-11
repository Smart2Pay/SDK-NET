using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PayoutService;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    public partial class CardPaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_requesting_card_payment_list
        {
            private static ApiResult<ApiCardPaymentListResponse> ApiListResult;
            private static CardPaymentsFilter PaymentsFilter;
            protected static CardPaymentFilteredBehaviourData Data;

            private Establish context = () => {
                ServiceTestsConstants.EnableTLS12();
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                PaymentsFilter = new CardPaymentsFilter { };
            };

            private Because of = () => {
                ApiListResult = CardPaymentService.GetPaymentListAsync().GetAwaiter().GetResult();
                Data = new CardPaymentFilteredBehaviourData
                {
                    ApiListResult = ApiListResult,
                    PaymentsFilter = PaymentsFilter
                };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            private It should_have_non_empty_payments_list = () => { ApiListResult.Value.Payments.Count.ShouldBeGreaterThan(0); };

            Behaves_like<CardPaymentFilteredBehavior> a_list_of_filtered_card_payments_response;

        }

        [Behaviors]
        public class CardPaymentFilteredBehavior
        {
            protected static CardPaymentFilteredBehaviourData Data;

            private It should_be_successful = () => { Data.ApiListResult.IsSuccess.ShouldBeTrue(); };

            private It should_have_ok_http_status = () => {
                Data.ApiListResult.HttpResponse.StatusCode.ShouldEqual(HttpStatusCode.OK);
            };

            private It should_have_correct_number_of_card_payments_in_list = () => {
                if (Data.Limit.HasValue)
                {
                    Data.ApiListResult.Value.Payments.Count.ShouldEqual(Data.Limit.Value);
                }
                else
                {
                    Data.ApiListResult.Value.Payments.Count.ShouldBeGreaterThan(0);
                }
            };

            private It should_have_correct_site_id = () => {
                Data.ApiListResult.Value.Payments.Count(p => p.SiteID == null ||
                                                        p.SiteID.Value != ServiceTestsConstants
                                                            .PayoutAuthenticationConfiguration.SiteId)
                    .ShouldEqual(0);
            };

            private It should_have_not_null_payment_ids = () => {
                Data.ApiListResult.Value.Payments.Count(p => p.ID == null || p.ID <= 0).ShouldEqual(0);
            };

            private It should_have_not_null_merchant_transaction_ids = () => {
                Data.ApiListResult.Value.Payments.Count(p => string.IsNullOrWhiteSpace(p.MerchantTransactionID)).ShouldEqual(0);
            };

            private It should_have_not_null_amounts = () => {
                Data.ApiListResult.Value.Payments.Count(p => p.Amount == null || p.Amount <= 0).ShouldEqual(0);
            };

            private It should_have_not_null_currency = () => {
                Data.ApiListResult.Value.Payments.Count(p => string.IsNullOrWhiteSpace(p.Currency)).ShouldEqual(0);
            };   
        }

        public class CardPaymentFilteredBehaviourData
        {
            public ApiResult<ApiCardPaymentListResponse> ApiListResult { get; set; }
            public int? Limit { get; set; }
            public CardPaymentsFilter PaymentsFilter { get; set; }

        }
    }
}
