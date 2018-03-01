using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure
{
    public static class HttpRequestResponseMessageExtensions
    {
        public const string JsonMediaType = "application/json";

        public static HttpRequestMessage ToHttpRequest<T>(this T @this, HttpMethod method, Uri uri)
        {
            @this.ThrowIfNull(typeof(T).Name.ToLower());
            method.ThrowIfNull(nameof(method));
            uri.ThrowIfNull(nameof(uri));

            var request = new HttpRequestMessage
            {
                Method = method,
                Content = new StringContent(JsonConvert.SerializeObject(@this), Encoding.UTF8,
                   JsonMediaType),
                RequestUri = uri
            };

            return request;
        }

        public static async Task<ApiResult> ToApiResult(this HttpResponseMessage @this, HttpRequestMessage request)
        {
            @this.ThrowIfNull(typeof(HttpResponseMessage).Name.ToLower());
            request.ThrowIfNull(nameof(request));

            var content = @this.Content == null ? string.Empty : await @this.Content.ReadAsStringAsync();
            return @this.IsSuccessStatusCode ? ApiResult.Success(request, @this, content) : ApiResult.Failure(request, @this, content);
        }

        public static async Task<ApiResult<T>> ToApiResult<T>(this HttpResponseMessage @this, HttpRequestMessage request)
        {
            @this.ThrowIfNull(typeof(HttpResponseMessage).Name.ToLower());
            request.ThrowIfNull(nameof(request));

            var jsonDeserializeResult = await @this.Content.ReadAsJsonAsync<T>();

            if (@this.IsSuccessStatusCode && jsonDeserializeResult.IsSuccess)
            {
                return ApiResult.Success(request, @this, jsonDeserializeResult.Content, jsonDeserializeResult.Value);
            }

            if (!@this.IsSuccessStatusCode && jsonDeserializeResult.IsSuccess)
            {
                return ApiResult.Failure(request, @this, jsonDeserializeResult.Content, jsonDeserializeResult.Value);
            }

            return ApiResult.Failure<T>(request, @this, jsonDeserializeResult.Content);
        }

        private static async Task<JsonDeserializeResult<T>> ReadAsJsonAsync<T>(this HttpContent @this)
        {
            var contentAsString = @this == null ? string.Empty : await @this.ReadAsStringAsync();
            return contentAsString.MapIf(content => {
                    var result = new JsonDeserializeResult<T> { Content = content, IsSuccess = true, Value = default(T) };
                    try
                    {
                        var deserializeObject = JsonConvert.DeserializeObject<T>(content);
                        result.Value = deserializeObject;
                    }
                    catch { result.IsSuccess = false; }
                    return result;
                }, content => !string.IsNullOrWhiteSpace(content),
                content => new JsonDeserializeResult<T> { Content = content, IsSuccess = false, Value = default(T) });
        }

        private class JsonDeserializeResult<T>
        {
            public T Value { get; set; }
            public bool IsSuccess { get; set; }
            public string Content { get; set; }
        }
    }
}
