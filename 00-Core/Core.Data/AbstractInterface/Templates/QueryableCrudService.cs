using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface.Templates
{
    public class QueryableCrudService<TEntity, TOptions> : ICrudService<TEntity, TOptions>
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
    {
        public event EventHandler ChangeDetected;
        public event CrudServiceChangeDetected<TEntity, TOptions> InsertDetected;
        public event CrudServiceChangeDetected<TEntity, TOptions> UpdateDetected;
        public event CrudServiceChangeDetected<TEntity, TOptions> DeleteDetected;
        public event CrudServiceBatchActionDetected<TEntity, TOptions> BatchInsertDetected;
        public event CrudServiceBatchActionDetected<TEntity, TOptions> BatchUpdateDetected;
        public event CrudServiceBatchActionDetected<TEntity, TOptions> BatchDeleteDetected;

        public virtual IQueryable<TEntity> Source { get; }

        public QueryableCrudService(IQueryable<TEntity> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));

            Source = queryable;
        }

        public virtual object GetUnderlyingContext() => Source;
        
        protected void OnInsertDetected(ICrudService<TEntity, TOptions> sender, TEntity entity)
            => InsertDetected?.Invoke(sender, entity);
        protected void OnUpdateDetected(ICrudService<TEntity, TOptions> sender, TEntity entity)
            => UpdateDetected?.Invoke(sender, entity);
        protected void OnDeleteDetected(ICrudService<TEntity, TOptions> sender, TEntity entity)
            => DeleteDetected?.Invoke(sender, entity);
        protected void OnBatchInsertDetected(ICrudService<TEntity, TOptions> sender, IEnumerable<TEntity> entities)
            => BatchInsertDetected?.Invoke(sender, entities);
        protected void OnBatchUpdateDetected(ICrudService<TEntity, TOptions> sender, IEnumerable<TEntity> entities)
            => BatchUpdateDetected?.Invoke(sender, entities);
        protected void OnBatchDeleteDetected(ICrudService<TEntity, TOptions> sender, IEnumerable<TEntity> entities)
            => BatchDeleteDetected?.Invoke(sender, entities);

        public object GetService(string serviceName)
        {
            switch (serviceName)
            {
                case CrudServiceSubService.QueryableService:
                    return Source.AsQueryable();
                default:
                    throw new NotSupportedException();
            }
        }
        public object GetService(string serviceName, Type crudType)
        {
            switch (serviceName)
            {
                case CrudServiceSubService.QueryableService:
                    if (crudType != typeof(IQueryable<TEntity>))
                        throw new NotSupportedException();

                    return Source.AsQueryable();
                default:
                    throw new NotSupportedException();
            }
        }
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IQueryable<TEntity>))
                return Source.AsQueryable();

            throw new NotSupportedException();
        }

        protected virtual IQueryable<TEntity> ApplyOptions(IQueryable<TEntity> table, TOptions options)
        {
            return table;
        }

        IEnumerable<TEntity> ICrudService<TEntity, TOptions>.GetAll(TOptions options)
            => ApplyOptions(Source, options);
        public virtual IEnumerable GetAll(TOptions options) => ApplyOptions(Source, options);
        public virtual IEnumerable<TSelect> GetAll<TSelect>(Expression<Func<TEntity, TSelect>> @select, TOptions options)
            => ApplyOptions(Source, options).Select(@select);

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ApplyOptions(Source, options).FirstOrDefault(predicate);
        public virtual TSelect Find<TSelect>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> @select, TOptions options)
            => ApplyOptions(Source.Where(predicate), options).Select(@select).FirstOrDefault();

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ApplyOptions(Source.Where(predicate), options);
        public virtual IEnumerable<TSelect> FindAll<TSelect>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> @select, TOptions options)
            => ApplyOptions(Source.Where(predicate), options).Select(@select);

        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, TOptions options)
        {
            throw new NotSupportedException();
        }
        public virtual IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, TOptions options)
        {
            throw new NotSupportedException();
        }
        public virtual IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities, TOptions options)
        {
            throw new NotSupportedException();
        }
        public virtual IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate, TOptions options)
        {
            throw new NotSupportedException();
        }
        public virtual IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities, TOptions options)
        {
            throw new NotSupportedException();
        }

        public virtual Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(TEntity other)
            => x => x.Equals(other);
        public virtual Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(IEnumerable<TEntity> others)
            => x => others.Any(o => o.Equals(x));
    }
}