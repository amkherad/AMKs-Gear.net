using AMKsGear.Core.LocalizationFramework;
using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.Core.Data.ErrorHandling
{
    public class ForeignKeyViolationException : DataCoreException
    {
        public ForeignKeyViolationException()
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.ForeignKeyViolation))
        { }
    }
}