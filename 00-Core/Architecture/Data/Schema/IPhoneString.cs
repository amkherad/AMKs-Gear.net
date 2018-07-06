using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IPhoneString
    {
        string Phone { get; set; }
    }
    public interface IPhoneStringEntity : IPhoneString, IEntity { }
    public interface IPhoneStringModel : IPhoneString, IModel { }
}