using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AMKsGear.Architecture.Automation;
using AMKsGear.Architecture.Automation.Mapper;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public delegate void ModelMemberInfoValueSetter(IModelMemberInfo memberInfo, object instance, object value);
    public delegate object ModelMemberInfoValueGetter(IModelMemberInfo memberInfo, object instance);

    public class ModelAbstractMemberInfo : IModelMemberInfo
    {
        public virtual string Name { get; }
        public virtual Type Type { get; }
        public MemberInfo MemberInfo { get; }
        public virtual IEnumerable<object> CustomAttributes { get; }

        //public IValueProv ValueResolver { get; }
        public ModelMemberInfoValueSetter ValueSetter { get; }
        public ModelMemberInfoValueGetter ValueGetter { get; }

        public ModelAbstractMemberInfo(
            string name,
            Type type,
            IEnumerable<object> customAttributes,
            //IValueResolver valueResolver,
            ModelMemberInfoValueGetter valueGetter,
            ModelMemberInfoValueSetter valueSetter,
            MemberInfo memberInfo)
        {
            Name = name;
            Type = type;
            CustomAttributes = customAttributes;
            //ValueResolver = valueResolver;
            ValueGetter = valueGetter;
            ValueSetter = valueSetter;
            MemberInfo = memberInfo;
        }

        public object GetUnderlyingContext() => null;

        public virtual IEnumerable<object> GetCustomAttributes(bool inherit)
            => CustomAttributes;

        public virtual IEnumerable<object> GetCustomAttributes(Type attributeType, bool inherit)
            => CustomAttributes;

        public virtual bool IsDefined(Type attributeType, bool inherit)
            => CustomAttributes.Any(x => x.GetType() == attributeType);

        public virtual object GetValue(object instance)
        {
//            var valueResolver = ValueResolver;
//            if (valueResolver != null)
//                return valueResolver.GetValue(Name);

            return ValueGetter?.Invoke(this, instance);
        }

        public virtual object GetValue(object instance, object defaultValue)
        {
//            var valueResolver = ValueResolver;
//            if (valueResolver != null)
//                return valueResolver.GetValue(Name) ?? defaultValue;

            return ValueGetter?.Invoke(this, instance) ?? defaultValue;
        }

        public virtual void SetValue(object instance, object value)
        {
            ValueSetter?.Invoke(this, instance, value);
        }
    }
}