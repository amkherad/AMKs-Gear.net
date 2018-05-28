using System;
using AMKsGear.Core.LocalizationFramework;
using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.Core.Data.ErrorHandling
{
    public class UniqueIndexOrPrimaryKeyOrConstriantViolationException : DataCoreException, IUniqueIndexException, IPrimaryKeyException, IConstraintViolationException
    {
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException()
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrPrimaryKeyOrConstriantViolation))
        { }
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException(Exception innerException)
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrPrimaryKeyOrConstriantViolation), innerException)
        { }
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException(string message)
            : base(message)
        { }
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}