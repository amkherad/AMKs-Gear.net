namespace AMKsGear.Architecture.Data.Serialization
{
    public interface ISerializerFactory
    {
        string SupportedEngines { get; }

        ISerializer CreateSerializer(string engine);

        IJsonSerializer JsonSerializer();
        IXmlSerializer XmlSerializer();
    }
}