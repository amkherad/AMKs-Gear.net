using System;
using System.Collections.Generic;
using System.Reflection;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Automation.IoC.Options;

namespace AMKsGear.Core.Automation.IoC.TypeBindings
{
    public abstract class TypeBindingOnProperty : ITypeResolverApplier
    {
        public PropertyInfo PropertyInfo { get; }

        public TypeBindingOnProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) throw new ArgumentNullException(nameof(propertyInfo));

            PropertyInfo = propertyInfo;
        }

        public abstract object GetValue();

        public void ApplyBindings(object instance, ITypeResolver resolver, TypeResolverTypeMapping mapping,
            TypeResolverTypeMappingContext context, List<TypeResolverOption> options)
        {
            var value = GetValue();

            PropertyInfo.SetValue(instance, value);
        }
    }

    public class TypeBindingOnPropertyResolver : TypeBindingOnProperty
    {
        public ITypeResolver TypeResolver { get; }
        public Type ResolvingType { get; }
        public object[] ResolvingOptions { get; }

        public TypeBindingOnPropertyResolver(ITypeResolver typeResolver, PropertyInfo propertyInfo,
            Type resolvingType, object[] resolvingOptions)
            : base(propertyInfo)
        {
            if (typeResolver == null) throw new ArgumentNullException(nameof(typeResolver));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));

            TypeResolver = typeResolver;
            ResolvingType = resolvingType;
            ResolvingOptions = resolvingOptions;
        }

        public override object GetValue() => ResolvingOptions == null
            ? TypeResolver.Resolve(ResolvingType, this)
            : TypeResolver.Resolve(ResolvingType, this, ResolvingOptions);
    }

    public class TypeBindingOnPropertyConstant : TypeBindingOnProperty
    {
        public object Value { get; }

        public TypeBindingOnPropertyConstant(PropertyInfo propertyInfo, object value)
            : base(propertyInfo)
        {
            Value = value;
        }

        public override object GetValue() => Value;
    }
}