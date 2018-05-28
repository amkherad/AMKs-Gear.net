using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IUpdateTrackable
    {
        DateTime? UpdateDateTime { get; set; }
    }
    public interface IForcedUpdateTrackable
    {
        DateTime UpdateDateTime { get; set; }
    }

    public interface IUpdateTrackableEntity : IUpdateTrackable, IEntity { }
    public interface IUpdateTrackableModel : IUpdateTrackable, IModel { }

    public interface IForcedUpdateTrackableEntity : IForcedUpdateTrackable, IEntity { }
    public interface IForcedUpdateTrackableModel : IForcedUpdateTrackable, IModel { }
}