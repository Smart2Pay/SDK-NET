using System;
using System.Text;
using S2p.RestClient.Sdk.Infrastructure.Authentication;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static string ToAuthenticationToken(this AuthenticationConfiguration @this)
        {
            @this.ThrowIfNull(typeof(AuthenticationConfiguration).Name.ToLower());

            var bytesArray = Encoding.UTF8.GetBytes($"{@this.SiteId}:{@this.ApiKey}");
            var token = Convert.ToBase64String(bytesArray);

            return token;
        }
    }
}
