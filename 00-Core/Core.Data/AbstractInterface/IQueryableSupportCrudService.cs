using System.Linq;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface IQueryableSupportCrudService<TOptions> : ICrudService<TOptions>
        where TOptions : ICrudServiceOptions
    {
        IQueryable AsQueryable();
    }
    public interface IQueryableSupportCrudService<TEntity, TOptions> : IQueryableSupportCrudService<TOptions>, ICrudService<TEntity, TOptions>
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
    {
        new IQueryable<TEntity> AsQueryable();
    }
}