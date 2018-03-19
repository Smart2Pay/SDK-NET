using System.Collections.Generic;
using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class ApiPaymentListResponse
    {
        public List<PaymentResponse> Payments { get; set; }

        public int Count { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }
    }
}
