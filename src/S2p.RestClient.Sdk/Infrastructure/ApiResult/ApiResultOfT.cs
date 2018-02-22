namespace S2p.RestClient.Sdk.Infrastructure.ApiResult
{
    public class ApiResult<T> : ApiResult
    {
        public T Value { get; protected internal set; }
    }
}
