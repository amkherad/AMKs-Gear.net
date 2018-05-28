using System;

namespace AMKsGear.Core.TraceTools.Mocking
{
    public interface IMockup
    {
        Type RealType { get; }
    }
    public interface IMockup<TMockType>
    {

    }
}