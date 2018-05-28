using System;

namespace AMKsGear.Core.Data.RawInterface
{
    public interface IRawInterfaceContext
    {
        Version MinSupportedVersion { get; }
        Version MaxSupportedVersion { get; }

        Version PreferredVersion { get; }
    }
}