namespace S2p.RestClient.Sdk.Validation
{
    public interface IValidator<T> : IValidator
    {
        ValidationResult Validate(T objectToValidate);
    }
}
