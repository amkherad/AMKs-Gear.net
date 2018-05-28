using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IFullName
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
    public interface IFullNameEntity : IFullName, IEntity { }
    public interface IFullNameModel : IFullName, IModel { }
}