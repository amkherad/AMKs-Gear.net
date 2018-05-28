using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Modeling
{
    public interface IModelMemberInfo : IWrapper
    {
        //Type DeclaringType { get; }
        //Module Module { get; }
        string Name { get; }
        Type Type { get; }
        //Type ReflectedType { get; }

        IEnumerable<Attribute> GetCustomAttributes(bool inherit);
        IEnumerable<Attribute> GetCustomAttributes(Type attributeType, bool inherit);

        bool IsDefined(Type attributeType, bool inherit);
        object GetValue(object instance);
        object GetValue(object instance, object defaultValue);
        void SetValue(object instance, object value);
    }
}