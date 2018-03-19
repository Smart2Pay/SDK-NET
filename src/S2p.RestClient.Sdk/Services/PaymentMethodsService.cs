using System;
using System.Collections.Generic;
using System.Text;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public class PaymentMethodsService : ServiceBase
    {
        private const string PaymentMethodsUrl = "/v1/methods";

        public PaymentMethodsService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder) { }


    }
}
