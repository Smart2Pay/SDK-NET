using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Helpers;
using S2p.RestClient.Sdk.Validation;
using System.Globalization;
using System.Text.RegularExpressions;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            var addressValidator = new AddressValidator();
            var customerValidator = new CustomerValidator();
            var articleValidator = new ArticleValidator();

            AddRuleFor(x => x.ID)
                .WithPredicate(x => Regex.IsMatch(x.ID.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.ID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.ID, ValidationRegexConstants.ID));
            AddRuleFor(x => x.SkinID)
                .WithPredicate(x => x.SkinID == null || Regex.IsMatch(x.SkinID.Value.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.SkinID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.SkinID, ValidationRegexConstants.SkinID));
            AddRuleFor(x => x.MerchantTransactionID)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.MerchantTransactionID) && Regex.IsMatch(x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID));
            AddRuleFor(x => x.OriginatorTransactionID)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.OriginatorTransactionID) || Regex.IsMatch(x.OriginatorTransactionID, ValidationRegexConstants.OriginatorTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.OriginatorTransactionID, ValidationRegexConstants.OriginatorTransactionID));
            AddRuleFor(x => x.Amount)
                .WithPredicate(x => x.Amount > 0 && Regex.IsMatch(x.Amount.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.Amount))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.Amount));
            AddRuleFor(x => x.Currency)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.Currency) && CurrencyValidationHelper.CurrencyExists(x.Currency))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.Currency));
            AddRuleFor(x => x.ReturnURL)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.ReturnURL) && Regex.IsMatch(x.ReturnURL, ValidationRegexConstants.ReturnURL))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.ReturnURL, ValidationRegexConstants.ReturnURL));
            AddRuleFor(x => x.Description)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Description) || Regex.IsMatch(x.Description, ValidationRegexConstants.Description))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.Description, ValidationRegexConstants.Description));
            AddRuleFor(x => x.MethodID)
                .WithPredicate(x => x.MethodID == null || Regex.IsMatch(x.MethodID.Value.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.MethodID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.MethodID, ValidationRegexConstants.MethodID));
            AddRuleFor(x => x.MethodOptionID)
                .WithPredicate(x => x.MethodOptionID == null || Regex.IsMatch(x.MethodOptionID.Value.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.MethodOptionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.MethodOptionID, ValidationRegexConstants.MethodOptionID));
            AddRuleFor(x => x.Guaranteed)
                .WithPredicate(x => x.Guaranteed == null || Regex.IsMatch(x.Guaranteed.ToString(), ValidationRegexConstants.Guaranteed))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.Guaranteed, ValidationRegexConstants.Guaranteed));
            AddRuleFor(x => x.RedirectInIframe)
                .WithPredicate(x => x.RedirectInIframe == null || Regex.IsMatch(x.RedirectInIframe.ToString(), ValidationRegexConstants.Guaranteed))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.RedirectInIframe, ValidationRegexConstants.Guaranteed));
            AddRuleFor(x => x.RedirectMerchantInIframe)
                .WithPredicate(x => x.RedirectMerchantInIframe == null || Regex.IsMatch(x.RedirectMerchantInIframe.ToString(), ValidationRegexConstants.Guaranteed))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.RedirectMerchantInIframe, ValidationRegexConstants.Guaranteed));
            AddRuleFor(x => x.Language)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Language) || Regex.IsMatch(x.Language, ValidationRegexConstants.Language))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.Language, ValidationRegexConstants.Language));

            AddInnerValidatorFor(x => x.BillingAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.ShippingAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.Customer, () => InnerValidator.Create(customerValidator, true));

            AddInnerValidatorFor(x => x.Articles, () => InnerValidator.Create(new EnumerableValidator<Article>(articleValidator), true));
           
        }
    }
}
