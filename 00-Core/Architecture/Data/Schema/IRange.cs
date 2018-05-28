using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IRange<T>
    {
        T Min { get; set; }
        T Max { get; set; }
    }
    public interface IRangeEntity<T> : IRange<T>, IEntity { }
    public interface IRangeModel<T> : IRange<T>, IModel { }
    
    public interface IInt32RangeEntity : IRangeEntity<int> { }
    public interface IInt54RangeEntity : IRangeEntity<long> { }
    
    public interface IInt32RangeModel : IRangeModel<int> { }
    public interface IInt54RangeModel : IRangeModel<long> { }
}