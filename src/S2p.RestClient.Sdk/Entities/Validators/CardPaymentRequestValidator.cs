using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Helper;
using S2p.RestClient.Sdk.Validation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class CardPaymentRequestValidator : AbstractValidator<CardPaymentRequest>
    {
        public CardPaymentRequestValidator()
        {
            var addressValidator = new AddressValidator();
            var customerValidator = new CustomerValidator();
            var cardDetailsRequestValidator = new CardDetailsRequestValidator();
            var creditCardTokenRequestValidator = new CreditCardTokenDetailsRequestValidator();

            AddRuleFor(x => x.MerchantTransactionID)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.MerchantTransactionID) && Regex.IsMatch(x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID));
            AddRuleFor(x => x.OriginatorTransactionID)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.OriginatorTransactionID) || Regex.IsMatch(x.OriginatorTransactionID, ValidationRegexConstants.OriginatorTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.OriginatorTransactionID, ValidationRegexConstants.OriginatorTransactionID));
            AddRuleFor(x => x.Amount)
                .WithPredicate(x => x.Amount > 0 && Regex.IsMatch(x.Amount.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.Amount))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPaymentRequest>(x => x.Amount));
            AddRuleFor(x => x.Currency)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.Currency) && Currency.Exists(x.Currency))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPaymentRequest>(x => x.Currency));
            AddRuleFor(x => x.ReturnURL)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.ReturnURL) || Regex.IsMatch(x.ReturnURL, ValidationRegexConstants.ReturnURL))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPaymentRequest>(x => x.ReturnURL, ValidationRegexConstants.ReturnURL));
            AddRuleFor(x => x.Description)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Description) || Regex.IsMatch(x.Description, ValidationRegexConstants.Description))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPaymentRequest>(x => x.Description, ValidationRegexConstants.Description));
            AddRuleFor(x => x.Language)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Language) || Regex.IsMatch(x.Language, ValidationRegexConstants.Language))
                .WithErrorMessage(Operator.InvalidPropertyMessage<CardPaymentRequest>(x => x.Language, ValidationRegexConstants.Language));

            AddInnerValidatorFor(x => x.Customer, () => InnerValidator.Create(customerValidator, true));

            AddInnerValidatorFor(x => x.BillingAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.ShippingAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.Card, () => InnerValidator.Create(cardDetailsRequestValidator, true));

            AddInnerValidatorFor(x => x.CreditCardToken, () => InnerValidator.Create(creditCardTokenRequestValidator, true));
        }
    }
}
