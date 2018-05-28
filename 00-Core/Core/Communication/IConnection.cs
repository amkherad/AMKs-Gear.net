using System;

namespace AMKsGear.Core.Communication
{
    public interface IConnection
    {
        object ConnectionObject { get; }

        string GetConnectionString(IFormatProvider formatProvider);
        string GetConnectionString();
    }
}
