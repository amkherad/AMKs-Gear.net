using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.Core.Data.ErrorHandling
{
    public class LoginFailedException : DataCoreException
    {
        public LoginFailedException()
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.LoginFailed))
        { }
    }
}