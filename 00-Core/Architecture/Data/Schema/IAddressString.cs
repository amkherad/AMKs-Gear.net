using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IAddressString
    {
        string Address { get; set; }
    }
    public interface IAddressStringEntity : IAddressString, IEntity { }
    public interface IAddressStringModel : IAddressString, IModel { }
}