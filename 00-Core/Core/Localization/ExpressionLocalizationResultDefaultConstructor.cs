using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Localization;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Automation.IoC;

namespace AMKsGear.Core.Localization
{
    internal class ExpressionLocalizationResultDefaultConstructor<TLocalization, TDefaultLocalization>
        : ITranslateResult
        where TLocalization : ILocalizationModel
        where TDefaultLocalization : class, TLocalization, new()
    {
        private readonly Func<TLocalization, string> _propertyFunc;
        private readonly Expression<Func<TLocalization, string>> _propertyExpression;
        private readonly object[] _args;
        private readonly IFormatProvider _formatProvider;

        public ExpressionLocalizationResultDefaultConstructor(
            Func<TLocalization, string> propertyFunc)
        {
            _propertyFunc = propertyFunc;
        }

        public ExpressionLocalizationResultDefaultConstructor(
            Expression<Func<TLocalization, string>> propertyExpression)
        {
            _propertyExpression = propertyExpression;
        }

        public ExpressionLocalizationResultDefaultConstructor(
            Func<TLocalization, string> propertyFunc,
            object[] args)
        {
            _propertyFunc = propertyFunc;
            _args = args;
        }

        public ExpressionLocalizationResultDefaultConstructor(
            Expression<Func<TLocalization, string>> propertyExpression,
            object[] args)
        {
            _propertyExpression = propertyExpression;
            _args = args;
        }

        public ExpressionLocalizationResultDefaultConstructor(
            Func<TLocalization, string> propertyFunc,
            object[] args,
            IFormatProvider formatProvider)
        {
            _propertyFunc = propertyFunc;
            _args = args;
            _formatProvider = formatProvider;
        }

        public ExpressionLocalizationResultDefaultConstructor(
            Expression<Func<TLocalization, string>> propertyExpression,
            object[] args,
            IFormatProvider formatProvider)
        {
            _propertyExpression = propertyExpression;
            _args = args;
            _formatProvider = formatProvider;
        }


        public string GetValue()
        {
            var defaultLocalization = new TDefaultLocalization();
            
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