using System;
using S2p.RestClient.Sdk.Infrastructure;

namespace S2p.RestClient.Sdk.Validation
{
    public class ValidationException : Exception
    {
        public ValidationResult ValidationResult { get; private set; }

        public ValidationException(ValidationResult validationResult) 
            : base(validationResult.Message)
        {
            ValidationResult = validationResult;
        }
    }

    public static class ValidationExceptionExtensions
    {
        public static ValidationException ToValidationException(this ValidationResult @this)
        {
            return new ValidationException(@this);
        }

        public static ApiResult<T> ToApiResult<T>(this ValidationException @this)
        {
            return ApiResult.Failure<T>(@this);
        }
    }
}
