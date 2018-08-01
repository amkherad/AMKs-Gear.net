using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data
{
    public class InvalidDatabaseException : DataCoreException
    {
        public InvalidDatabaseException()
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.InvalidDatabase))
        { }
    }
}