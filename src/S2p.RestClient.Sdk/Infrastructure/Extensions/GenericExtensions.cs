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
            @this.ThrowIfNull("Cannot apply on a null instance");
            methodToApply.ThrowIfNull("Cannot apply a null method");
            condition.ThrowIfNull("Cannot apply with a null consdition");

            if (condition(@this))
            {
                methodToApply(@this);
            }

            return @this;
        }

        public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> mapper)
        {
            @this.ThrowIfNull("Cannot project null values");
            mapper.ThrowIfNull("Mapper function must be specified");

            return mapper(@this);
        }

        public static TOut MapIf<TIn, TOut>(this TIn @this, Func<TIn, TOut> mapper,
            Predicate<TIn> condition, Func<TIn, TOut> defaultValueFactory)
        {
            @this.ThrowIfNull("Cannot project null values");
            mapper.ThrowIfNull("Mapper function must be specified");
            condition.ThrowIfNull("Cannot map with a null consdition");
            defaultValueFactory.ThrowIfNull("Default value should be provided if condition is false");

            return condition(@this) 
                ? mapper(@this)
                : defaultValueFactory(@this);
        }
    }
}
