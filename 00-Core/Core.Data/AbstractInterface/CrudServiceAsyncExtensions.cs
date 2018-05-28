using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data;
using AMKsGear.Core.Data.AbstractInterface;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace AMKsGear.Core.Data
{
    public static class CrudServiceAsyncExtensions
    {
        #region Basic Crud Services
        #region Simple Getters
        #region GetAll
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> GetAllAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.GetAll(
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> GetAllAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.GetAll(select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        #region Find
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> FindAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Find(predicate,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TSelect> FindAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Find(predicate, select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> FindAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.Find(service.CreateEqualityComparerExpression(entity),
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TSelect> FindAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, Expression<Func<TEntity, TSelect>> select,
            object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.Find(service.CreateEqualityComparerExpression(entity), select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        #region FindAll
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> FindAllAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.FindAll(predicate,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> FindAllAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.FindAll(predicate, select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> FindAllAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.FindAll(service.CreateEqualityComparerExpression(entities),
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> FindAllAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.FindAll(service.CreateEqualityComparerExpression(entities), select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion
        #endregion
        #region Ranged Crud Services
        #region GetAllRanged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> GetAllRangedAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service,
            long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => Task.Run(() => service.GetAll(
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> GetAllRangedAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, TSelect>> select,
            long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => Task.Run(() => service.GetAll(select,
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take)));
        #endregion

        #region FindAllRanged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> FindAllRangedAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => Task.Run(() => service.FindAll(predicate,
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> FindAllRangedAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => Task.Run(() => service.FindAll(predicate, select,
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take)));
        #endregion
        #endregion
        #region Paging Crud Services
        #region GetAllPaged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> GetAllPagedAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service,
            long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                Task.Run(() => service.GetAllRanged(page * pageSize, pageSize, sortField, sortingOrder));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> GetAllPagedAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, TSelect>> select,
            long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                Task.Run(() => service.GetAllRanged(select, page * pageSize, pageSize, sortField, sortingOrder));
        #endregion

        #region FindAllPaged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> FindAllPagedAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                Task.Run(() => service.FindAllRanged(predicate, page * pageSize, pageSize, sortField, sortingOrder));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TSelect>> FindAllPagedAsync<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                Task.Run(() => service.FindAllRanged(predicate, select, page * pageSize, pageSize, sortField, sortingOrder));
        #endregion
        #endregion

        #region Insert
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> InsertAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Insert(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> InsertAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.Insert(entities, DefaultCrudServiceOptions.Default<TOptions>()));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> InsertAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Insert(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> InsertAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Insert(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        #region Update
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> UpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Update(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> UpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.Update(entities, DefaultCrudServiceOptions.Default<TOptions>()));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> UpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Update(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> UpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Update(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        #region InsertOrUpdate
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> InsertOrUpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.InsertOrUpdate(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> InsertOrUpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.InsertOrUpdate(entities, DefaultCrudServiceOptions.Default<TOptions>()));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> InsertOrUpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.InsertOrUpdate(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> InsertOrUpdateAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.InsertOrUpdate(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        #region Delete
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> DeleteAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Delete(predicate,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> DeleteAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Delete(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> DeleteAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => Task.Run(() => service.Delete(entities, DefaultCrudServiceOptions.Default<TOptions>()));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> DeleteAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Delete(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<TEntity>> DeleteAsync<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => Task.Run(() => service.Delete(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion
        #endregion

        #region Id Crud Services
        #region Find
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TEntity> FindAsync<TEntity, TOptions, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => Task.Run(() => service.Find(id,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TSelect> FindAsync<TEntity, TOptions, TSelect, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, Expression<Func<TEntity, TSelect>> select,
            object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => Task.Run(() => service.Find(id, select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<bool> UpdateAsync<TEntity, TOptions, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, TEntity newValues, object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => Task.Run(() => service.Update(id, newValues,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<bool> DeleteAsync<TEntity, TOptions, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => Task.Run(() => service.Delete(id,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)));
        #endregion

        #region Relational Crud Services

        #endregion
    }
}