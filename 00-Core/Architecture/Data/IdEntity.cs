using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data
{
    public interface IIdEntity<TId> : IEntity, IIdModel<TId>
    {
        //TId Id { get; set; }
    }
    public interface IInt32IdEntity : IIdEntity<int> { }
    public interface IInt64IdEntity : IIdEntity<long> { }
    public interface IGuidIdEntity : IIdEntity<Guid> { }
}