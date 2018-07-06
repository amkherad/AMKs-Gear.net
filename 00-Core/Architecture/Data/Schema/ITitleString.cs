using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ITitleString
    {
        string Title { get; set; }
    }
    public interface ITitleStringEntity : ITitleString, IEntity { }
    public interface ITitleStringModel : ITitleString, IModel { }
}