using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;
using System.Globalization;
using System.Text.RegularExpressions;
using S2p.RestClient.Sdk.Entities.Validators;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class RefundRequestValidator : AbstractValidator<RefundRequest>
    {
        public RefundRequestValidator()
        {
            var addressValidator = new AddressValidator();
            var customerValidator = new CustomerValidator();
            var articleValidator = new ArticleValidator();

            AddRuleFor(x => x.ID)
                .WithPredicate(x => Regex.IsMatch(x.ID.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.ID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<RefundRequest>(x => x.ID, ValidationRegexConstants.ID));
            AddRuleFor(x => x.MerchantTransactionID)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.MerchantTransactionID) && Regex.IsMatch(x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<RefundRequest>(x => x.MerchantTransactionID, ValidationRegexConstants.MerchantTransactionID));
            AddRuleFor(x => x.OriginatorTransactionID)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.OriginatorTransactionID) || Regex.IsMatch(x.OriginatorTransactionID, ValidationRegexConstants.OriginatorTransactionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<RefundRequest>(x => x.OriginatorTransactionID, ValidationRegexConstants.OriginatorTransactionID));
            AddRuleFor(x => x.Amount)
                .WithPredicate(x => x.Amount > 0 && Regex.IsMatch(x.Amount.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.Amount))
                .WithErrorMessage(Operator.InvalidPropertyMessage<RefundRequest>(x => x.Amount));
            AddRuleFor(x => x.Description)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Description) || Regex.IsMatch(x.Description, ValidationRegexConstants.Description))
                .WithErrorMessage(Operator.InvalidPropertyMessage<RefundRequest>(x => x.Description, ValidationRegexConstants.Description));

            AddInnerValidatorFor(x => x.Customer, () => InnerValidator.Create(customerValidator, true));

            AddInnerValidatorFor(x => x.BillingAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.BankAddress, () => InnerValidator.Create(addressValidator, true));

            AddInnerValidatorFor(x => x.Articles, () => InnerValidator.Create(new EnumerableValidator<Article>(articleValidator), true));

            AddRuleFor(x => x.TokenLifetime.HasValue)
                .WithPredicate(x => x.TokenLifetime == null || x.TokenLifetime.Value < 0)
                .WithErrorMessage(Operator.InvalidPropertyMessage<RefundRequest>(x => x.TokenLifetime));

        }
    }
}
