using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ITitle
    {
        string Title { get; set; }
    }
    public interface ITitleEntity : ITitle, IEntity { }
    public interface ITitleModel : ITitle, IModel { }
}