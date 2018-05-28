using System;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IOwnerTrackable<TOwnerIdType>
        where TOwnerIdType : struct
    {
        TOwnerIdType? OwnerId { get; set; }
    }
    public interface IOwnerTrackableEntity<TOwnerIdType> : IOwnerTrackable<TOwnerIdType>, IEntity
        where TOwnerIdType : struct
    { }
    public interface IInt32OwnerTrackableEntity : IOwnerTrackable<int>, IInt32IdEntity { }
    public interface IInt64OwnerTrackableEntity : IOwnerTrackable<long>, IInt64IdEntity { }
    public interface IGuidOwnerTrackableEntity : IOwnerTrackable<Guid>, IGuidIdEntity { }

    public interface IForcedOwnerTrackable<TOwnerIdType>
        where TOwnerIdType : struct
    {
        TOwnerIdType OwnerId { get; set; }
    }
    public interface IForcedOwnerTrackableEntity<TOwnerIdType> : IForcedOwnerTrackable<TOwnerIdType>, IEntity
        where TOwnerIdType : struct
    { }
    public interface IInt32ForcedOwnerTrackableEntity : IForcedOwnerTrackableEntity<int>, IInt32IdEntity { }
    public interface IInt64ForcedOwnerTrackableEntity : IForcedOwnerTrackableEntity<long>, IInt64IdEntity { }
    public interface IGuidForcedOwnerTrackableEntity : IForcedOwnerTrackableEntity<Guid>, IGuidIdEntity { }
}