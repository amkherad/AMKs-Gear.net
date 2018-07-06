using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IDescriptionString
    {
        string Description { get; set; }
    }
    public interface IDescriptionStringEntity : IDescriptionString, IEntity { }
    public interface IDescriptionStringModel : IDescriptionString, IModel { }
}