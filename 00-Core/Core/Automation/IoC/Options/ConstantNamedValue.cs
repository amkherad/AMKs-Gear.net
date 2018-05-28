namespace AMKsGear.Core.Automation.IoC.Options
{
    public class ConstantNamedValue : ConstantValue
    {
        public string ParameterName { get; }

        public ConstantNamedValue(string parameterName, object parameterValue)
            : base(parameterValue)
        {
            ParameterName = parameterName;
        }
        public ConstantNamedValue(string parameterName)
            : base(null)
        {
            ParameterName = parameterName;
        }
    }
}