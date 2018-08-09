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
    public interface IInt32OrderedEntity : IOrderedEntity<int> { }
    public interface IInt64OrderedEntity : IOrderedEntity<long> { }
    public interface IGuidOrderedEntity : IOrderedEntity<Guid> { }

    public interface IOrderedModel<TOrder> : IOrdered<TOrder>, IModel
        //where TOrder : IEquatable<TOrder>
    { }
    public interface IInt32OrderedModel : IOrderedModel<int> { }
    public interface IInt64OrderedModel : IOrderedModel<long> { }
    public interface IGuidOrderedModel : IOrderedModel<Guid> { }
}