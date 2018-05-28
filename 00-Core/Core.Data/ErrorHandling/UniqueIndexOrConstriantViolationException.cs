using System;
using AMKsGear.Core.LocalizationFramework;
using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.Core.Data.ErrorHandling
{
    public class UniqueIndexOrConstriantViolationException : DataCoreException, IUniqueIndexException, IConstraintViolationException
    {
        public UniqueIndexOrConstriantViolationException()
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrConstriantViolation))
        { }
        public UniqueIndexOrConstriantViolationException(Exception innerException)
            : base(Localization.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrConstriantViolation), innerException)
        { }
        public UniqueIndexOrConstriantViolationException(string message)
            : base(message)
        { }
        public UniqueIndexOrConstriantViolationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}