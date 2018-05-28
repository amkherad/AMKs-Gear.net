using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ICity
    {
        string Name { get; set; }
        string Description { get; set; }
    }
    public interface ICityEntity : ICity, IEntity { }
    public interface ICityModel : ICity, IModel { }
}
