using System;
using System.Collections.Generic;
using System.Text;
using S2p.RestClient.Sdk.Validation;

namespace S2p.RestClient.Sdk.Entities.Validators
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        internal static readonly string IdValidationText = "Must be positive, non zero, non null";
        internal static readonly string QuantityValidationText = "Must be greater or equal to zero";

        public ArticleValidator()
        {
            AddRuleFor(x => x.MerchantArticleID).WithErrorMessage(IdValidationText)
                .WithPredicate(x => !string.IsNullOrEmpty(x.MerchantArticleID));
            AddRuleFor(x => x.Quantity).WithErrorMessage(QuantityValidationText)
                .WithPredicate(x => x.Quantity >= 0);
        }
    }
}
