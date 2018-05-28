using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IHideable
    {
        bool Visible { get; set; }
    }
    public interface IHideableEntity : IHideable, IEntity { }
    public interface IHideableModel : IHideable, IModel { }
}