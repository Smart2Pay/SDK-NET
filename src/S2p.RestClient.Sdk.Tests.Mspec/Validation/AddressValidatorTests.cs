using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class AddressValidatorTests
    {
        private static Address address { get; set; }
        private static AddressValidator validator { get; set; }
        private static ValidationResult validationResult { get; set; }

        [Subject(typeof(AddressValidator))]
        public class When_address_is_correct
        {
            private Establish context = () => {
                address = new Address() {ID = 1, Country = "RO"};
                validator = new AddressValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(address);
            };

            private It should_pass_validation = () => {
                validationResult.IsValid.ShouldBeTrue();
            };

            private It should_have_zero_errors = () => {
                validationResult.ErrorsCount.ShouldEqual(0);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual(String.Empty);
            };
        }

        [Subject(typeof(AddressValidator))]
        public class When_city_is_empty
        {
            private Establish context = () => {
                address = new Address {ID = 1, Country = "RO", City = string.Empty};
                validator = new AddressValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(address);
            };

            private It should_fail_validation = () => {
                validationResult.IsValid.ShouldBeTrue();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(0);
            };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(string.Empty);
            };
        }

        [Subject(typeof(AddressValidator))]
        public class When_city_is_incorrect
        {
            private Establish context = () => {
                address = new Address {ID = 1, Country = "RO", City = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"};
                validator = new AddressValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(address);
            };

            private It should_fail_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(Address).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(address.City)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{AddressValidator.CityValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}"
                    );
            };
        }

        [Subject(typeof(AddressValidator))]
        public class When_city_and_country_are_incorrect
        {

            private Establish context = () => {
                address = new Address {ID = 1, Country = "ROU", City = "asasasasasasasasasasasasasasasasasasaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"};
                validator = new AddressValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(address);
            };

            private It should_fail_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(2);
            };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(Address).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(address.City)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{AddressValidator.CityValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}" +
                    $"{nameof(address.Country)}{ValidationConstants.PropertyMessageSeparator}{AddressValidator.CountryValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}"
                );
            };

        }
    }
}
