using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Validation
{
    public class InnerValidator
    {
        public IValidator Validator { get; private set; }
        public bool AllowNull { get; private set; }

        private InnerValidator() { }

        public static InnerValidator Create(IValidator validator, bool allowNull)
        {
            validator.ThrowIfNull(nameof(validator));
            return new InnerValidator { Validator = validator, AllowNull = allowNull };
        }
    }
}
