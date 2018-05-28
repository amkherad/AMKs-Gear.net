namespace AMKsGear.Core.Trace
{
    public class LoggerOption
    {
        public const string LoggerOptionCategory = "Category";

        public string Name { get; }
        public object Value { get; }

        public LoggerOption(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}