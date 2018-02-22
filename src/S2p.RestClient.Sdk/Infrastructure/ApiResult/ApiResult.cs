using System;
using System.Net;
using System.Net.Http;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.ApiResult
{
    public class ApiResult
    {
        public HttpRequestMessage Request { get; protected internal set; }
        public bool IsSuccess { get; protected internal set; }
        public HttpStatusCode StatusCode { get; protected internal set; }
        public Exception Error { get; protected internal set; }

        public static ApiResult Success(HttpRequestMessage request, HttpStatusCode httpStatusCode)
        {
            request.ThrowIfNull(nameof(request));
            httpStatusCode.ThrowIfNull(nameof(httpStatusCode));

            return new ApiResult
            {
                IsSuccess = true,
                Request = request,
                StatusCode = httpStatusCode
            };
        }

        public static ApiResult<T> Success<T>(HttpRequestMessage request, HttpStatusCode httpStatusCode, T value)
        {
            request.ThrowIfNull(nameof(request));
            httpStatusCode.ThrowIfNull(nameof(httpStatusCode));
            value.ThrowIfNull(nameof(value));

            return new ApiResult<T>
            {
                IsSuccess = true,
                Request = request,
                StatusCode = httpStatusCode,
                Value = value
            };
        }

        public static ApiResult Failure(HttpRequestMessage request, HttpStatusCode httpStatusCode)
        {
            request.ThrowIfNull(nameof(request));
            httpStatusCode.ThrowIfNull(nameof(httpStatusCode));

            return new ApiResult
            {
                IsSuccess = false,
                Request = request,
                StatusCode = httpStatusCode
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
                Error = exception
            };
        }

        public static ApiResult<T> Failure<T>(HttpRequestMessage request, HttpStatusCode httpStatusCode)
        {
            request.ThrowIfNull(nameof(request));
            httpStatusCode.ThrowIfNull(nameof(httpStatusCode));

            return new ApiResult<T>
            {
                IsSuccess = false,
                Request = request,
                StatusCode = httpStatusCode,
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
                Error = exception,
                Value = default(T)
            };
        }
    }
}
