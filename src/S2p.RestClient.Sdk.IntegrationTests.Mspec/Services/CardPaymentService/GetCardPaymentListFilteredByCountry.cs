using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Services;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.CardPaymentService
{
    partial class CardPaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.CardPaymentService))]
        public class When_requesting_card_payment_list_filtered_by_country
        {
            private static ApiResult<ApiCardPaymentListResponse> ApiListResult;
            private static CardPaymentsFilter PaymentsFilter;
            protected static CardPaymentFilteredBehaviourData Data;
            private const int Limit = 68;
            private static readonly DateTime StartDate = new DateTime(2018, 4, 9);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 11);
            private static readonly string Country = "US";


            private Establish context = () =>
            {
                ServiceTestsConstants.EnableTLS12();
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                CardPaymentService = new Sdk.Services.CardPaymentService(HttpClient, BaseAddress);
                PaymentsFilter = new CardPaymentsFilter { startDate = StartDate, endDate = EndDate, limit = 100, country = Country };
            };

            private Because of = () =>
            {
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

            Behaves_like<CardPaymentFilteredBehavior> a_list_of_filtered_card_payments_response;
        }

    }
}
