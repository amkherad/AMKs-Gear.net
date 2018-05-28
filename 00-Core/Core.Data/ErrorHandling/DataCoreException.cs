using System;

namespace AMKsGear.Core.Data.ErrorHandling
{
    public interface IDataCoreException
    {
        
    }
    public class DataCoreException : Exception, IDataCoreException
    {
        public DataCoreException() { }
        public DataCoreException(string message) : base(message) { }
        public DataCoreException(string message, Exception innerException) : base(message, innerException) { }
    }
}