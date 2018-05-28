namespace AMKsGear.Core.Data.Serialization
{
    public class SerializationOptions
    {
        public bool CamelCase { get; set; }
        public bool EscapeNulls { get; set; }

        public static SerializationOptions JsOptions()
        {
            return new SerializationOptions
            {
                CamelCase = true,
                EscapeNulls = true
            };
        }
    }
}