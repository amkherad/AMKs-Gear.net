using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IUsernameString
    {
        string Username { get; set; }
    }
    public interface IUsernameStringEntity : IUsernameString, IEntity { }
    public interface IUsernameStringModel : IUsernameString, IModel { }
}