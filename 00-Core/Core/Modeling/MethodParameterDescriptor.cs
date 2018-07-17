using System;
using System.Reflection;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Modeling
{
    /// <summary>
    /// Provides abstract access to parameter info.
    /// </summary>
    public class MethodParameterDescriptor : IParameterDescriptor
    {
        public ParameterInfo ParameterInfo { get; }

        public MethodParameterDescriptor(ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo;
        }

        public object GetUnderlyingContext() => ParameterInfo;

        public string Name => ParameterInfo.Name;
        public Type Type => ParameterInfo.ParameterType;
        public object DefaultValue => ParameterInfo.DefaultValue;
        public bool HasDefaultValue => ParameterInfo.HasDefaultValue;
        public bool IsIn => ParameterInfo.IsIn;
        public bool IsOut => ParameterInfo.IsOut;
        public bool IsOptional => ParameterInfo.IsOptional;
        public bool IsRetVal => ParameterInfo.IsRetval;
        public int Position => ParameterInfo.Position;
        public IMemberInfo Member => new ClassMemberInfo(ParameterInfo.Member);
    }
}