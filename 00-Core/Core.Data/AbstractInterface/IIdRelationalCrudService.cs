using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface IIdRelationalCrudService<TEntity, TOptions, TId> : IIdCrudService<TEntity, TOptions, TId>
        where TEntity : IIdEntity<TId>
        where TOptions : ICrudServiceOptions
    {
        TEntity Delete(TId id, Expression<Func<TEntity, object>>[] includes, TOptions options);
    }
    public interface IInt32IdRelationalCrudService<TEntity, TOptions> : IIdRelationalCrudService<TEntity, TOptions, int>, IInt32IdCrudService<TEntity, TOptions>
        where TEntity : IIdEntity<int> where TOptions : ICrudServiceOptions { }
    public interface IInt64IdRelationalCrudService<TEntity, TOptions> : IIdRelationalCrudService<TEntity, TOptions, long>, IInt64IdCrudService<TEntity, TOptions>
        where TEntity : IIdEntity<long> where TOptions : ICrudServiceOptions { }
    public interface IGuidIdRelationalCrudService<TEntity, TOptions> : IIdRelationalCrudService<TEntity, TOptions, Guid>, IGuidIdCrudService<TEntity, TOptions>
        where TEntity : IIdEntity<Guid> where TOptions : ICrudServiceOptions { }
}