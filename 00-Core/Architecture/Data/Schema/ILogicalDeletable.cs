using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface ILogicalDeletable
    {
        bool IsDeleted { get; set; }
    }
    public interface IResidentEntity : ILogicalDeletable, IEntity { }
    public interface IResidentModel : ILogicalDeletable, IModel { }
}
