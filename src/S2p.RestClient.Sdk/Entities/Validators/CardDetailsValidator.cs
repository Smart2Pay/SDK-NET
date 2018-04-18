using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;
using System.Text.RegularExpressions;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class CardDetailsValidator : AbstractValidator<CardDetails>
    {
        public CardDetailsValidator()
        {
            AddRuleFor(x => x.HolderName)
               .WithPredicate(x => string.IsNullOrWhiteSpace(x.HolderName) || Regex.IsMatch(x.HolderName, ValidationRegexConstants.CardHolderName))
               .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.HolderName, ValidationRegexConstants.CardHolderName));
            AddRuleFor(x => x.Number)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.HolderName) || Regex.IsMatch(x.Number, ValidationRegexConstants.CardNumber))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.Number, ValidationRegexConstants.CardNumber));
            AddRuleFor(x => x.Type)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Type) || Regex.IsMatch(x.Type, ValidationRegexConstants.CardType))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.Type, ValidationRegexConstants.CardType));
            AddRuleFor(x => x.ExpirationMonth)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.ExpirationMonth) || Regex.IsMatch(x.ExpirationMonth, ValidationRegexConstants.CardExpirationMonth))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.ExpirationMonth, ValidationRegexConstants.CardExpirationMonth));
            AddRuleFor(x => x.ExpirationYear)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.ExpirationYear) || Regex.IsMatch(x.ExpirationYear, ValidationRegexConstants.CardExpirationYear))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.ExpirationYear, ValidationRegexConstants.CardExpirationYear));
            AddRuleFor(x => x.SecurityCode)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.SecurityCode) || Regex.IsMatch(x.SecurityCode, ValidationRegexConstants.CardSecurityCode))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.SecurityCode, ValidationRegexConstants.CardSecurityCode));
            AddRuleFor(x => x.Installments)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Installments) || Regex.IsMatch(x.Installments, ValidationRegexConstants.Installment))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetails>(x => x.Installments, ValidationRegexConstants.Installment));
        }
    }
}
