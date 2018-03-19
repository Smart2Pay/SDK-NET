using System;
using System.Threading;
using System.Threading.Tasks;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Services
{
    public interface IPaymentMethodService : IDisposable
    {
        Task<ApiResult<ApiPaymentMethodResponse>> GetPaymentMethodAsync(string paymentMethodId,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentMethodResponse>> GetPaymentMethodAsync(string paymentMethodId);
        Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync();

        Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync(string countryCode,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentMethodListResponse>> GetPaymentMethodsListAsync(string countryCode);
        Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync(CancellationToken cancellationToken);
        Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync();

        Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync(string countryCode,
            CancellationToken cancellationToken);

        Task<ApiResult<ApiPaymentMethodListResponse>> GetAssignedPaymentMethodsListAsync(string countryCode);
    }
}