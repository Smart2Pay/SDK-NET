using Machine.Specifications;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class ValidationTests
    {
        private static DummyClassValidator validator = new DummyClassValidator();
        private static DummyClass dummyClass;
        private static ValidationResult validationResult;

        [Subject("Validation")]
        public class When_id_is_null
        {
            private Establish context = () => { dummyClass = new DummyClass {Id = null, Quantity = null}; };

            private Because of = () => { validationResult = validator.Validate(dummyClass); };

            private It should_fail_validation = () => { validationResult.IsValid.ShouldBeFalse(); };

            private It should_have_one_error = () => { validationResult.ErrorsCount.ShouldEqual(1); };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(DummyClass).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(dummyClass.Id)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}"
                );
            };
        }

        [Subject("Validation")]
        public class When_id_is_null_and_quantity_is_negative
        {
            private Establish context = () => { dummyClass = new DummyClass {Id = null, Quantity = -1}; };

            private Because of = () => { validationResult = validator.Validate(dummyClass); };

            private It should_fail_validation = () => { validationResult.IsValid.ShouldBeFalse(); };

            private It should_have_two_errors = () => { validationResult.ErrorsCount.ShouldEqual(2); };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(DummyClass).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(dummyClass.Id)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}{ValidationConstants.ErrorMessageSeparator}" +
                    $"{nameof(dummyClass.Quantity)}{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.QuantityValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}");
            };
        }

        [Subject("Validation")]
        public class When_id_is_positive_and_quantity_is_positive
        {
            private Establish context = () => { dummyClass = new DummyClass {Id = 1, Quantity = 1}; };

            private Because of = () => { validationResult = validator.Validate(dummyClass); };

            private It should_pass_validation = () => { validationResult.IsValid.ShouldBeTrue(); };

            private It should_have_zero_errors = () => { validationResult.ErrorsCount.ShouldEqual(0); };

            private It should_have_the_empty_error_message = () => {
                validationResult.Message.ShouldEqual(string.Empty);
            };
        }

        [Subject("Validation")]
        public class When_wrapper_class_allows_null
        {
            private static DummyWrapperClass dummyWrapperClass;
            private static DummyWrapperClassValidatorAllowNull dummyWrapperClassValidator;

            private Establish context = () => {
                dummyWrapperClass = new DummyWrapperClass();
                dummyWrapperClassValidator = new DummyWrapperClassValidatorAllowNull();
            };

            private Because of = () => { validationResult = dummyWrapperClassValidator.Validate(dummyWrapperClass); };

            private It should_fail_validatiob = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => { validationResult.ErrorsCount.ShouldEqual(1); };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(DummyWrapperClass).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(dummyClass.Id)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}"
                    );
            };
        }

        [Subject("Validation")]
        public class When_wrapper_class_does_not_allow_null
        {
            private static DummyWrapperClass dummyWrapperClass;
            private static DummyWrapperClassValidatorNotNull dummyWrapperClassValidator;

            private Establish context = () => {
                dummyWrapperClass = new DummyWrapperClass();
                dummyWrapperClassValidator = new DummyWrapperClassValidatorNotNull();
            };

            private Because of = () => { validationResult = dummyWrapperClassValidator.Validate(dummyWrapperClass); };

            private It should_fail_validatiob = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_two_errors = () => { validationResult.ErrorsCount.ShouldEqual(2); };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(DummyWrapperClass).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(dummyClass.Id)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}" + 
                    $"{nameof(dummyWrapperClass.DummyClass)}{ValidationConstants.PropertyMessageSeparator}" +
                    $"{ValidationConstants.IsNullMessage}{ValidationConstants.ErrorMessageSeparator}"
                );
            };
        }

        [Subject("Validation")]
        public class When_wrapper_class_does_gets_error_from_inner_property
        {
            private static DummyWrapperClass dummyWrapperClass;
            private static DummyWrapperClassValidatorNotNull dummyWrapperClassValidator;

            private Establish context = () => {
                dummyWrapperClass = new DummyWrapperClass { DummyClass = new DummyClass { Id = null, Quantity = -1 } };
                dummyWrapperClassValidator = new DummyWrapperClassValidatorNotNull();
            };

            private Because of = () => { validationResult = dummyWrapperClassValidator.Validate(dummyWrapperClass); };

            private It should_fail_validatiob = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_three_errors = () => { validationResult.ErrorsCount.ShouldEqual(3); };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{typeof(DummyWrapperClass).Name}{ValidationConstants.ObjectPropertySeparator}{nameof(dummyClass.Id)}" +
                    $"{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}{nameof(dummyWrapperClass.DummyClass)}{ValidationConstants.ObjectPropertySeparator}" +
                    $"{nameof(dummyClass.Id)}{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}{nameof(dummyClass.Quantity)}{ValidationConstants.PropertyMessageSeparator}" +
                    $"{DummyClassValidator.QuantityValidationText}{ValidationConstants.ErrorMessageSeparator}"
                );
            };
        }

        [Subject("Validation")]
        public class When_wrapper_class_does_gets_error_only_from_inner_property
        {
            private static DummyWrapperClass dummyWrapperClass;
            private static DummyWrapperClassValidatorNotNull dummyWrapperClassValidator;

            private Establish context = () => {
                dummyWrapperClass = new DummyWrapperClass {Id = 1, DummyClass = new DummyClass { Id = null, Quantity = -1 } };
                dummyWrapperClassValidator = new DummyWrapperClassValidatorNotNull();
            };

            private Because of = () => { validationResult = dummyWrapperClassValidator.Validate(dummyWrapperClass); };

            private It should_fail_validatiob = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_two_errors = () => { validationResult.ErrorsCount.ShouldEqual(2); };

            private It should_have_the_correct_error_message = () => {
                validationResult.Message.ShouldEqual(
                    $"{nameof(dummyWrapperClass.DummyClass)}{ValidationConstants.ObjectPropertySeparator}" +
                    $"{nameof(dummyClass.Id)}{ValidationConstants.PropertyMessageSeparator}{DummyClassValidator.IdValidationText}" +
                    $"{ValidationConstants.ErrorMessageSeparator}{nameof(dummyClass.Quantity)}{ValidationConstants.PropertyMessageSeparator}" +
                    $"{DummyClassValidator.QuantityValidationText}{ValidationConstants.ErrorMessageSeparator}"
                );
            };
        }
    }
}
