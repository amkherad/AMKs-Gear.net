using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface IIdCrudService<TEntity, TOptions, TId> : ICrudService<TEntity, TOptions>
        where TEntity : IIdEntity<TId>
        where TOptions : ICrudServiceOptions
    {
        TEntity Find(TId id, TOptions options);
        TSelect Find<TSelect>(TId id, Expression<Func<TEntity, TSelect>> select, TOptions options);

        bool Update(TId id, TEntity newValues, TOptions options);
        
        bool Delete(TId id, TOptions options);

        Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(TId id); // => x => x.Id.Equals(id);
    }
    public interface IInt32IdCrudService<TEntity, TOptions> : IIdCrudService<TEntity, TOptions, int>
        where TEntity : IIdEntity<int> where TOptions : ICrudServiceOptions { }
    public interface IInt64IdCrudService<TEntity, TOptions> : IIdCrudService<TEntity, TOptions, long>
        where TEntity : IIdEntity<long> where TOptions : ICrudServiceOptions { }
    public interface IGuidIdCrudService<TEntity, TOptions> : IIdCrudService<TEntity, TOptions, Guid>
        where TEntity : IIdEntity<Guid> where TOptions : ICrudServiceOptions { }
}