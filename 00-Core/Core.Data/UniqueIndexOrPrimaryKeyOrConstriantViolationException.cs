using System;
using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data
{
    public class UniqueIndexOrPrimaryKeyOrConstriantViolationException : ConstraintViolationException
    {
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException()
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrPrimaryKeyOrConstraintViolation))
        { }
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException(Exception innerException)
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrPrimaryKeyOrConstraintViolation), innerException)
        { }
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException(string message)
            : base(message)
        { }
        public UniqueIndexOrPrimaryKeyOrConstriantViolationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}