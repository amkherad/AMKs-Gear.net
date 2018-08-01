using System;
using AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data
{
    public class ConstraintViolationException : DataCoreException
    {
        public ConstraintViolationException()
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.ConstraintViolation))
        { }
        public ConstraintViolationException(Exception innerException)
            : base(LocalizationServices.Format<ISqlExceptionsLocalization, DefaultSqlExceptionsLocalization>(x => x.ConstraintViolation), innerException)
        { }
        public ConstraintViolationException(string message)
            : base(message)
        { }
        public ConstraintViolationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}