namespace AMKsGear.Core.Data.Serialization.Json
{
    public class JsonSerializer : SerializerBase
    {
        public JsonSerializationContext Context { get; }
        
        
        public JsonSerializer(JsonSerializationContext context)
        {
            Context = context;
        }
    }
}