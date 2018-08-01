using System;

namespace AMKsGear.Core.Data
{
    public class DataCoreException : Exception
    {
        public DataCoreException() { }
        public DataCoreException(string message) : base(message) { }
        public DataCoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}