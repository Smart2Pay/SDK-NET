using System;
using System.Collections.Generic;
using System.Text;
using S2p.RestClient.Sdk.Infrastructure.Extensions;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentRequestValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentRequestValidator()
        {
            AddRuleFor(x => x.BillingAddress.Country)
                .WithPredicate(x => Helper.CurrencyExists(x.BillingAddress.Country))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.BillingAddress.Country));
            AddRuleFor(x => x.Currency)
                .WithPredicate(x => Helper.CurrencyExists(x.Currency))
                .WithErrorMessage(Operator.InvalidPropertyMessage<PaymentRequest>(x => x.Currency));

        }
    }
}
