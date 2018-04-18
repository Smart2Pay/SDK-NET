using System;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Entities.Validators;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class PreapprovalRequestValidatorTests
    {
        private static PreapprovalRequest preapprovalRequest { get; set; }
        private static PreapprovalRequestValidator validator { get; set; }
        private static ValidationResult validationResult { get; set; }

        [Subject(typeof(PreapprovalRequestValidator))]
        public class When_preapproval_request_is_correct
        {
            private Establish context = () => {
                preapprovalRequest = new PreapprovalRequest()
                {
                    MethodID = 1,
                    MerchantPreapprovalID = "444444"
                };
                validator = new PreapprovalRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(preapprovalRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeTrue();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(0);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual(String.Empty);
            };
        }

        [Subject(typeof(PreapprovalRequestValidator))]
        public class When_preapproval_request_has_negative_recurring_period
        {
            private Establish context = () => {
                preapprovalRequest = new PreapprovalRequest()
                {
                    MethodID = 1,
                    MerchantPreapprovalID = "444444",
                    RecurringPeriod = -1
                };
                validator = new PreapprovalRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(preapprovalRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual(@"PreapprovalRequest-RecurringPeriod:Invalid RecurringPeriod, Regex: ^\d{1,5}$;");
            };
        }
        
        [Subject(typeof(PreapprovalRequestValidator))]
        public class When_preapproval_request_has_incorrect_merchant_preapproval_id
        {
            private Establish context = () => {
                preapprovalRequest = new PreapprovalRequest()
                {
                    MethodID = 1,
                    MerchantPreapprovalID = "444444.",
                    RecurringPeriod = 1
                };
                validator = new PreapprovalRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(preapprovalRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual(@"PreapprovalRequest-MerchantPreapprovalID:Invalid MerchantPreapprovalID, Regex: ^[0-9a-zA-Z_-]{1,50}$;");
            };
        }
    }
}