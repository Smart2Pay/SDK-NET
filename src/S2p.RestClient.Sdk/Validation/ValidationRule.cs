using System;

namespace S2p.RestClient.Sdk.Validation
{
    public class ValidationRule<T>
    {
        public string ErrorMessage { get; private set; }
        public Predicate<T> Predicate { get; private set; }
        public string PropertyName { get; }

        public ValidationRule(string propertyName)
        {
            propertyName.ThrowIfNullOrWhiteSpace("Cannot pass a null or empty property name to a validation rule");

            PropertyName = propertyName;
            Predicate = t => true;
            ErrorMessage = string.Empty;
        }

        public ValidationRule<T> WithPredicate(Predicate<T> rule)
        {
            rule.ThrowIfNull("Cannot pass a null predicate to a validation rule");

            Predicate = rule;
            return this;
        }

        public ValidationRule<T> WithErrorMessage(string errorMessage)
        {
            errorMessage.ThrowIfNullOrWhiteSpace("Cannot pass a null or empty error message to a validation rule");

            ErrorMessage = errorMessage;
            return this;
        }
    }
}
