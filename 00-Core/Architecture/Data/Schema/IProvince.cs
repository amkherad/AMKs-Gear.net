using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IProvince
    {
        string Name { get; set; }
        string Description { get; set; }
    }
    public interface IProvinceEntity : IProvince, IEntity { }
    public interface IProvinceModel : IProvince, IModel { }
}
