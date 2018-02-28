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
                    $"{nameof(dummyClass.Id)}:{DummyClassValidator.IdValidationText};");
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
                    $"{nameof(dummyClass.Id)}:{DummyClassValidator.IdValidationText};{nameof(dummyClass.Quantity)}:{DummyClassValidator.QuantityValidationText};");
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
    }
}
