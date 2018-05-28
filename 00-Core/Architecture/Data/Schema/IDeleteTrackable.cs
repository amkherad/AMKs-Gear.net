using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IDeleteTrackable
    {
        DateTime DeleteDateTime { get; set; }
    }
    public interface IDeleteTrackableEntity : IDeleteTrackable, IEntity { }
    public interface IDeleteTrackableModel : IDeleteTrackable, IModel { }
}