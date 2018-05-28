using System;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Modeling
{
    public interface IParameterDescriptor : IWrapper
    {
        string Name { get; }
        Type Type { get; }
        object DefaultValue { get; }

        bool HasDefaultValue { get; }
        bool IsIn { get; }
        bool IsOut { get; }
        bool IsOptional { get; }
        bool IsRetVal { get; }

        int Position { get; }
        IMemberInfo Member { get; }
    }
}