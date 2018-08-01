using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data
{
    public class ForeignKeyViolationException : DataCoreException
    {
        public ForeignKeyViolationException()
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.ForeignKeyViolation))
        { }
    }
}