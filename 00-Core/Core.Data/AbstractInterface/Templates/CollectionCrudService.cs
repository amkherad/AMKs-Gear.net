using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Data.AbstractInterface.Templates
{
    public class CollectionCrudService<TEntity, TOptions> : ICrudService<TEntity, TOptions>
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

        private readonly ICollection<TEntity> _innerCollection;
        public virtual IQueryable<TEntity> Source { get; }

        public CollectionCrudService(ICollection<TEntity> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            _innerCollection = collection;
            Source = collection.AsQueryable();
        }

        public virtual object GetUnderlyingContext() => Source;

        protected void OnChangeDetected()
            => ChangeDetected?.Invoke(this, EventArgs.Empty);
        protected void OnInsertDetected(TEntity entity) => InsertDetected?.Invoke(this, entity);
        protected void OnUpdateDetected(TEntity entity) => UpdateDetected?.Invoke(this, entity);
        protected void OnDeleteDetected(TEntity entity) => DeleteDetected?.Invoke(this, entity);
        protected void OnBatchInsertDetected(IEnumerable<TEntity> entities)
            => BatchInsertDetected?.Invoke(this, entities);
        protected void OnBatchUpdateDetected(IEnumerable<TEntity> entities)
            => BatchUpdateDetected?.Invoke(this, entities);
        protected void OnBatchDeleteDetected(IEnumerable<TEntity> entities)
            => BatchDeleteDetected?.Invoke(this, entities);

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

        protected virtual IQueryable<TEntity> ApplyOptions(IQueryable<TEntity> list, TOptions options)
        {
            return list;
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

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, TOptions options)
        {
            foreach (var entity in entities)
            {
                _innerCollection.Add(entity);
                OnInsertDetected(entity);
            }
            OnBatchInsertDetected(entities);
            return entities;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public virtual IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, TOptions options)
        {
            foreach (var entity in entities)
                OnUpdateDetected(entity);
            OnBatchUpdateDetected(entities);
            return entities.ToList();
        }
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public virtual IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities, TOptions options)
        {
            var list = _innerCollection as IList<TEntity>;
            var inserts = new List<TEntity>();
            var updates = new List<TEntity>();
            if (list != null)
            {
                foreach (var entity in entities)
                {
                    _innerCollection.Add(entity);
                    OnInsertDetected(entity);
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    if (_innerCollection.AddDistinct(entity))
                    {
                        OnInsertDetected(entity);
                        inserts.Add(entity);
                    }
                    else
                    {
                        OnUpdateDetected(entity);
                        updates.Add(entity);
                    }
                }
            }

            if (inserts.Any()) OnBatchInsertDetected(inserts);
            if (updates.Any()) OnBatchUpdateDetected(updates);

            return entities;
        }
        public virtual IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate, TOptions options)
        {
            var all = _innerCollection.Where(predicate.Compile()).ToList();
            foreach (var entity in all)
                _innerCollection.Remove(entity);
            return all;
        }
        public virtual IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities, TOptions options)
        {
            var all = entities.ToList();
            foreach (var entity in all)
                _innerCollection.Remove(entity);
            return all;
        }

        public virtual Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(TEntity other)
            => x => x.Equals(other);
        public virtual Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(IEnumerable<TEntity> others)
            => x => others.Any(o => o.Equals(x));
    }
}