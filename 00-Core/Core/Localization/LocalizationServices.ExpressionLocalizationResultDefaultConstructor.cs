using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.Localization
{
    public static partial class LocalizationServices
    {
        public static ITranslateResult FormatExpression<TLocalization, TDefaultLocalization>(
            Expression<Func<TLocalization, string>> propertyExpression)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>(
                propertyExpression);

        public static ITranslateResult FormatLazy<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>(
                propertyFunc);

        public static ITranslateResult FormatExpression<TLocalization, TDefaultLocalization>(
            Expression<Func<TLocalization, string>> propertyExpression, params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>(
                propertyExpression,
                args);

        public static ITranslateResult FormatLazy<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc, params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>(
                propertyFunc,
                args);

        public static ITranslateResult FormatExpression<TLocalization, TDefaultLocalization>(
            Expression<Func<TLocalization, string>> propertyExpression, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>(
                propertyExpression,
                args,
                formatProvider);

        public static ITranslateResult FormatLazy<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFun, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>(
                propertyFun,
                args,
                formatProvider);
    }
}