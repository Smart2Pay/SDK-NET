namespace S2p.RestClient.Sdk.Validation
{
    public interface IValidator
    {
        ValidationResult Validate(object objectToValidate);
    }
}
