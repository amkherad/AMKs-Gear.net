using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Automation.Mapper.Options
{
    public class MapperOption : IKeyValuePairBehavior<string, object>
    {
        public string Key { get; }
        public object Value { get; }
        
        
        
        public MapperOption(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}