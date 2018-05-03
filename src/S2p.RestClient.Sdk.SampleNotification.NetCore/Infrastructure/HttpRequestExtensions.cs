using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.SampleNotification.NetCore.Infrastructure
{
    public static class HttpRequestExtensions
    {
        public static Task<string> ReadBodyAsStringAsync(this HttpRequest request, Encoding encoding)
        {
            request.ThrowIfNull(nameof(request));
            encoding.ThrowIfNull(nameof(encoding));

            using (var reader = new StreamReader(request.Body, encoding))
            {
                return reader.ReadToEndAsync();
            }
        }

        public static Task<string> ReadBodyAsStringAsync(this HttpRequest request)
        {
            return ReadBodyAsStringAsync(request, Encoding.UTF8);
        }
    }
}
