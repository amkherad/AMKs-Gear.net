using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface IRelationalCrudService<TOptions> : ICrudService<TOptions>
        where TOptions : ICrudServiceOptions
    {

    }
    public interface IRelationalCrudService<TEntity, TOptions> : IRelationalCrudService<TOptions>
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>>[] includes, TOptions options);
        IEnumerable<TSelect> GetAll<TSelect>(Expression<Func<TEntity, TSelect>> select, Expression<Func<TEntity, object>>[] includes, TOptions options);
        
        TEntity Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes, TOptions options);
        TSelect Find<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> select, Expression<Func<TEntity, object>>[] includes, TOptions options);

        TEntity Find(TEntity entity, Expression<Func<TEntity, object>>[] includes, TOptions options);
        TSelect Find<TSelect>(TEntity entity, Expression<Func<TEntity, TSelect>> select, Expression<Func<TEntity, object>>[] includes, TOptions options);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes, TOptions options);
        IEnumerable<TSelect> FindAll<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> select, Expression<Func<TEntity, object>>[] includes, TOptions options);

        long Delete(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes, TOptions options);
        TEntity Delete(TEntity entity, Expression<Func<TEntity, object>>[] includes, TOptions options);
    }
}