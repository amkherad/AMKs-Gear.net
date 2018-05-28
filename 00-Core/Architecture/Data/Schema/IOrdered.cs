using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IOrdered<TOrder>
        //where TOrder : IEquatable<TOrder>
    {
        TOrder Order { get; set; }
    }

    public interface IOrderedEntity<TOrder> : IOrdered<TOrder>, IEntity
        //where TOrder : IEquatable<TOrder>
    { }
    public interface IInt32OrderedEntity : IOrderedEntity<int>, IInt32IdEntity { }
    public interface IInt64OrderedEntity : IOrderedEntity<long>, IInt64IdEntity { }
    public interface IGuidOrderedEntity : IOrderedEntity<Guid>, IGuidIdEntity { }

    public interface IOrderedModel<TOrder> : IOrdered<TOrder>, IModel
        //where TOrder : IEquatable<TOrder>
    { }
    public interface IInt32OrderedModel : IOrderedEntity<int>, IInt32IdEntity { }
    public interface IInt64OrderedModel : IOrderedEntity<long>, IInt64IdEntity { }
    public interface IGuidOrderedModel : IOrderedEntity<Guid>, IGuidIdEntity { }
}