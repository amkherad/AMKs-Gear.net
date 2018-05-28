using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data;
using AMKsGear.Core.Data.AbstractInterface;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace AMKsGear.Core.Data
{
    public static class CrudServiceExtensions
    {
        #region Basic Crud Services
        #region Simple Getters
        #region GetAll
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> GetAll<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.GetAll(
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> GetAll<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.GetAll(select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        #region Find
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity Find<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Find(predicate,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelect Find<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Find(predicate, select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity Find<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.Find(service.CreateEqualityComparerExpression(entity),
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelect Find<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, Expression<Func<TEntity, TSelect>> select,
            object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.Find(service.CreateEqualityComparerExpression(entity), select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        #region FindAll
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> FindAll<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.FindAll(predicate,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> FindAll<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.FindAll(predicate, select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> FindAll<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.FindAll(service.CreateEqualityComparerExpression(entities),
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> FindAll<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities,
            Expression<Func<TEntity, TSelect>> select, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.FindAll(service.CreateEqualityComparerExpression(entities), select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion
        #endregion
        #region Ranged Crud Services
        #region GetAllRanged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> GetAllRanged<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service,
            long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => service.GetAll(
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> GetAllRanged<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, TSelect>> select,
            long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => service.GetAll(select,
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take));
        #endregion

        #region FindAllRanged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> FindAllRanged<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => service.FindAll(predicate,
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> FindAllRanged<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, long skip, long take,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> => service.FindAll(predicate, select,
                DefaultCrudServiceOptions.FromPagingParameters<TEntity, TOptions>(sortField, sortingOrder, skip, take));
        #endregion
        #endregion
        #region Paging Crud Services
        #region GetAllPaged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> GetAllPaged<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service,
            long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                service.GetAllRanged(page * pageSize, pageSize, sortField, sortingOrder);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> GetAllPaged<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, TSelect>> select,
            long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                service.GetAllRanged(select, page * pageSize, pageSize, sortField, sortingOrder);
        #endregion

        #region FindAllPaged
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> FindAllPaged<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                service.FindAllRanged(predicate, page * pageSize, pageSize, sortField, sortingOrder);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSelect> FindAllPaged<TEntity, TOptions, TSelect>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TSelect>> select, long page, long pageSize,
            Expression<Func<TEntity, object>> sortField = null, SortingOrder sortingOrder = SortingOrder.Ascending)
            where TEntity : IEntity where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity> =>
                service.FindAllRanged(predicate, select, page * pageSize, pageSize, sortField, sortingOrder);
        #endregion
        #endregion

        #region Insert
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity Insert<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Insert(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Insert<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.Insert(entities, DefaultCrudServiceOptions.Default<TOptions>());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Insert<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Insert(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Insert<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Insert(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        #region Update
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity Update<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Update(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Update<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.Update(entities, DefaultCrudServiceOptions.Default<TOptions>());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Update<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Update(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Update<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Update(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        #region InsertOrUpdate
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity InsertOrUpdate<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.InsertOrUpdate(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> InsertOrUpdate<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.InsertOrUpdate(entities, DefaultCrudServiceOptions.Default<TOptions>());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> InsertOrUpdate<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.InsertOrUpdate(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> InsertOrUpdate<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.InsertOrUpdate(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        #region Delete
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Delete<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, Expression<Func<TEntity, bool>> predicate, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Delete(predicate,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity Delete<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, TEntity entity, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Delete(new[] {entity},
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context)).FirstOrDefault();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Delete<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions
            => service.Delete(entities, DefaultCrudServiceOptions.Default<TOptions>());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Delete<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, IEnumerable<TEntity> entities, object context = null)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Delete(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEntity> Delete<TEntity, TOptions>(
            this ICrudService<TEntity, TOptions> service, object context, params TEntity[] entities)
            where TEntity : IEntity where TOptions : ICrudServiceOptions => service.Delete(entities,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion
        #endregion

        #region Id Crud Services
        #region Find
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TEntity Find<TEntity, TOptions, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => service.Find(id,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TSelect Find<TEntity, TOptions, TSelect, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, Expression<Func<TEntity, TSelect>> select,
            object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => service.Find(id, select,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Update<TEntity, TOptions, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, TEntity newValues, object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => service.Update(id, newValues,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Delete<TEntity, TOptions, TId>(
            this IIdCrudService<TEntity, TOptions, TId> service, TId id, object context = null)
            where TEntity : IIdEntity<TId> where TOptions : ICrudServiceOptions => service.Delete(id,
                context == null
                    ? DefaultCrudServiceOptions.Default<TOptions>()
                    : DefaultCrudServiceOptions.FromContext<TOptions>(context));
        #endregion

        #region Relational Crud Services

        #endregion

        #region Meta Crud Services
        #region GetService
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TService GetService<TService, TEntity, TOptions>(this ICrudService<TEntity, TOptions> crud)
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
            => (TService) crud.GetService(typeof(TService));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TService GetService<TService, TEntity, TOptions>(this ICrudService<TEntity, TOptions> crud, string serviceName)
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
            => (TService)crud.GetService(serviceName, typeof(TService));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TService GetService<TService, TOptions>(this ICrudService<TOptions> crud)
        where TOptions : ICrudServiceOptions
            => (TService)crud.GetService(typeof(TService));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TService GetService<TService, TOptions>(this ICrudService<TOptions> crud, string serviceName)
        where TOptions : ICrudServiceOptions
            => (TService)crud.GetService(serviceName, typeof(TService));
        #endregion
        #region AsQueryable
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<TEntity> AsQueryable<TEntity, TOptions>(this ICrudService<TEntity, TOptions> crud)
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
            => crud.GetService<IQueryable<TEntity>, TEntity, TOptions>(CrudServiceSubService.QueryableService);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable AsQueryable<TOptions>(this ICrudService<TOptions> crud)
        where TOptions : ICrudServiceOptions
            => crud.GetService<IQueryable, TOptions>(CrudServiceSubService.QueryableService);
        #endregion
        #endregion
    }
}