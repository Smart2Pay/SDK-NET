namespace S2p.RestClient.Sdk.Infrastructure
{
    public class ApiResult<T> : ApiResult
    {
        public T Value { get; protected internal set; }
    }
}
