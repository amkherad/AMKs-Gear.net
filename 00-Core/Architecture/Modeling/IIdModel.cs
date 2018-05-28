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

    public interface IBoundedIdModel<TId, TBoundedEntity> : IIdModel<TId>, IBoundedModel<TBoundedEntity>
        where TBoundedEntity : IEntity
    { }
    public interface IInt32BoundedIdModel<TBoundedEntity> : IBoundedIdModel<int, TBoundedEntity> where TBoundedEntity : IEntity { }
    public interface IInt64BoundedIdModel<TBoundedEntity> : IBoundedIdModel<long, TBoundedEntity> where TBoundedEntity : IEntity { }
    public interface IGuidBoundedIdModel<TBoundedEntity> : IBoundedIdModel<Guid, TBoundedEntity> where TBoundedEntity : IEntity { }
}