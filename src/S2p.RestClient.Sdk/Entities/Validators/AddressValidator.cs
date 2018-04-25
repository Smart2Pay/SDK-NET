using System.Globalization;
using System.Text.RegularExpressions;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Infrastructure.Helper;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            AddRuleFor(x => x.ID)
                .WithPredicate(x => Regex.IsMatch(x.ID.ToString(CultureInfo.InvariantCulture), ValidationRegexConstants.ID))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.ID, ValidationRegexConstants.ID));
            AddRuleFor(x => x.City)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.City) || Regex.IsMatch(x.City, ValidationRegexConstants.City))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.City, ValidationRegexConstants.City));
            AddRuleFor(x => x.ZipCode)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.ZipCode) || Regex.IsMatch(x.ZipCode, ValidationRegexConstants.ZipCodeV3))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.ZipCode, ValidationRegexConstants.ZipCodeV3));
            AddRuleFor(x => x.State)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.State) || Regex.IsMatch(x.State, ValidationRegexConstants.State))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.State, ValidationRegexConstants.State));
            AddRuleFor(x => x.Street)
                .WithPredicate(x => string.IsNullOrWhiteSpace(x.Street) || Regex.IsMatch(x.Street, ValidationRegexConstants.Street))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.Street, ValidationRegexConstants.Street));
            AddRuleFor(x => x.StreetNumber)
                .WithPredicate(x =>
                    string.IsNullOrWhiteSpace(x.StreetNumber) || Regex.IsMatch(x.StreetNumber, ValidationRegexConstants.StreetNumber))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.StreetNumber, ValidationRegexConstants.StreetNumber));
            AddRuleFor(x => x.HouseNumber)
                .WithPredicate(x =>
                    string.IsNullOrWhiteSpace(x.HouseNumber) || Regex.IsMatch(x.HouseNumber, ValidationRegexConstants.HouseNumber))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.HouseNumber, ValidationRegexConstants.HouseNumber));
            AddRuleFor(x => x.Country)
                .WithPredicate(x => !string.IsNullOrWhiteSpace(x.Country) && Country.Exists(x.Country))
                .WithErrorMessage(Operator.InvalidPropertyMessage<Address>(x => x.Country));
        }
    }
}
