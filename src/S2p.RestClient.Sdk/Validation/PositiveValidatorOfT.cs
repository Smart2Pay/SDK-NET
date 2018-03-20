namespace S2p.RestClient.Sdk.Validation
{
    public class PositiveValidator<T> : IValidator<T>
    {
        public ValidationResult Validate(T objectToValidate)
        {
            return new ValidationResult();
        }

        public ValidationResult Validate(object objectToValidate)
        {
            return Validate((T) objectToValidate);
        }
    }
}
