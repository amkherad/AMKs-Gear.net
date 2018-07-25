using System;

namespace AMKsGear.Core.Linq.Convert
{
    public class TypeConvertException : Exception
    {
        public TypeConvertException()
        {
        }

        public TypeConvertException(string message)
            : base(message)
        {
        }

        public TypeConvertException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}