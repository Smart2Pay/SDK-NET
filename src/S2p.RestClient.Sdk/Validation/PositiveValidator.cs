namespace S2p.RestClient.Sdk.Validation
{
    public class PositiveValidator : IValidator
    {
        public static readonly PositiveValidator Instance = new PositiveValidator();

        private PositiveValidator() { }
        public ValidationResult Validate(object objectToValidate)
        {
            return new ValidationResult();
        }
    }
}
