using System.Collections.Generic;
using System.Linq;
using System.Text;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Validation
{
    public class EnumerableValidator<T> : IValidator<IEnumerable<T>>
    {
        private readonly IValidator<T> _itemValidator;

        public EnumerableValidator(IValidator<T> itemValidator)
        {
            itemValidator.ThrowIfNull(nameof(itemValidator));

            _itemValidator = itemValidator;
        }

        public ValidationResult Validate(IEnumerable<T> collectionToValidate)
        {
            return collectionToValidate
                .Select((item, index) => new { Item = item, Index = index })
                .Aggregate(new { ValidationResult = new ValidationResult(), MessageBuilder = new StringBuilder() },
                    (result, current) => {
                        var currentResult = _itemValidator.Validate(current.Item);

                        result.ValidationResult.IsValid = result.ValidationResult.IsValid && currentResult.IsValid;
                        result.ValidationResult.ErrorsCount += currentResult.ErrorsCount;
                        currentResult.ApplyIf(r => result.MessageBuilder.Append(
                                string.Concat(ValidationConstants.ItemMessage, current.Index, 
                                    ValidationConstants.ObjectPropertySeparator, currentResult.Message)),
                            r => !r.IsValid);

                        return result;
                    })
                .Map(result => {
                    var validationResult = new ValidationResult
                    {
                        IsValid = result.ValidationResult.IsValid,
                        ErrorsCount = result.ValidationResult.ErrorsCount,
                        Message = result.ValidationResult.ErrorsCount == 0
                            ? string.Empty
                            : result.MessageBuilder.ToString()
                    };

                    return validationResult;
                });
        }

        ValidationResult IValidator.Validate(object collectionToValidate)
        {
            return Validate((IEnumerable<T>)collectionToValidate);
        }
    }
}
