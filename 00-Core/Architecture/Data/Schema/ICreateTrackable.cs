using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ICreateTrackable
    {
        DateTime CreatedDateTime { get; set; }
    }
    public interface ICreateTrackableEntity : ICreateTrackable, IEntity { }
    public interface ICreateTrackableModel : ICreateTrackable, IModel { }
}