using System;

namespace S2p.RestClient.Sdk.Infrastructure.Extensions
{
    public static class GenericExtensions
    {
        public static T ValueIfNull<T>(this T @this, Func<T> valueProvider)
        {
            return @this != null
                ? @this
                : valueProvider();
        }

        public static T ApplyIf<T>(this T @this, Action<T> methodToApply, Predicate<T> condition)
        {
            @this.ThrowIfNull(typeof(T).Name.ToLower());
            methodToApply.ThrowIfNull(nameof(methodToApply));
            condition.ThrowIfNull(nameof(condition));

            if (condition(@this))
            {
                methodToApply(@this);
            }

            return @this;
        }

        public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> mapper)
        {
            @this.ThrowIfNull(typeof(TIn).Name.ToLower());
            mapper.ThrowIfNull(nameof(mapper));

            return mapper(@this);
        }

        public static TOut MapIf<TIn, TOut>(this TIn @this, Func<TIn, TOut> mapper,
            Predicate<TIn> condition, Func<TIn, TOut> defaultValueFactory)
        {
            @this.ThrowIfNull(typeof(TIn).Name.ToLower());
            mapper.ThrowIfNull(nameof(mapper));
            condition.ThrowIfNull(nameof(condition));
            defaultValueFactory.ThrowIfNull(nameof(defaultValueFactory));

            return condition(@this) 
                ? mapper(@this)
                : defaultValueFactory(@this);
        }
    }
}
