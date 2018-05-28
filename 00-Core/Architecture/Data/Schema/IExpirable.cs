using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IExpirable
    {
        DateTime ExpirationDateTime { get; set; }
    }
    public interface IExpirableEntity : IExpirable, IEntity { }
    public interface IExpirableModel : IExpirable, IModel { }
}