using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.ApiResult
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<ApiResult<T>> ToApiResult<T>(this HttpResponseMessage @this, HttpRequestMessage request)
        {
            @this.ThrowIfNull(typeof(HttpResponseMessage).Name.ToLower());
            request.ThrowIfNull(nameof(request));

            if (!@this.IsSuccessStatusCode) return ApiResult.Failure<T>(request, @this);
            var value = await @this.Content.ReadAsJsonAsync<T>();
            return ApiResult.Success(request, @this, value);
        }

        internal static async Task<T> ReadAsJsonAsync<T>(this HttpContent @this)
        {
            @this.ThrowIfNull(typeof(HttpContent).Name.ToLower());

            var text = await @this.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(text);

            return result;
        }
    }
}
