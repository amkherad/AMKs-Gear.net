using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IPhone
    {
        string Phone { get; set; }
    }
    public interface IPhoneEntity : IPhone, IEntity { }
    public interface IPhoneModel : IPhone, IModel { }
}