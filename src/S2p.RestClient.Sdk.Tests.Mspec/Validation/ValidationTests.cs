using Machine.Specifications;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class ValidationTests
    {
        private static DummyClassValidator validator = new DummyClassValidator();
        private static DummyClass dummyClass;
        private static ValidationResult validationResult;

        public class When_id_is_null
        {
            private Establish context = () => { dummyClass = new DummyClass {Id = null, Quantity = null}; };

            private Because of = () => { validationResult = validator.Validate(dummyClass); };

            private It should_fail_validation = () => { validationResult.IsValid.ShouldBeFalse(); };

            private It should_have_one_error = () => { validationResult.NumberOfErrors.ShouldEqual(1); };

            private It should_have_the_correct_error_message = () =>
            {
                validationResult.Message.ShouldEqual(string.Format("{0}:{1};",
                    "Id", DummyClassValidator.IdValidationText));
            };
        }

        public class When_id_is_null_and_quantity_is_negative
        {
            private Establish context = () => { dummyClass = new DummyClass { Id = null, Quantity = -1 }; };

            private Because of = () => { validationResult = validator.Validate(dummyClass); };

            private It should_fail_validation = () => { validationResult.IsValid.ShouldBeFalse(); };

            private It should_have_two_errors = () => { validationResult.NumberOfErrors.ShouldEqual(2); };

            private It should_have_the_correct_error_message = () =>
            {
                validationResult.Message.ShouldEqual(string.Format("{0}:{1};{2}:{3};",
                    "Id", DummyClassValidator.IdValidationText, "Quantity", DummyClassValidator.QuantityValidationText));
            };
        }

        public class When_id_is_positive_and_quantity_is_positive
        {
            private Establish context = () => { dummyClass = new DummyClass { Id = 1, Quantity = 1 }; };

            private Because of = () => { validationResult = validator.Validate(dummyClass); };

            private It should_pass_validation = () => { validationResult.IsValid.ShouldBeTrue(); };

            private It should_have_zero_errors = () => { validationResult.NumberOfErrors.ShouldEqual(0); };

            private It should_have_the_empty_error_message = () =>
            {
                validationResult.Message.ShouldEqual(string.Empty);
            };
        }
    }
}
