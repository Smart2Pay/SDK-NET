using System.Threading.Tasks;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class TaskExtensions
    {
        public static Task<T> ToAwaitable<T>(this T @this)
        {
            return Task.FromResult(@this);
        }
    }
}
