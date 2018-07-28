using System;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Architecture.Modeling
{
    public interface IIdModel<TId> : IModel
    {
        TId Id { get; set; }
    }
    public interface IInt32IdModel : IIdModel<int> { }
    public interface IInt64IdModel : IIdModel<long> { }
    public interface IGuidIdModel : IIdModel<Guid> { }
}