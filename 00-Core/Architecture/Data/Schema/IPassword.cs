using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IPassword
    {
        string Password { get; set; }
    }
    public interface IPasswordEntity : IPassword, IEntity { }
    public interface IPasswordModel : IPassword, IModel { }
}