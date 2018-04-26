using System;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    public partial class CardPaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_requesting_card_payment_list_filtered_by_status
        {
            private static ApiResult<ApiCardPaymentListResponse> ApiListResult;
            private static CardPaymentsFilter PaymentsFilter;
            protected static CardPaymentFilteredBehaviourData Data;
            private const int Limit = 20;
            private static readonly DateTime StartDate = new DateTime(2018, 4, 9);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 11);
            private static readonly int StatusId = 9;


            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                PaymentsFilter = new CardPaymentsFilter { startDate = StartDate, endDate = EndDate, limit = 100, statusID = StatusId };
            };

            private Because of = () => {
                ApiListResult = CardPaymentService.GetPaymentListAsync(PaymentsFilter).GetAwaiter()
                    .GetResult();
                Data = new CardPaymentFilteredBehaviourData
                {
                    ApiListResult = ApiListResult,
                    Limit = Limit,
                    PaymentsFilter = PaymentsFilter
                };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<CardPaymentFilteredBehavior> a_list_of_filtered_payments_response;
        }
    }
}
