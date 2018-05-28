namespace AMKsGear.AppLayer.Core.Globalization.Geo.Iran
{
    public struct Dialect
    {
        public string LanguageName;
        public string ParsiLanguageName;
        public string Name;
        public string ParsiName;

        public Dialect(string languageName, string parsiLanguageName, string name, string parsiName)
        {
            Name = name;
            ParsiName = parsiName;
            LanguageName = languageName;
            ParsiLanguageName = parsiLanguageName;
        }

        public static Dialect Parsi => new Dialect("Parsi", "پارسی", "Parsi", "فارسی");
        public static Dialect TorkiAzari => new Dialect("Torki", "ترکی", "Azari", "آذری");
        public static Dialect ParsiIsfahani => new Dialect("Parsi", "پارسی", "Isfahani", "اصفهانی");
    }
}