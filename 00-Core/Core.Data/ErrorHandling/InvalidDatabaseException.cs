using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.Core.Data.ErrorHandling
{
    public class InvalidDatabaseException : DataCoreException
    {
        public InvalidDatabaseException()
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.InvalidDatabase))
        { }
    }
}