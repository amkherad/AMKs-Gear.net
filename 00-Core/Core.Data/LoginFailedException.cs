using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data
{
    public class LoginFailedException : DataCoreException
    {
        public LoginFailedException()
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.LoginFailed))
        { }
    }
}