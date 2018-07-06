using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IFullNameString
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
    public interface IFullNameStringEntity : IFullNameString, IEntity { }
    public interface IFullNameStringModel : IFullNameString, IModel { }
}