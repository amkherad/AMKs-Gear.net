using System.Collections.Generic;
using AMKsGear.Core.Data.RawInterface.RawObjectBuilder;
using AMKsGear.Core.Data.RawInterface.StoredProcedures;
using AMKsGear.Core.Data.RawInterface.Triggers;

namespace AMKsGear.Core.Data.RawInterface
{
    /// <summary>
    /// It's the root class for raw supported environments,
    /// Here is the place for gathering services, creators, stored procedures, etc. of a single raw environment.
    /// </summary>
    public interface IRawEnvironment
    {
        IRawObjectBuilderService[] RawObjectBuilderServices { get; }
        IRawObjectBuilderContext GetRawObjectBuilderContext(object callingContext);
        IEnumerable<IDbTrigger> CreateTriggers();
        IEnumerable<IStoredProcedure> CreateStoredProcedures();
    }
}