using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IDescription
    {
        string Description { get; set; }
    }
    public interface IDescriptionEntity : IDescription, IEntity { }
    public interface IDescriptionModel : IDescription, IModel { }
}