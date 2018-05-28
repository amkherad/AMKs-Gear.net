using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IAddress
    {
        string Address { get; set; }
    }
    public interface IAddressEntity : IAddress, IEntity { }
    public interface IAddressModel : IAddress, IModel { }
}