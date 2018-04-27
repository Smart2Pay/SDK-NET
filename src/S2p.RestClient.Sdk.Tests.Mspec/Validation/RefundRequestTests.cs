using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Validation;
using System;
using S2p.RestClient.Sdk.Entities.Validators;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class RefundRequestTests
    {
        private static RefundRequest refundRequest { get; set; }
        private static RefundRequestValidator validator { get; set; }
        private static ValidationResult validationResult { get; set; }

        [Subject(typeof(RefundRequestValidator))]
        public class When_refund_request_is_correct
        {
            private Establish context = () => {
                refundRequest = new RefundRequest()
                {
                    Customer = new Customer() { FirstName = "First", Email = "test@test.com" },
                    MerchantTransactionID = "111111111",
                    Amount = 4,
                };
                validator = new RefundRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(refundRequest);
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

        [Subject(typeof(RefundRequestValidator))]
        public class When_refund_request_has_negative_amount
        {
            private Establish context = () => {
                refundRequest = new RefundRequest()
                {
                    Customer = new Customer() { FirstName = "First", Email = "test@test.com" },
                    MerchantTransactionID = "111111111",
                    Amount = -22,
                };
                validator = new RefundRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(refundRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual("RefundRequest-Amount:Invalid Amount;");
            };
        }

        [Subject(typeof(RefundRequestValidator))]
        public class When_refund_request_has_invalid_bank_address
        {
            private Establish context = () => {
                refundRequest = new RefundRequest()
                {
                    BankAddress = new Address() { Country = "ROU"},
                    MerchantTransactionID = "111111111",
                    Amount = 4,
                };
                validator = new RefundRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(refundRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual("Address-Country:Invalid Country;");
            };
        }
    }
}
