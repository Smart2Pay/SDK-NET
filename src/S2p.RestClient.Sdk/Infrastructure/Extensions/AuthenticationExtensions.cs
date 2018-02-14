using System;
using System.Text;
using S2p.RestClient.Sdk.Infrastructure.Authentication;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static string ToAuthenticationToken(this AuthenticationConfiguration @this)
        {
            @this.ThrowIfNull("Cannot create token from null auth data");

            var bytesArray = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", @this.SiteId, @this.ApiKey));
            var token = Convert.ToBase64String(bytesArray);

            return token;
        }
    }
}
