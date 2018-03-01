using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Services
{
    public class PaymentService : ServiceBase
    {
        private Uri CreatePaymentUri { get; }

        public PaymentService(IHttpClientBuilder httpClientBuilder) : base(httpClientBuilder)
        {
            CreatePaymentUri = new Uri(HttpClient.BaseAddress, "v1/payments");
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest, 
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, CreatePaymentUri);
           return HttpClient.Invoke<RestPaymentResponse>(request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest)
        {
            return CreatePaymentAsync(paymentRequest, CancellationToken.None);
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest, string idempotencyToken,
            CancellationToken cancellationToken)
        {
            paymentRequest.ThrowIfNull(nameof(paymentRequest));
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            var request = paymentRequest.ToHttpRequest(HttpMethod.Post, CreatePaymentUri);
            return HttpClient.Invoke<RestPaymentResponse>(idempotencyToken, request, cancellationToken);
        }

        public Task<ApiResult<RestPaymentResponse>> CreatePaymentAsync(RestPaymentRequest paymentRequest, string idempotencyToken)
        {
            return CreatePaymentAsync(paymentRequest, idempotencyToken, CancellationToken.None);
        }
    }
}
