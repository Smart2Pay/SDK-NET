namespace S2p.RestClient.Sdk.Validation
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T obj);
    }
}
