using System;
using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data
{
    public class UniqueIndexOrConstraintViolationException : ConstraintViolationException
    {
        public UniqueIndexOrConstraintViolationException()
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrConstraintViolation))
        { }
        public UniqueIndexOrConstraintViolationException(Exception innerException)
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.UniqueIndexOrConstraintViolation), innerException)
        { }
        public UniqueIndexOrConstraintViolationException(string message)
            : base(message)
        { }
        public UniqueIndexOrConstraintViolationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}