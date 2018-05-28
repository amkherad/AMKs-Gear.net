using System;

namespace AMKsGear.Core.Automation.IoC.Options
{
    public class ConstantTypedValue : ConstantValue
    {
        public Type ParameterType { get; }

        public ConstantTypedValue(Type parameterType, object parameterValue)
            : base(parameterValue)
        {
            ParameterType = parameterType;
        }
        public ConstantTypedValue(Type parameterType)
            : base(null)
        {
            ParameterType = parameterType;
        }
        public ConstantTypedValue(object parameterValue)
            : base(parameterValue)
        {
            if (parameterValue == null) throw new ArgumentNullException(nameof(parameterValue));
            ParameterType = parameterValue.GetType();
        }
    }
}