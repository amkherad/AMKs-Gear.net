using System;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IAuditable : ICreateTrackable, IUpdateTrackable
    {

    }

    public interface IAuditableEntity : IAuditable, IEntity, ICreateTrackableEntity, IUpdateTrackableEntity
    {
    }
    public interface IForcedUpdateAuditableEntity : ICreateTrackableEntity, IForcedUpdateTrackableEntity
    {
    }

    public interface IFullAuditableEntity<TOwner> : IEntity, IAuditableEntity, ICreateTrackableEntity, IUpdateTrackableEntity, IOwnerTrackableEntity<TOwner>
        where TOwner : struct
    {
    }
    public interface IInt32FullAuditableEntity : IFullAuditableEntity<int>, IInt32IdEntity, IInt32OwnerTrackableEntity { }
    public interface IInt64FullAuditableEntity : IFullAuditableEntity<long>, IInt64IdEntity, IInt64OwnerTrackableEntity { }
    public interface IGuidFullAuditableEntity : IFullAuditableEntity<Guid>, IGuidIdEntity, IGuidOwnerTrackableEntity { }
}