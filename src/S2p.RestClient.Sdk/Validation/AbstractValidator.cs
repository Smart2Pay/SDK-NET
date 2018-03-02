using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Validation
{
    public abstract class AbstractValidator<T> : IValidator<T>
    {
        private readonly IList<ValidationRule<T>> _rules = new List<ValidationRule<T>>();
        private readonly IDictionary<string, Func<InnerValidator>> _innerValidators = new Dictionary<string, Func<InnerValidator>>();

        protected ValidationRule<T> AddRuleFor(Expression<Func<T, object>> memberExpression)
        {
            memberExpression.ThrowIfNull(nameof(memberExpression));

            var rule = new ValidationRule<T>(Operator.Nameof<T>(memberExpression));
            _rules.Add(rule);
            return rule;
        }

        protected void AddInnerValidatorFor(Expression<Func<T, object>> memberExpression, Func<InnerValidator> validatorFactory)
        {
            memberExpression.ThrowIfNull(nameof(memberExpression));
            validatorFactory.ThrowIfNull(nameof(validatorFactory));

            _innerValidators.Add(Operator.Nameof<T>(memberExpression), validatorFactory);
        }

        ValidationResult IValidator.Validate(object objectToValidate)
        {
            return Validate((T)objectToValidate);
        }

        public ValidationResult Validate(T objectToValidate)
        {
            objectToValidate.ThrowIfNull(nameof(objectToValidate));

            var resultList = new List<ValidationResult> {RunRulesValidator(objectToValidate)};
            resultList.AddRange(RunInnerValidators(objectToValidate));

            var validationResult = resultList.Aggregate(new
                {
                    ValidationResult = new ValidationResult(),
                    MessageBuilder = new StringBuilder()
                }, (result, current) => {
                    result.ValidationResult.IsValid = result.ValidationResult.IsValid && current.IsValid;
                    result.ValidationResult.ErrorsCount += current.ErrorsCount;
                    result.MessageBuilder.Append(current.Message);
                    return result;
                })
                .Map(result => {
                    result.ValidationResult.Message = result.ValidationResult.ErrorsCount == 0
                        ? string.Empty
                        : result.MessageBuilder.ToString();
                    return result.ValidationResult;
                });

            return validationResult;
        }

        private IEnumerable<ValidationResult> RunInnerValidators(T objectToValidate)
        {
            var innerValidatorsResult = _innerValidators.Aggregate(new List<ValidationResult>(), (result, kvp) => {
                var propertyName = kvp.Key;
                var innerValidator = kvp.Value()
                    .ValueIfNull(() => InnerValidator.Create(PositiveValidator.Instance, true));

                var propertyValue = objectToValidate.GetType().GetRuntimeProperty(propertyName)?.GetValue(objectToValidate);
                result.ApplyIf(r => r.Add(new ValidationResult()), r => propertyValue == null && innerValidator.AllowNull);
                result.ApplyIf(r => r.Add(new ValidationResult {
                        ErrorsCount = 1,
                        IsValid = false,
                        Message =
                            $"{propertyName}{ValidationConstants.PropertyMessageSeparator}" +
                            $"{ValidationConstants.IsNullMessage}{ValidationConstants.ErrorMessageSeparator}"
                    }),
                    r => propertyValue == null && !innerValidator.AllowNull);
                result.ApplyIf(r => r.Add(innerValidator.Validator.Validate(propertyValue)), r => propertyValue != null);
                return result;
            });
            return innerValidatorsResult;
        }

        private ValidationResult RunRulesValidator(T objectToValidate)
        {
            var validationResult = _rules
                .Aggregate(new {IsValidList = new List<bool>(), Message = new StringBuilder()}, (result, rule) => {
                    var ruleResult = rule.Predicate(objectToValidate);
                    result.IsValidList.Add(ruleResult);
                    if (!ruleResult)
                    {
                        result.Message.Append(string.Concat(rule.PropertyName, ValidationConstants.PropertyMessageSeparator,
                            rule.ErrorMessage, ValidationConstants.ErrorMessageSeparator));
                    }

                    return result;
                })
                .Map(ir => {
                    var errorsCount = ir.IsValidList.Count(isValid => !isValid);
                    var message = errorsCount == 0
                        ? string.Empty
                        : string.Concat(typeof(T).Name, ValidationConstants.ObjectPropertySeparator,
                            ir.Message.ToString());
                    return new ValidationResult
                    {
                        IsValid = ir.IsValidList.Count == 0 || ir.IsValidList.All(isValid => isValid),
                        ErrorsCount = errorsCount,
                        Message = message
                    };
                });
            return validationResult;
        }
    }
}
