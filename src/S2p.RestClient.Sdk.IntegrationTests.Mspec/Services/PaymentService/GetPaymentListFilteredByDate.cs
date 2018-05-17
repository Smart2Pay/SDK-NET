using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using System;

namespace S2p.RestClient.Sdk.IntegrationTests.Mspec.Services.PaymentService
{
    public partial class PaymentServiceTests
    {
        [Subject(typeof(Sdk.Services.AlternativePaymentService))]
        public class When_requesting_payment_list_filtered_by_date
        {
            private static ApiResult<ApiPaymentListResponse> ApiListResult;
            private static PaymentsFilter PaymentsFilter;
            protected static PaymentFilteredBehaviourData Data;
            private const int Limit = 27;
            private static readonly DateTime StartDate = new DateTime(2018, 4, 3);
            private static readonly DateTime EndDate = new DateTime(2018, 4, 4);


            private Establish context = () => {
                InitializeHttpBuilder();
                HttpClient = HttpClientBuilder.Build();
                _alternativePaymentService = new Sdk.Services.AlternativePaymentService(HttpClient, BaseAddress);
                PaymentsFilter = new PaymentsFilter {startDate = StartDate, endDate = EndDate, pageSize = 100};
            };

            private Because of = () => {
                ApiListResult = PaymentServiceTests._alternativePaymentService.GetPaymentListAsync(PaymentsFilter).GetAwaiter()
                    .GetResult();
                Data = new PaymentFilteredBehaviourData
                {
                    ApiListResult = ApiListResult,
                    Limit = Limit,
                    PaymentsFilter = PaymentsFilter
                };
            };

            private Cleanup after = () => { HttpClient.Dispose(); };

            Behaves_like<PaymentFilteredBehavior> a_list_of_filtered_payments_response;
        }


    }
}
