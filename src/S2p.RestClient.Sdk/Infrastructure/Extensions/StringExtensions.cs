namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string UrlEncoded(this string @this)
        {
            return System.Net.WebUtility.UrlEncode(@this.ValueIfNull(() => string.Empty));
        }
    }
}
