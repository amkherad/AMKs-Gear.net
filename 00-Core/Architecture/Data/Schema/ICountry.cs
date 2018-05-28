using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ICountry
    {
        string Name { get; set; }
        string Continent { get; set; }
        string CultureId { get; set; }
        string Languages { get; set; }
        string Religions { get; set; }
        string Description { get; set; }
    }
    public interface ICountryEntity : ICountry, IEntity { }
    public interface ICountryModel : ICountry, IModel { }
}