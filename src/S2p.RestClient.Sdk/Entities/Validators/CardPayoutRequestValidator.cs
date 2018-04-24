using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Helper;
using S2p.RestClient.Sdk.Validation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class CardPayoutRequestValidator : AbstractValidator<CardPayoutRequest>
    {
        public CardPayoutRequestValidator()
        {
            var addressValidator = new AddressValidator();
            var customerValidator = new CustomerValidator();
            var cardValidator = new CardDetailsRequestValidator();

            AddRuleFor(x => x.MerchantTransactionID)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.MerchantTransactionID) && Regex.IsMatch(x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPayoutRequest>(x => x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID));
            AddRuleFor(x => x.Amount)
                .WithPredicate(x => x.Amount > 0 && Regex.IsMatch(x.Amount.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.Amount))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPayoutRequest>(x => x.Amount));
            AddRuleFor(x => x.Currency)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.Currency) && Currency.Exists(x.Currency))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPayoutRequest>(x => x.Currency));
            AddRuleFor(x => x.Description)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Description) || Regex.IsMatch(x.Description, ValidationRegexConstants.Description))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPayoutRequest>(x => x.Description, ValidationRegexConstants.Description));
            AddRuleFor(x => x.StatementDescriptor)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.StatementDescriptor) || Regex.IsMatch(x.StatementDescriptor, ValidationRegexConstants.StatementDescriptor))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPayoutRequest>(x => x.Description, ValidationRegexConstants.StatementDescriptor));

            AddInnerValidatorFor(x => x.BillingAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.Customer, () => InnerValidator.Create(customerValidator, true));

            AddInnerValidatorFor(x => x.Card, () => InnerValidator.Create(cardValidator, true));

        }
    }
}