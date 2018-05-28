using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.LifetimeManagers;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public abstract class QueryableEntityService<TEntity, TOptions> :
        ICrudService<TEntity, TOptions>,
        IRelationalCrudService<TEntity, TOptions>,
        IEntityStateService<TEntity, TOptions>,
        IQueryableSupportCrudService<TEntity, TOptions>,
        IQueryExecutorCrudService<TEntity, TOptions>,
        IDisposable
        where TEntity : class, IEntity
        where TOptions : ICrudServiceOptions
    {
        public event EventHandler ChangeDetected;

        public event CrudServiceChangeDetected<TEntity, TOptions> InsertDetected;
        public event CrudServiceChangeDetected<TEntity, TOptions> UpdateDetected;
        public event CrudServiceChangeDetected<TEntity, TOptions> DeleteDetected;

        public event CrudServiceBatchActionDetected<TEntity, TOptions> BatchInsertDetected;
        public event CrudServiceBatchActionDetected<TEntity, TOptions> BatchUpdateDetected;
        public event CrudServiceBatchActionDetected<TEntity, TOptions> BatchDeleteDetected;

        public virtual IQueryable<TEntity> Queryable { get; }

        public QueryableEntityService(IQueryable<TEntity> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));

            Queryable = queryable;
        }

        #region Meta
        //~QueryableEntityService()
        //{
        //    Dispose(false);
        //}
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected abstract void Dispose(bool disposing);

        public virtual object GetUnderlyingContext() => Queryable;

        public virtual object GetService(Type serviceType)
        {
            if (serviceType == typeof(IQueryable<TEntity>) ||
                serviceType == typeof(IQueryable<>) ||
                serviceType == typeof(IQueryable))
            {
                return Queryable;
            }
            throw new NotSupportedException();
        }
        public virtual object GetService(string serviceName)
        {
            switch (serviceName)
            {
                case CrudServiceSubService.QueryableService:
                    return Queryable;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceName));
            }
        }
        public virtual object GetService(string serviceName, Type crudType)
        {
            switch (serviceName)
            {
                case CrudServiceSubService.QueryableService:
                    return Queryable;
                default:
                    throw new ArgumentOutOfRangeException(nameof(serviceName));
            }
        }
        
        protected virtual void OnChangeDetected() => ChangeDetected?.Invoke(this, EventArgs.Empty);

        protected virtual void OnInsertDetected(TEntity entity) => InsertDetected?.Invoke(this, entity);
        protected virtual void OnUpdateDetected(TEntity entity) => UpdateDetected?.Invoke(this, entity);
        protected virtual void OnDeleteDetected(TEntity entity) => DeleteDetected?.Invoke(this, entity);

        protected virtual void OnBatchInsertDetected(IEnumerable<TEntity> entities) => BatchInsertDetected?.Invoke(this, entities);
        protected virtual void OnBatchUpdateDetected(IEnumerable<TEntity> entities) => BatchUpdateDetected?.Invoke(this, entities);
        protected virtual void OnBatchDeleteDetected(IEnumerable<TEntity> entities) => BatchDeleteDetected?.Invoke(this, entities);
        #endregion

        #region AsQueryable
        IQueryable IQueryableSupportCrudService<TOptions>.AsQueryable() => Queryable;
        public IQueryable<TEntity> AsQueryable() => Queryable;
        #endregion

        #region Protected Helpers
        protected virtual IQueryable<TEntity> ApplyOptions(IQueryable<TEntity> query, TOptions options)
        {
            if (options == null) return query;

            //if (options.Skip != null)
            //{
            //    table = table.Skip((int)options.Skip.Value);
            //
            //    //Logger.Write($"Skip: {options.Skip.Value}");
            //}
            //if (options.Take != null)
            //{
            //    table = table.Take((int)options.Take.Value);
            //
            //    //Logger.Write($"Take: {options.Take.Value}");
            //}
            //
            //
            //
            //var sorting = options.Sorting;
            //if (sorting != null)
            //{
            //    var first = sorting.First();
            //    table = first.Order == SortingOrder.Descending
            //        ? table.OrderByDescending(first.FieldSelector)
            //        : table.OrderBy(first.FieldSelector);
            //}

            return query;
        }

        protected virtual IEnumerable<TResult> ToMultiResult<TResult>(IQueryable<TResult> source, TOptions options)
            => source.ToList();
        protected virtual TResult ToSingleResult<TResult>(IQueryable<TResult> source, TOptions options)
            => source.FirstOrDefault();

        public abstract Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(TEntity other);
        //public abstract Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(int id);
        public abstract Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(IEnumerable<TEntity> others);
        #endregion

        #region GetAll
        protected virtual IQueryable<TEntity> GetAllHelper(IQueryable<TEntity> source, TOptions options)
        {
            return ApplyOptions(source, options);
        }

        public virtual IEnumerable<TEntity> GetAll(TOptions options) => ToMultiResult(GetAllHelper(Queryable, options), options);
        public virtual IEnumerable<TSelect> GetAll<TSelect>(
            Expression<Func<TEntity, TSelect>> @select, TOptions options)
            => ToMultiResult(GetAllHelper(Queryable, options).Select(@select), options);
        public abstract IEnumerable<TEntity> GetAll(Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToMultiResult(GetAllHelper(Queryable.IncludeAll(includes), options), options);
        public abstract IEnumerable<TSelect> GetAll<TSelect>(Expression<Func<TEntity, TSelect>> @select,
            Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToMultiResult(GetAllHelper(Queryable.IncludeAll(includes), options).Select(@select), options);
        IEnumerable ICrudService<TOptions>.GetAll(TOptions options) => GetAll(options);
        #endregion

        #region Find
        protected virtual IQueryable<TEntity> FindHelper(IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate, TOptions options)
        {
            return ApplyOptions(source.Where(predicate), options);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ToSingleResult(FindHelper(Queryable, predicate, options), options);
        public virtual TSelect Find<TSelect>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> @select, TOptions options)
            => ToSingleResult(FindHelper(Queryable, predicate, options).Select(@select), options);
        public abstract TEntity Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes,
            TOptions options); // => ToSingleResult(FindHelper(Queryable.IncludeAll(includes), predicate, options), options);
        public abstract TSelect Find<TSelect>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> @select, Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToSingleResult(FindHelper(Queryable.IncludeAll(includes), predicate, options).Select(@select), options);
        public abstract TEntity Find(TEntity entity, Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToSingleResult(
        //            FindHelper(Queryable.IncludeAll(includes), CreateEqualityComparerExpression(entity), options),
        //            options);
        public abstract TSelect Find<TSelect>(TEntity entity, Expression<Func<TEntity, TSelect>> @select,
            Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToSingleResult(
        //            FindHelper(Queryable.IncludeAll(includes), CreateEqualityComparerExpression(entity), options)
        //                .Select(@select), options);

        //public TEntity Find(int id, TOptions options)
        //{
        //    return _setOptions(Table.Where(x => x.Id.Equals(id)), options).FirstOrDefault();
        //}
        //public TSelect Find<TSelect>(int id, Expression<Func<TEntity, TSelect>> @select, CrudOptions<TEntity> options)
        //{
        //    lock (RawWrapper)
        //        return _setOptions(Table.Where(x => x.Id.Equals(id)), options).Select(@select).FirstOrDefault();
        //}
        #endregion

        #region FindAll
        protected virtual IQueryable<TEntity> FindAllHelper(IQueryable<TEntity> source,
            Expression<Func<TEntity, bool>> predicate, TOptions options)
        {
            return ApplyOptions(source.Where(predicate), options);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ToMultiResult(FindAllHelper(Queryable, predicate, options), options);
        public virtual IEnumerable<TSelect> FindAll<TSelect>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, TOptions options)
            => ToMultiResult(FindAllHelper(Queryable, predicate, options).Select(select), options);

        public abstract IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToMultiResult(FindAllHelper(Queryable.IncludeAll(includes), predicate, options), options);
        public abstract IEnumerable<TSelect> FindAll<TSelect>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> @select, Expression<Func<TEntity, object>>[] includes, TOptions options);
        //    => ToMultiResult(FindAllHelper(Queryable.IncludeAll(includes), predicate, options).Select(@select), options);
        #endregion

        #region RawQueryResult
        IEnumerable IQueryExecutorCrudService.MultiResultQuery(string query, params object[] args)
            => MultiResultQuery(query, args);
        object IQueryExecutorCrudService.SingleResultQuery(string query, params object[] args)
            => SingleResultQuery(query, args);

        public abstract object RawResultQuery(string query, params object[] args);
        public abstract IEnumerable<TEntity> MultiResultQuery(string query, params object[] args);
        public abstract TEntity SingleResultQuery(string query, params object[] args);
        public abstract TScalar ScalarResultQuery<TScalar>(string query, params object[] args);
        #endregion

        #region Insert
        public abstract IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, TOptions options);
        #endregion

        #region Update
        public abstract IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, TOptions options);
        #endregion

        #region InsertOrUpdate
        public abstract IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities, TOptions options);
        #endregion

        #region Delete
        public abstract IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate, TOptions options);
        public abstract IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities, TOptions options);
        public abstract long Delete(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, object>>[] includes, TOptions options);
        public abstract TEntity Delete(TEntity entity, Expression<Func<TEntity, object>>[] includes, TOptions options);
        #endregion

        #region EntityState
        #region Any
        public virtual bool Any(TOptions options)
            => ApplyOptions(Queryable, options).Any();
        public virtual bool Any(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ApplyOptions(Queryable, options).Any(predicate);
        #endregion

        #region Count
        public virtual int Count(TOptions options)
            => ApplyOptions(Queryable, options).Count();
        public virtual long LongCount(TOptions options)
            => ApplyOptions(Queryable, options).LongCount();
        public virtual int Count(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ApplyOptions(Queryable, options).Count(predicate);
        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ApplyOptions(Queryable, options).LongCount(predicate);
        #endregion

        #region Exists
        public bool Exists(TEntity entity, TOptions options)
            => ApplyOptions(Queryable, options).Any(CreateEqualityComparerExpression(entity));
        #endregion

        #region All
        public bool All(Expression<Func<TEntity, bool>> predicate, TOptions options)
            => ApplyOptions(Queryable, options).All(predicate);
        #endregion

        #region Max
        public TEntity Max(TOptions options)
            => ApplyOptions(Queryable, options).Max();
        public TProperty Max<TProperty>(Expression<Func<TEntity, TProperty>> selector, TOptions options)
            => ApplyOptions(Queryable, options).Max(selector);
        #endregion

        #region Min
        public TEntity Min(TOptions options)
            => ApplyOptions(Queryable, options).Min();
        public TProperty Min<TProperty>(Expression<Func<TEntity, TProperty>> selector, TOptions options)
            => ApplyOptions(Queryable, options).Min(selector);
        #endregion
        #endregion

        #region LifetimeManaging
        public abstract IActionBlock BeginActionBlock();
        public abstract ITransaction BeginTransaction();
        #endregion
    }
}
