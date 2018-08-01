using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Localization;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.IoC;

namespace AMKsGear.Core.Localization
{
    internal class ExpressionLocalizationResultTypeResolver<TLocalization, TDefaultLocalization>
        : ITranslateResult
        where TLocalization : ILocalizationModel
        where TDefaultLocalization : class, TLocalization
    {
        private readonly IDynamicTypeResolver _typeResolver;
        private readonly Func<TLocalization, string> _propertyFunc;
        private readonly Expression<Func<TLocalization, string>> _propertyExpression;
        private readonly object[] _args;
        private readonly IFormatProvider _formatProvider;

        public ExpressionLocalizationResultTypeResolver(
            IDynamicTypeResolver typeResolver,
            Func<TLocalization, string> propertyFunc)
        {
            _typeResolver = typeResolver;
            _propertyFunc = propertyFunc;
        }

        public ExpressionLocalizationResultTypeResolver(
            IDynamicTypeResolver typeResolver,
            Expression<Func<TLocalization, string>> propertyExpression)
        {
            _typeResolver = typeResolver;
            _propertyExpression = propertyExpression;
        }

        public ExpressionLocalizationResultTypeResolver(
            IDynamicTypeResolver typeResolver,
            Func<TLocalization, string> propertyFunc,
            object[] args)
        {
            _typeResolver = typeResolver;
            _propertyFunc = propertyFunc;
            _args = args;
        }

        public ExpressionLocalizationResultTypeResolver(
            IDynamicTypeResolver typeResolver,
            Expression<Func<TLocalization, string>> propertyExpression,
            object[] args)
        {
            _typeResolver = typeResolver;
            _propertyExpression = propertyExpression;
            _args = args;
        }

        public ExpressionLocalizationResultTypeResolver(
            IDynamicTypeResolver typeResolver,
            Func<TLocalization, string> propertyFunc,
            object[] args,
            IFormatProvider formatProvider)
        {
            _typeResolver = typeResolver;
            _propertyFunc = propertyFunc;
            _args = args;
            _formatProvider = formatProvider;
        }

        public ExpressionLocalizationResultTypeResolver(
            IDynamicTypeResolver typeResolver,
            Expression<Func<TLocalization, string>> propertyExpression,
            object[] args,
            IFormatProvider formatProvider)
        {
            _typeResolver = typeResolver;
            _propertyExpression = propertyExpression;
            _args = args;
            _formatProvider = formatProvider;
        }


        public string GetValue()
        {
            var defaultLocalization = _typeResolver.Resolve<TLocalization, TDefaultLocalization>();
            if (defaultLocalization == null) throw new InvalidOperationException();
            
            var format = _propertyExpression == null
                ? _propertyFunc(defaultLocalization)
                : (_propertyExpression.Compile())(defaultLocalization);
            
            return _formatProvider == null
                ? LocalizationServices.Format<TLocalization>(format, _args)
                : LocalizationServices.Format<TLocalization>(format, _formatProvider, _args);
        }

        object ILazyValue.GetValue() => GetValue();
    }
}