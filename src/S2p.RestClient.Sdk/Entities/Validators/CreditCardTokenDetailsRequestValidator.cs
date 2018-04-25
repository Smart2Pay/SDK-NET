using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;
using System.Text.RegularExpressions;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class CreditCardTokenDetailsRequestValidator : AbstractValidator<CreditCardTokenDetailsRequest>
    {
        public CreditCardTokenDetailsRequestValidator()
        {
            AddRuleFor(x => x.Value)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.Value))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CreditCardTokenDetailsRequest>(x => x.Value));
            AddRuleFor(x => x.SecurityCode)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.SecurityCode) || Regex.IsMatch(x.SecurityCode, ValidationRegexConstants.CardSecurityCode))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CreditCardTokenDetailsRequest>(x => x.SecurityCode, ValidationRegexConstants.CardSecurityCode));

        }
    }
}
