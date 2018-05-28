namespace AMKsGear.Core.Automation.IoC.Options
{
    public class ConstantValue : TypeResolverOption
    {
        public object ParameterValue { get; }
        public ConstantValue(object parameterValue)
        {
            ParameterValue = parameterValue;
        }
    }
}