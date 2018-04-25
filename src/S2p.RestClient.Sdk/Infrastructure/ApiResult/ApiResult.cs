using System;
using System.Net.Http;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure
{
    public class ApiResult
    {
        public bool IsSuccess { get; protected internal set; }
        public HttpRequestMessage HttpRequest { get; protected internal set; }
        public HttpResponseMessage HttpResponse { get; protected internal set; }
        public string Content { get; protected internal set; }
        public Exception Exception { get; protected internal set; }

        public static ApiResult Success(HttpRequestMessage request, HttpResponseMessage response, string content)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));

            return new ApiResult
            {
                IsSuccess = true,
                HttpRequest = request,
                HttpResponse = response,
                Content = content.ValueIfNull(() => string.Empty)
            };
        }

        public static ApiResult<T> Success<T>(HttpRequestMessage request, HttpResponseMessage response, string content, T value)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));
            value.ThrowIfNull(nameof(value));

            return new ApiResult<T>
            {
                IsSuccess = true,
                HttpRequest = request,
                HttpResponse = response,
                Content = content.ValueIfNull(() => string.Empty),
                Value = value
            };
        }

        public static ApiResult Failure(HttpRequestMessage request, HttpResponseMessage response, string content)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));

            return new ApiResult
            {
                IsSuccess = false,
                HttpRequest = request,
                Content = content.ValueIfNull(() => string.Empty),
                HttpResponse = response
            };
        }

        public static ApiResult Failure(HttpRequestMessage request, Exception exception)
        {
            request.ThrowIfNull(nameof(request));
            exception.ThrowIfNull(nameof(exception));

            return new ApiResult
            {
                IsSuccess = false,
                HttpRequest = request,
                Content = string.Empty,
                Exception = exception
            };
        }

        public static ApiResult<T> Failure<T>(HttpRequestMessage request, HttpResponseMessage response, string content)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));

            return new ApiResult<T>
            {
                IsSuccess = false,
                HttpRequest = request,
                HttpResponse = response,
                Content = content.ValueIfNull(() => string.Empty),
                Value = default(T)
            };
        }

        public static ApiResult<T> Failure<T>(HttpRequestMessage request, HttpResponseMessage response, string content, T value)
        {
            request.ThrowIfNull(nameof(request));
            response.ThrowIfNull(nameof(response));
            value.ThrowIfNull(nameof(value));

            return new ApiResult<T>
            {
                IsSuccess = false,
                HttpRequest = request,
                HttpResponse = response,
                Content = content.ValueIfNull(() => string.Empty),
                Value = value
            };
        }

        public static ApiResult<T> Failure<T>(HttpRequestMessage request, Exception exception)
        {
            request.ThrowIfNull(nameof(request));
            exception.ThrowIfNull(nameof(exception));

            return new ApiResult<T>
            {
                IsSuccess = false,
                HttpRequest = request,
                Exception = exception,
                Content = string.Empty,
                Value = default(T)
            };
        }

        public static ApiResult<T> Failure<T>(Exception exception)
        {
            exception.ThrowIfNull(nameof(exception));

            return new ApiResult<T>
            {
                IsSuccess = false,
                Exception = exception,
                Content = string.Empty,
                Value = default(T)
            };
        }
    }
}
