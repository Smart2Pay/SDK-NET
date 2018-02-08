using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class DummyClassValidator : AbstractValidator<DummyClass>
    {
        internal static readonly string IdValidationText = "Must be positive, non zero, non null";
        internal static readonly string QuantityValidationText = "Must be greater or equal to zero or null";

        public DummyClassValidator()
        {
            AddRuleFor(x => x.Id).WithErrorMessage(IdValidationText)
                .WithPredicate(x => x.Id.HasValue && x.Id.Value > 0);
            AddRuleFor(x => x.Quantity).WithErrorMessage(QuantityValidationText)
                .WithPredicate(x => !x.Quantity.HasValue || x.Quantity >= 0);
        }
    }
}
