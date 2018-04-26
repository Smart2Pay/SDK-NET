using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    public partial class CardPaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_requesting_card_payment_list_filtered_by_limit_and_offset
        {
            private static ApiResult<ApiCardPaymentListResponse> ApiListResult;
            private static CardPaymentsFilter PaymentsFilter;
            protected static CardPaymentFilteredBehaviourData Data;
            private const int Limit = 5;
            private const long Offset = 279245;

            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                PaymentsFilter = new CardPaymentsFilter {limit = Limit, offset = Offset};
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
