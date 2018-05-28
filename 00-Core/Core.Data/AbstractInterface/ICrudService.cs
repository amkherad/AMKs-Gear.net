using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface ICrudService<in TOptions> : IWrapper, IServiceProvider
        where TOptions : ICrudServiceOptions
    {
        /// <summary>
        /// A lazy-implemented method to retrieve all records in set.
        /// </summary>
        /// <param name="options">Optional platform-specified options.</param>
        /// <returns></returns>
        IEnumerable GetAll(TOptions options);

        object GetService(string serviceName);
        object GetService(string serviceName, Type crudType);
    }
    public interface ICrudService<TEntity, TOptions> : ICrudService<TOptions>
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
    {
        event EventHandler ChangeDetected;

        event CrudServiceChangeDetected<TEntity, TOptions> InsertDetected;
        event CrudServiceChangeDetected<TEntity, TOptions> UpdateDetected;
        event CrudServiceChangeDetected<TEntity, TOptions> DeleteDetected;

        event CrudServiceBatchActionDetected<TEntity, TOptions> BatchInsertDetected;
        event CrudServiceBatchActionDetected<TEntity, TOptions> BatchUpdateDetected;
        event CrudServiceBatchActionDetected<TEntity, TOptions> BatchDeleteDetected;

        /// <summary>
        /// A lazy-implemented method to retrieve all records in set.
        /// </summary>
        /// <param name="options">Optional platform-specified options.</param>
        /// <returns></returns>
        new IEnumerable<TEntity> GetAll(TOptions options);
        IEnumerable<TSelect> GetAll<TSelect>(Expression<Func<TEntity, TSelect>> select, TOptions options);

        TEntity Find(Expression<Func<TEntity, bool>> predicate, TOptions options);
        TSelect Find<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> select, TOptions options);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, TOptions options);
        IEnumerable<TSelect> FindAll<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> select, TOptions options);

        IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, TOptions options);
        
        IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, TOptions options);
        
        IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities, TOptions options);
        
        IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate, TOptions options);
        IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities, TOptions options);

        Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(TEntity other);
        Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(IEnumerable<TEntity> others);
    }
}