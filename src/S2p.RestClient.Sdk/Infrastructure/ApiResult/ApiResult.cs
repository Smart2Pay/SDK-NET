using System;
using System.Net.Http;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure
{
    public class ApiResult
    {
        public bool IsSuccess { get; protected internal set; }
        public HttpRequestMessage Request { get; protected internal set; }
        public HttpResponseMessage Response { get; protected internal set; }
        public Exception Exception { get; protected internal set; }

        public static ApiResult Success(HttpRequestMessage request, HttpResponseMessage response)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));

            return new ApiResult
            {
                IsSuccess = true,
                Request = request,
                Response = response
            };
        }

        public static ApiResult<T> Success<T>(HttpRequestMessage request, HttpResponseMessage response, T value)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));
            value.ThrowIfNull(nameof(value));

            return new ApiResult<T>
            {
                IsSuccess = true,
                Request = request,
                Response = response,
                Value = value
            };
        }

        public static ApiResult Failure(HttpRequestMessage request, HttpResponseMessage response)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));

            return new ApiResult
            {
                IsSuccess = false,
                Request = request,
                Response = response
            };
        }

        public static ApiResult Failure(HttpRequestMessage request, Exception exception)
        {
            request.ThrowIfNull(nameof(request));
            exception.ThrowIfNull(nameof(exception));

            return new ApiResult
            {
                IsSuccess = false,
                Request = request,
                Exception = exception
            };
        }

        public static ApiResult<T> Failure<T>(HttpRequestMessage request, HttpResponseMessage response)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));

            return new ApiResult<T>
            {
                IsSuccess = false,
                Request = request,
                Response = response,
                Value = default(T)
            };
        }

        public static ApiResult<T> Failure<T>(HttpRequestMessage request, Exception exception)
        {
            request.ThrowIfNull(nameof(request));
            exception.ThrowIfNull(nameof(exception));

            return new ApiResult<T>
            {
                IsSuccess = false,
                Request = request,
                Exception = exception,
                Value = default(T)
            };
        }
    }
}
