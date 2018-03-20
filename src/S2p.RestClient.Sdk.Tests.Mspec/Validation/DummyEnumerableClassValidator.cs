using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class DummyEnumerableClassValidator : AbstractValidator<DummyEnumerableClass>
    {
        public DummyEnumerableClassValidator()
        {
            AddInnerValidatorFor(x => x.DummyClassList, () => InnerValidator.Create(
                new EnumerableValidator<DummyClass>(new DummyClassValidator()), false));
        }
    }
}
