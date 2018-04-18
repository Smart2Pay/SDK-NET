using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class CardDetailsRequestValidator : AbstractValidator<CardDetailsRequest>
    {
        public CardDetailsRequestValidator()
        {
            AddRuleFor(x => x.HolderName)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.HolderName) && Regex.IsMatch(x.HolderName, ValidationRegexConstants.CardHolderName))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetailsRequest>(x => x.HolderName, ValidationRegexConstants.CardHolderName));
            AddRuleFor(x => x.Number)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.HolderName) && Regex.IsMatch(x.Number, ValidationRegexConstants.CardNumber))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetailsRequest>(x => x.Number, ValidationRegexConstants.CardNumber));
            AddRuleFor(x => x.ExpirationMonth)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.ExpirationMonth) && Regex.IsMatch(x.ExpirationMonth, ValidationRegexConstants.CardExpirationMonth))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetailsRequest>(x => x.ExpirationMonth, ValidationRegexConstants.CardExpirationMonth));
            AddRuleFor(x => x.ExpirationYear)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.ExpirationYear) && Regex.IsMatch(x.ExpirationYear, ValidationRegexConstants.CardExpirationYear))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetailsRequest>(x => x.ExpirationYear, ValidationRegexConstants.CardExpirationYear));
            AddRuleFor(x => x.SecurityCode)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.SecurityCode) || Regex.IsMatch(x.SecurityCode, ValidationRegexConstants.CardSecurityCode))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetailsRequest>(x => x.SecurityCode, ValidationRegexConstants.CardSecurityCode));
            AddRuleFor(x => x.MaskedNumber)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.MaskedNumber) || Regex.IsMatch(x.MaskedNumber, ValidationRegexConstants.MaskCardNumber))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardDetailsRequest>(x => x.MaskedNumber, ValidationRegexConstants.MaskCardNumber));

        }
    }
}
