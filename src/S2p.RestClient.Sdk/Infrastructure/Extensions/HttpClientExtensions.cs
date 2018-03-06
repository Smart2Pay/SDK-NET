using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<ApiResult> InvokeAsync(this HttpClient @this, HttpRequestMessage request)
        {
            return @this.InvokeAsync(request, CancellationToken.None);
        }

        public static Task<ApiResult> InvokeAsync(this HttpClient @this, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return @this.InvokeAsync(request, (c, r, ct) => c.SendAsync(r, ct), cancellationToken);
        }

        public static Task<ApiResult> InvokeAsync(this HttpClient @this, string idempotencyToken, HttpRequestMessage request)
        {
            return @this.InvokeAsync(idempotencyToken, request, CancellationToken.None);
        }

        public static Task<ApiResult> InvokeAsync(this HttpClient @this, string idempotencyToken, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            return @this.InvokeAsync(request, (c, r, ct) => c.SendWithIdempotencyTokenAsync(idempotencyToken, r, ct), cancellationToken);
        }

        public static Task<ApiResult<T>> InvokeAsync<T>(this HttpClient @this, HttpRequestMessage request)
        {
            return @this.InvokeAsync<T>(request, CancellationToken.None);
        }

        public static Task<ApiResult<T>> InvokeAsync<T>(this HttpClient @this, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return @this.InvokeAsync<T>(request, (c, r, ct) => c.SendAsync(r, ct), cancellationToken);
        }

        public static Task<ApiResult<T>> InvokeAsync<T>(this HttpClient @this, string idempotencyToken, HttpRequestMessage request)
        {
            return @this.InvokeAsync<T>(idempotencyToken, request, CancellationToken.None);
        }

        public static Task<ApiResult<T>> InvokeAsync<T>(this HttpClient @this, string idempotencyToken, HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));
            return @this.InvokeAsync<T>(request, (c, r, ct) => c.SendWithIdempotencyTokenAsync(idempotencyToken, r, ct), cancellationToken);
        }

        private static async Task<ApiResult<T>> InvokeAsync<T>(this HttpClient @this, HttpRequestMessage request,
            Func<HttpClient, HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>invocation, CancellationToken cancellationToken)
        {
            @this.ThrowIfNull(typeof(HttpClient).Name.ToLower());
            request.ThrowIfNull(nameof(request));
            invocation.ThrowIfNull(nameof(invocation));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            ApiResult<T> apiResult;
            try
            {
                var response = await invocation(@this, request, cancellationToken);
                apiResult = await response.ToApiResult<T>(request);
            }
            catch (Exception e)
            {
                apiResult = ApiResult.Failure<T>(request, e);
            }

            return apiResult;
        }

        private static async Task<ApiResult> InvokeAsync(this HttpClient @this, HttpRequestMessage request,
            Func<HttpClient, HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> invocation, CancellationToken cancellationToken)
        {
            @this.ThrowIfNull(typeof(HttpClient).Name.ToLower());
            request.ThrowIfNull(nameof(request));
            invocation.ThrowIfNull(nameof(invocation));
            cancellationToken.ThrowIfNull(nameof(cancellationToken));

            ApiResult apiResult;
            try
            {
                var response = await invocation(@this, request, cancellationToken);
                apiResult = await response.ToApiResult(request);
            }
            catch (Exception e)
            {
                apiResult = ApiResult.Failure(request, e);
            }

            return apiResult;
        }

        private static Task<HttpResponseMessage> SendWithIdempotencyTokenAsync(this HttpClient @this, string idempotencyToken,
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            @this.ThrowIfNull(typeof(HttpClient).Name.ToLower());
            idempotencyToken.ThrowIfNullOrWhiteSpace(nameof(idempotencyToken));

            try
            {
                @this.DefaultRequestHeaders.AddIdempotencyHeader(idempotencyToken);
                return @this.SendAsync(request, cancellationToken);
            }
            finally
            {
                @this.DefaultRequestHeaders.RemoveIdempotencyHeader();
            }
        }
    }
}
