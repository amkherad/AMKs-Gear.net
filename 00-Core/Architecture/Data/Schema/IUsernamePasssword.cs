using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IUsernamePasssword : IUsername, IPassword
    {
    }
    public interface IUsernamePassswordEntity : IUsernamePasssword, IUsernameEntity, IPasswordEntity { }
    public interface IUsernamePassswordModel : IUsernamePasssword, IUsernameModel, IPasswordModel { }
}