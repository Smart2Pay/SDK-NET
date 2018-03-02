using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class DummyWrapperClassValidatorNotNull : AbstractValidator<DummyWrapperClass>
    {
        internal static readonly string IdValidationText = "Must be positive, non zero, non null";

        public DummyWrapperClassValidatorNotNull()
        {
            AddRuleFor(x => x.Id).WithPredicate(x => x.Id != null && x.Id > 0)
                .WithErrorMessage(IdValidationText);
            AddInnerValidatorFor(x => x.DummyClass,
                () => InnerValidator.Create(new DummyClassValidator(), false));
        }
    }
}
