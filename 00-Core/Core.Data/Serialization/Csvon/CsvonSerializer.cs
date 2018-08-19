namespace AMKsGear.Core.Data.Serialization.Csvon
{
    public class CsvonSerializer : SerializerBase
    {
        public CsvonSerializationContext Context { get; }
        
        
        public CsvonSerializer(CsvonSerializationContext context)
        {
            Context = context;
        }
    }
}