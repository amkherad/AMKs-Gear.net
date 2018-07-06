using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IUsernameStringPasssword : IUsernameString, IPasswordString
    {
    }
    public interface IUsernameStringPassswordStringEntity : IUsernameStringPasssword, IUsernameStringEntity, IPasswordStringEntity { }
    public interface IUsernameStringPassswordStringModel : IUsernameStringPasssword, IUsernameStringModel, IPasswordStringModel { }
}