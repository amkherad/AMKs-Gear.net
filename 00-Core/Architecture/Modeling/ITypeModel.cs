using System;
using System.Collections.Generic;
using System.Reflection;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Modeling
{
    public interface ITypeModel : IWrapper
    {
        Type Type { get; }
        TypeInfo TypeInfo { get; }

        IEnumerable<IMemberInfo> AllMembers { get; }
        IEnumerable<PropertyInfo> Properties { get; }
        IEnumerable<FieldInfo> Fields { get; }
        IEnumerable<MethodInfo> Methods { get; } 
        IEnumerable<EventInfo> Events { get; } 
        
    }
}