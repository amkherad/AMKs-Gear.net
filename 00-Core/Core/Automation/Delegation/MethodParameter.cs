using System;
using AMKsGear.Core.Automation.Reflection;

namespace AMKsGear.Core.Automation.Delegation
{
    public class MethodParameter
    {
        public string Name { get; }
        public Type ParameterType { get; }

        public object Value { get; set; }

        private object _defaultValue;
        private bool _useDefaultValue = false;
        public object DefaultValue
        {
            get
            {
                if (_useDefaultValue)
                    return _defaultValue;
                return ParameterType.IsValueType ? Activator.CreateInstance(ParameterType) : null;
            }
            set
            {
                _useDefaultValue = true;
                _defaultValue = value;
            }
        }

        public MethodParameter(string name, Type parameterType)
        {
            Name = name;
            ParameterType = parameterType;
        }
    }
}
