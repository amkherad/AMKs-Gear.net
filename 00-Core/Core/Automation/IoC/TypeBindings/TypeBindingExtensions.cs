using System;
using System.Linq.Expressions;
using System.Reflection;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Automation.IoC.TypeBindings
{
    public static class TypeBindingExtensions
    {
        #region BindProperty
        #region BindPropertyResolve
        private static void _bindPropertyResolve(this ITypeResolverAppliableContainer container,
            Type type, Type resolvingType, string propertyName, params object[] options)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));

            var propertyInfo = type.GetRuntimeProperty(propertyName);
            if (propertyInfo == null) throw new InvalidOperationException(
                Localization.Format<ITypeResolverLocalization, DefaultTypeResolverLocalization>(
                    x => x.PropertyNotFound, propertyName
                ));

            container.RegisterApplier(type, new TypeBindingOnPropertyResolver(
                container, propertyInfo, resolvingType, options));
        }
        public static void BindProperty(this ITypeResolverAppliableContainer container,
            Type type, Type resolvingType, string propertyName, params object[] options)
            => _bindPropertyResolve(container, type, resolvingType, propertyName, options);
        public static void BindProperty<TBase, TResolve>(this ITypeResolverAppliableContainer container,
            string propertyName, params object[] options)
            => _bindPropertyResolve(container, typeof(TBase), typeof(TResolve), propertyName, options);
        public static void BindProperty<TBase, TResolve, TProperty>(this ITypeResolverAppliableContainer container,
            Expression<Func<TBase, TProperty>> propertyExpression, params object[] options)
        => _bindPropertyResolve(container, typeof(TBase), typeof(TResolve), propertyExpression.GetMemberName(), options);
        public static void BindProperty<TBase, TResolve>(this ITypeResolverAppliableContainer container,
            Expression<Func<TBase, object>> propertyExpression, params object[] options)
        => _bindPropertyResolve(container, typeof(TBase), typeof(TResolve), propertyExpression.GetMemberName(), options);
        #endregion
        #region BindPropertyConstant
        private static void _bindPropertyConstant(this ITypeResolverAppliableContainer container,
            Type type, string propertyName, object value)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (type == null) throw new ArgumentNullException(nameof(type));

            var propertyInfo = type.GetRuntimeProperty(propertyName);
            if (propertyInfo == null) throw new InvalidOperationException();

            container.RegisterApplier(type,
                new TypeBindingOnPropertyConstant(propertyInfo, value));
        }
        public static void BindProperty(this ITypeResolverAppliableContainer container,
            Type type, string propertyName, object value)
            => _bindPropertyConstant(container, type, propertyName, value);
        public static void BindProperty<TBase>(this ITypeResolverAppliableContainer container,
            string propertyName, object value)
            => _bindPropertyConstant(container, typeof(TBase), propertyName, value);
        public static void BindProperty<TBase, TProperty>(this ITypeResolverAppliableContainer container,
            Expression<Func<TBase, TProperty>> propertyExpression, object value)
        => _bindPropertyConstant(container, typeof(TBase), propertyExpression.GetMemberName(), value);
        public static void BindProperty<TBase>(this ITypeResolverAppliableContainer container,
            Expression<Func<TBase, object>> propertyExpression, object value)
        => _bindPropertyConstant(container, typeof(TBase), propertyExpression.GetMemberName(), value);
        #endregion
        #endregion
    }
}