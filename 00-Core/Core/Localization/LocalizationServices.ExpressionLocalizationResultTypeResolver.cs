using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.Localization
{
    public static partial class LocalizationServices
    {
        public static ITranslateResult FormatExpression<TLocalization, TDefaultLocalization>(IDynamicTypeResolver typeResolver,
            Expression<Func<TLocalization, string>> propertyExpression)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>(
                typeResolver,
                propertyExpression);

        public static ITranslateResult Format<TLocalization, TDefaultLocalization>(IDynamicTypeResolver typeResolver,
            Func<TLocalization, string> propertyFunc)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>(
                typeResolver,
                propertyFunc);

        public static ITranslateResult FormatExpression<TLocalization, TDefaultLocalization>(IDynamicTypeResolver typeResolver,
            Expression<Func<TLocalization, string>> propertyExpression, params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>(
                typeResolver,
                propertyExpression,
                args);

        public static ITranslateResult Format<TLocalization, TDefaultLocalization>(IDynamicTypeResolver typeResolver,
            Func<TLocalization, string> propertyFunc, params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>(
                typeResolver,
                propertyFunc,
                args);

        public static ITranslateResult FormatExpression<TLocalization, TDefaultLocalization>(IDynamicTypeResolver typeResolver,
            Expression<Func<TLocalization, string>> propertyExpression, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>(
                typeResolver,
                propertyExpression,
                args,
                formatProvider);

        public static ITranslateResult Format<TLocalization, TDefaultLocalization>(IDynamicTypeResolver typeResolver,
            Func<TLocalization, string> propertyFun, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
            => new ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>(
                typeResolver,
                propertyFun,
                args,
                formatProvider);
    }
}