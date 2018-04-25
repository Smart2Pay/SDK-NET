using System.Globalization;
using System.Text.RegularExpressions;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            AddRuleFor(x => x.ID)
                .WithPredicate(x => Regex.IsMatch(x.ID.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.CustomerID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.ID, ValidationRegexConstants.CustomerID));
            AddRuleFor(x => x.FirstName)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.FirstName) || Regex.IsMatch(x.FirstName, ValidationRegexConstants.CustomerName))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Customer>(x => x.FirstName, ValidationRegexConstants.CustomerName));
            AddRuleFor(x => x.LastName)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.LastName) || Regex.IsMatch(x.LastName, ValidationRegexConstants.CustomerName))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Customer>(x => x.LastName, ValidationRegexConstants.CustomerName));
            AddRuleFor(x => x.Email)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Email) || Regex.IsMatch(x.Email, ValidationRegexConstants.CustomerEmail))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Customer>(x => x.Email, ValidationRegexConstants.CustomerEmail));
            AddRuleFor(x => x.Phone)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Phone) || Regex.IsMatch(x.Phone, ValidationRegexConstants.CustomerPhone))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Customer>(x => x.Phone, ValidationRegexConstants.CustomerPhone));
        }
    }
}
