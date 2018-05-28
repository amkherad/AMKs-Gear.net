using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface IEntityStateService<in TOptions> : IWrapper
        where TOptions : ICrudServiceOptions
    {
        bool Any(TOptions options);
        int Count(TOptions options);
        long LongCount(TOptions options);
    }
    public interface IEntityStateService<TEntity, in TOptions> : IEntityStateService<TOptions>
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
    {
        bool Exists(TEntity entity, TOptions options);
        bool Any(Expression<Func<TEntity, bool>> predicate, TOptions options);
        bool All(Expression<Func<TEntity, bool>> predicate, TOptions options);
        int Count(Expression<Func<TEntity, bool>> predicate, TOptions options);
        long LongCount(Expression<Func<TEntity, bool>> predicate, TOptions options);

        TEntity Max(TOptions options);
        TProperty Max<TProperty>(Expression<Func<TEntity, TProperty>> selector, TOptions options);

        TEntity Min(TOptions options);
        TProperty Min<TProperty>(Expression<Func<TEntity, TProperty>> selector, TOptions options);
        //IEnumerable<TEntity> Min(Expression<Func<TEntity, object>> selector, TOptions options);
    }
}