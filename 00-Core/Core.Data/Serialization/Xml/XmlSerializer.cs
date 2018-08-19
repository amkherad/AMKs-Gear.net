namespace AMKsGear.Core.Data.Serialization.Xml
{
    public class XmlSerializer : SerializerBase
    {
        public XmlSerializationContext Context { get; }
        
        
        public XmlSerializer(XmlSerializationContext context)
        {
            Context = context;
        }
    }
}