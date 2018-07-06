using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IPasswordString
    {
        string Password { get; set; }
    }
    public interface IPasswordStringEntity : IPasswordString, IEntity { }
    public interface IPasswordStringModel : IPasswordString, IModel { }
}