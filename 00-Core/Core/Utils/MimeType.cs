namespace AMKsGear.Core.Utils
{
    /// <summary>
    /// Multipurpose Internet Mail Extensions wrapper type.
    /// </summary>
    public class MimeType
    {
        public string Media { get; set; }
        public string Type { get; set; }

        public override string ToString() => $"{Media}/{Type}";
    }
}