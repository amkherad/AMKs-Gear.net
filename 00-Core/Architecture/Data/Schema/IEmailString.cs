using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IEmailString
    {
        string Email { get; set; }
    }
    public interface IEmailStringEntity : IEmailString, IEntity { }
    public interface IEmailStringModel : IEmailString, IModel { }
}