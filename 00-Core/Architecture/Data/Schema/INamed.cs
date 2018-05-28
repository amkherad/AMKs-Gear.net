using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface INamed
    {
        string Name { get; set; }
    }
    public interface INamedEntity : INamed, IEntity { }
    public interface INamedModel : INamed, IModel { }
}