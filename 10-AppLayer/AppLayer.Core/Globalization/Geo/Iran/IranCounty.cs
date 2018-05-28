namespace AMKsGear.AppLayer.Core.Globalization.Geo.Iran
{
    public class IranCounty
    {
        public string Name { get; }
        public string ParsiName { get; }
        public string PhonePrefix { get; }

        public IranCounty(string name, string parsiName, string phonePrefix)
        {
            Name = name;
            ParsiName = parsiName;
            PhonePrefix = phonePrefix;
        }
    }
}