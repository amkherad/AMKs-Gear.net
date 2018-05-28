using System;
using System.Collections.Generic;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    public class TypeModel : ITypeModel
    {
        public Type Type { get; }
        public TypeInfo TypeInfo { get; }

        public TypeModel(Type type)
        {
            Type = type;
            TypeInfo = type.GetTypeInfo();
        }
        public TypeModel(TypeInfo typeInfo)
        {
            TypeInfo = typeInfo;
            Type = typeInfo.AsType();
        }

        public object GetUnderlyingContext() => Type;

        public IEnumerable<IMemberInfo> AllMembers { get; }
        public IEnumerable<PropertyInfo> Properties { get; }
        public IEnumerable<FieldInfo> Fields { get; }
        public IEnumerable<MethodInfo> Methods { get; }
        public IEnumerable<EventInfo> Events { get; }
    }
}