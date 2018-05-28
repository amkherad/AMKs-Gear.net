using System;

namespace AMKsGear.Core.Communication
{
    public class ConnectionString : IConnection
    {
        public string ConnectionValue { get; set; }

        public ConnectionString() { }
        public ConnectionString(string connectionString)
        {
            ConnectionValue = connectionString;
        }

        public string GetConnectionString(IFormatProvider formatProvider)
        {
            return ConnectionValue;
        }

        public string GetConnectionString()
        {
            return ConnectionValue;
        }
        public object ConnectionObject => ConnectionValue;
    }
}
