using System;
using System.Reflection;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Modeling
{
    public interface IParameterDescriptor : IAdapter
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
        ParameterInfo Member { get; }
    }
}