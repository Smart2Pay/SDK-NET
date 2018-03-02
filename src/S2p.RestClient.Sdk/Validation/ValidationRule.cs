using System;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Validation
{
    public class ValidationRule<T>
    {
        public string ErrorMessage { get; private set; }
        public Predicate<T> Predicate { get; private set; }
        public string PropertyName { get; }

        public ValidationRule(string propertyName)
        {
            propertyName.ThrowIfNullOrWhiteSpace(nameof(propertyName));

            PropertyName = propertyName;
            Predicate = t => true;
            ErrorMessage = string.Empty;
        }

        public ValidationRule<T> WithPredicate(Predicate<T> rule)
        {
            rule.ThrowIfNull(nameof(rule));

            Predicate = rule;
            return this;
        }

        public ValidationRule<T> WithErrorMessage(string errorMessage)
        {
            errorMessage.ThrowIfNullOrWhiteSpace(nameof(errorMessage));

            ErrorMessage = errorMessage;
            return this;
        }
    }
}
