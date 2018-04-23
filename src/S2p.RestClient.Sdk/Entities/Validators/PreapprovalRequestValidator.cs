using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;
using System.Globalization;
using System.Text.RegularExpressions;
using S2p.RestClient.Sdk.Infrastructure.Helpers;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class PreapprovalRequestValidator : AbstractValidator<PreapprovalRequest>
    {
        public PreapprovalRequestValidator()
        {
            var addressValidator = new AddressValidator();
            var customerValidator = new CustomerValidator();

            AddRuleFor(x => x.MethodID)
                .WithPredicate(x => x.MethodID > 0 && Regex.IsMatch(x.MethodID.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.MethodID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.MethodID, ValidationRegexConstants.MethodID));
            AddRuleFor(x => x.MerchantPreapprovalID)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.MerchantPreapprovalID) && Regex.IsMatch(x.MerchantPreapprovalID, ValidationRegexConstants.MerchantPreapprovalID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.MerchantPreapprovalID, ValidationRegexConstants.MerchantPreapprovalID));
            AddRuleFor(x => x.RecurringPeriod)
                .WithPredicate(x => x.RecurringPeriod > 0 || Regex.IsMatch(x.RecurringPeriod.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.RecurringPeriod))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.RecurringPeriod, ValidationRegexConstants.RecurringPeriod));
            AddRuleFor(x => x.Description)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Description) || Regex.IsMatch(x.Description, ValidationRegexConstants.PreapprovalDescription))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.Description, ValidationRegexConstants.PreapprovalDescription));
            AddRuleFor(x => x.PreapprovedMaximumAmount)
                .WithPredicate(x => !x.PreapprovedMaximumAmount.HasValue || Regex.IsMatch(x.PreapprovedMaximumAmount.Value.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.Amount))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.PreapprovedMaximumAmount, ValidationRegexConstants.Amount));
            AddRuleFor(x => x.Currency)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Currency) || CurrencyValidationHelper.CurrencyExists(x.Currency))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.Currency));
            AddRuleFor(x => x.ReturnURL)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.ReturnURL) || Regex.IsMatch(x.ReturnURL, ValidationRegexConstants.PreapprovalReturnURL))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.ReturnURL, ValidationRegexConstants.PreapprovalReturnURL));
            AddRuleFor(x => x.MethodOptionID)
                .WithPredicate(x => x.MethodOptionID == 0 || Regex.IsMatch(x.MethodOptionID.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.MethodOptionID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PreapprovalRequest>(x => x.MethodOptionID, ValidationRegexConstants.MethodOptionID));

            AddInnerValidatorFor(x => x.Customer, () => InnerValidator.Create(customerValidator, true));

            AddInnerValidatorFor(x => x.BillingAddress, () => InnerValidator.Create(addressValidator, true));
        }
    }
}
