using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IUsername
    {
        string Username { get; set; }
    }
    public interface IUsernameEntity : IUsername, IEntity { }
    public interface IUsernameModel : IUsername, IModel { }
}