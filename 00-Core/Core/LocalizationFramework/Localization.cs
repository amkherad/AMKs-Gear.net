using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.LocalizationFramework;
using AMKsGear.Core.Automation.IoC;

namespace AMKsGear.Core.LocalizationFramework
{
    public static class Localization
    {
        internal static readonly ILazyTypeResolver TypeResolver = new LazyTypeResolverContainer();

        #region Formatters
        public static LocalizableResult Format(Type localizationType, string format)
        {
            return format;
        }
        public static LocalizableResult Format(Type localizationType, string format, params object[] args)
        {
            return format;
        }

        public static LocalizableResult Format<TLocalization>(string format)
            where TLocalization : ILocalization
        {
            return format;
        }
        public static LocalizableResult Format<TLocalization>(
            string format, params object[] args)
            where TLocalization : ILocalization
        {
            return args == null
                ? string.Format(format)
                : string.Format(format, args);
        }
        public static LocalizableResult Format<TLocalization>(
            string format,
            IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalization
        {
            return args == null
                ? string.Format(format, formatProvider)
                : string.Format(format, formatProvider, args);
        }

        #region Default Expression

        #region Simple Format
        public static LocalizableResult FormatExpression<TLocalization, TDefaultLocalization>(
            Expression<Func<TLocalization, string>> propertyExpression)
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
                => new DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>(
                    TypeResolver,
                    propertyExpression);
        public static LocalizableResult Format<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc)
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
                => new DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>(
                    TypeResolver,
                    propertyFunc);
        #endregion
        #region Params Format
        public static LocalizableResult FormatExpression<TLocalization, TDefaultLocalization>(
            Expression<Func<TLocalization, string>> propertyExpression, params object[] args)
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
                => new DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>(
                    TypeResolver,
                    propertyExpression,
                    args);
        public static LocalizableResult Format<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc, params object[] args)
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
                => new DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>(
                    TypeResolver,
                    propertyFunc,
                    args);
        #endregion
        #region FormatProvider Format
        public static LocalizableResult FormatExpression<TLocalization, TDefaultLocalization>(
            Expression<Func<TLocalization, string>> propertyExpression, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
                => new DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>(
                    TypeResolver,
                    propertyExpression,
                    args,
                    formatProvider);
        public static LocalizableResult Format<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFun, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
                => new DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>(
                    TypeResolver,
                    propertyFun,
                    args,
                    formatProvider);
        #endregion

        internal class DefaultExpressionLocalizationResult<TLocalization, TDefaultLocalization>
            : LocalizableResult
            where TLocalization : ILocalization
            where TDefaultLocalization : class, TLocalization
        {
            private readonly ILazyTypeResolver _typeResolver;
            private readonly Func<TLocalization, string> _propertyFunc;
            private readonly Expression<Func<TLocalization, string>> _propertyExpression;
            private readonly object[] _args;
            private readonly IFormatProvider _formatProvider;

            #region Constructors
            #region Constructor #1
            public DefaultExpressionLocalizationResult(
                ILazyTypeResolver typeResolver,
                Func<TLocalization, string> propertyFunc)
            {
                _typeResolver = typeResolver;
                _propertyFunc = propertyFunc;
            }
            public DefaultExpressionLocalizationResult(
                ILazyTypeResolver typeResolver,
                Expression<Func<TLocalization, string>> propertyExpression)
            {
                _typeResolver = typeResolver;
                _propertyExpression = propertyExpression;
            }
            #endregion

            #region Constructor #2
            public DefaultExpressionLocalizationResult(
                ILazyTypeResolver typeResolver,
                Func<TLocalization, string> propertyFunc,
                object[] args)
            {
                _typeResolver = typeResolver;
                _propertyFunc = propertyFunc;
                _args = args;
            }
            public DefaultExpressionLocalizationResult(
                ILazyTypeResolver typeResolver,
                Expression<Func<TLocalization, string>> propertyExpression,
                object[] args)
            {
                _typeResolver = typeResolver;
                _propertyExpression = propertyExpression;
                _args = args;
            }
            #endregion

            #region Constructor #3
            public DefaultExpressionLocalizationResult(
                ILazyTypeResolver typeResolver,
                Func<TLocalization, string> propertyFunc,
                object[] args,
                IFormatProvider formatProvider)
            {
                _typeResolver = typeResolver;
                _propertyFunc = propertyFunc;
                _args = args;
                _formatProvider = formatProvider;
            }
            public DefaultExpressionLocalizationResult(
                ILazyTypeResolver typeResolver,
                Expression<Func<TLocalization, string>> propertyExpression,
                object[] args,
                IFormatProvider formatProvider)
            {
                _typeResolver = typeResolver;
                _propertyExpression = propertyExpression;
                _args = args;
                _formatProvider = formatProvider;
            }
            #endregion
            #endregion
            public override string GetResult()
            {
                var defaultLocalization = _typeResolver.Resolve<TLocalization, TDefaultLocalization>();
                if (defaultLocalization == null) throw new InvalidOperationException();
                var format = _propertyExpression == null
                    ? _propertyFunc(defaultLocalization)
                    : (_propertyExpression.Compile())(defaultLocalization);
                return _formatProvider == null
                    ? Format<TLocalization>(format, _args)
                    : Format<TLocalization>(format, _formatProvider, _args);
            }
        }
        #endregion
        #endregion
    }
}