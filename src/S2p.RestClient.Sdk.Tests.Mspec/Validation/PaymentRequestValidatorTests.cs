using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Validation;
using System;
using S2p.RestClient.Sdk.Entities.Validators;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class PaymentRequestValidatorTests
    {
        private static AlternativePaymentRequest paymentRequest { get; set; }
        private static AlternativePaymentRequestValidator validator { get; set; }
        private static ValidationResult validationResult { get; set; }

        [Subject(typeof(AlternativePaymentRequestValidator))]
        public class When_payment_request_is_correct
        {
            private Establish context = () => {
                paymentRequest = new AlternativePaymentRequest()
                {
                    MerchantTransactionID = "111111111",
                    Amount = 4,
                    Currency = "RON",
                    ReturnURL = "http://smart2pay.com/ReturnURL",
                    MethodID = 1
                };
                validator = new AlternativePaymentRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(paymentRequest);
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

        [Subject(typeof(AlternativePaymentRequestValidator))]
        public class When_currency_is_missing
        {
            private Establish context = () => {
                paymentRequest = new AlternativePaymentRequest()
                {
                    MerchantTransactionID = "111111111",
                    Amount = 4,
                    ReturnURL = "http://smart2pay.com/ReturnURL",
                    MethodID = 1
                };
                validator = new AlternativePaymentRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(paymentRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual("AlternativePaymentRequest-Currency:Invalid Currency;");
            };
        }

        [Subject(typeof(AlternativePaymentRequestValidator))]
        public class When_MerchantTransactionID_is_empty
        {
            private Establish context = () => {
                paymentRequest = new AlternativePaymentRequest()
                {
                    ID = 1,
                    MerchantTransactionID = "",
                    Amount = 4,
                    Currency = "RON",
                    ReturnURL = "http://smart2pay.com/ReturnURL",
                    MethodID = 1
                };
                validator = new AlternativePaymentRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(paymentRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual("AlternativePaymentRequest-MerchantTransactionID:Invalid MerchantTransactionID, Regex: ^[0-9a-zA-Z_-]{1,50}$;");
            };
        }
        
    }
}
