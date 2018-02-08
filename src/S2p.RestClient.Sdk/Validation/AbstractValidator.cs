using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace S2p.RestClient.Sdk.Validation
{
    public class AbstractValidator<T> : IValidator<T>
    {
        private readonly IList<ValidationRule<T>> _rules = new List<ValidationRule<T>>();

        protected internal ValidationRule<T> AddRuleFor(Expression<Func<T, object>> memberExpression)
        {
            var rule = new ValidationRule<T>(Operator.Nameof<T>(memberExpression));
            _rules.Add(rule);
            return rule;
        }

        public ValidationResult Validate(T obj)
        {
            var validationResult = _rules
                .Aggregate(new { IsValid = new List<bool>(), Message = new StringBuilder() }, (result, rule) =>
                {
                    var ruleResult = rule.Predicate(obj);
                    result.IsValid.Add(ruleResult);
                    if (!ruleResult)
                    {
                        result.Message.Append(string.Format("{0}:{1};", rule.PropertyName, rule.ErrorMessage));
                    }

                    return result;
                })
                .Transform(ir => new ValidationResult
                {
                    IsValid = ir.IsValid.Count == 0 ||
                              ir.IsValid.All(isValid => isValid),
                    Message = ir.Message.ToString(),
                    NumberOfErrors = ir.IsValid.Count(isValid => !isValid)
                });

            return validationResult;
        }
    }
}
