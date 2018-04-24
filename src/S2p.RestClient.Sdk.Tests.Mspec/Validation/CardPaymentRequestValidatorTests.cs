using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Validation;
using System;
using S2p.RestClient.Sdk.Entities.Validators;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class CardPaymentRequestValidatorTests
    {
        private static CardPaymentRequest paymentRequest { get; set; }
        private static CardPaymentRequestValidator validator { get; set; }
        private static ValidationResult validationResult { get; set; }

        [Subject(typeof(CardPaymentRequestValidator))]
        public class When_payment_request_is_correct
        {
            private Establish context = () => {
                paymentRequest = new CardPaymentRequest()
                {
                    MerchantTransactionID = "42526224",
                    Amount = 4,
                    Currency = "RON",
                    Card = new CardDetailsRequest() { HolderName = "Holder Name", Number = "4444444444444444", ExpirationMonth = "01", ExpirationYear = "01"},
                    CreditCardToken = new CreditCardTokenDetailsRequest() { Value = "aaaaaaaaaa"}
                };
                validator = new CardPaymentRequestValidator();
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

        [Subject(typeof(CardPaymentRequestValidator))]
        public class When_payment_request_with_incorrect_holder_name
        {
            private Establish context = () => {
                paymentRequest = new CardPaymentRequest()
                {
                    MerchantTransactionID = "42526224",
                    Amount = 4,
                    Currency = "RON",
                    Card = new CardDetailsRequest() { HolderName = "2Holder Name", Number = "4444444444444444", ExpirationMonth = "01", ExpirationYear = "01" },
                    CreditCardToken = new CreditCardTokenDetailsRequest() { Value = "aaaaaaaaaa" }
                };
                validator = new CardPaymentRequestValidator();
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
                validationResult.Message.ShouldEqual("CardDetailsRequest-HolderName:Invalid HolderName, Regex: ^([A-Z a-z]{1,50})$;");
            };
        }
    }
}
