using System;
using Machine.Specifications;
using S2p.RestClient.Sdk.Entities;
using S2p.RestClient.Sdk.Entities.Validators;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Tests.Mspec.Validation
{
    public class CardPayoutRequestValidatorTests
    {
        private static CardPayoutRequest payoutRequest { get; set; }
        private static CardPayoutRequestValidator validator { get; set; }
        private static ValidationResult validationResult { get; set; }

        [Subject(typeof(CardPayoutRequestValidator))]
        public class When_payout_request_is_correct
        {
            private Establish context = () => {
                payoutRequest = new CardPayoutRequest()
                {
                    MerchantTransactionID = "42526224",
                    Amount = 4,
                    Currency = "RON",
                    Card = new CardDetailsRequest() { HolderName = "Holder Name", Number = "4444444444444444", ExpirationMonth = "01", ExpirationYear = "01" },
                };
                validator = new CardPayoutRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(payoutRequest);
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

        [Subject(typeof(CardPayoutRequestValidator))]
        public class When_payout_request_has_missing_MTID
        {
            private Establish context = () => {
                payoutRequest = new CardPayoutRequest()
                {
                    MerchantTransactionID = null,
                    Amount = 4,
                    Currency = "RON",
                };
                validator = new CardPayoutRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(payoutRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual("CardPayoutRequest-MerchantTransactionID:Invalid MerchantTransactionID, Regex: ^[0-9a-zA-Z_-]{1,50}$;");
            };
        }

        [Subject(typeof(CardPayoutRequestValidator))]
        public class When_payout_request_has_invalid_card_number
        {
            private Establish context = () => {
                payoutRequest = new CardPayoutRequest()
                {
                    MerchantTransactionID = "42526224",
                    Amount = 4,
                    Currency = "RON",
                    Card = new CardDetailsRequest() { HolderName = "Holder Name", Number = "444444444444444400000", ExpirationMonth = "01", ExpirationYear = "01" },
                };
                validator = new CardPayoutRequestValidator();
            };

            private Because of = () => {
                validationResult = validator.Validate(payoutRequest);
            };

            private It should_not_pass_validation = () => {
                validationResult.IsValid.ShouldBeFalse();
            };

            private It should_have_one_error = () => {
                validationResult.ErrorsCount.ShouldEqual(1);
            };

            private It should_have_empty_error_message = () => {
                validationResult.Message.ShouldEqual(@"CardDetailsRequest-Number:Invalid Number, Regex: ^\d{1,20}$;");
            };
        }
    }
}