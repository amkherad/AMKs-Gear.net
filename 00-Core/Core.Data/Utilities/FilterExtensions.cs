using System;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Data.Utilities
{
    public static class FilterExtensions
    {
        #region Trackables & Auditables
        #region GetUpdatedAuditables
        public static IQueryable<TEntity> GetUpdatedAuditables<TEntity>(
            this IQueryable<TEntity> queryable, string updateDateTime)
            where TEntity : class, IEntity, ICreateTrackableEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            DateTime update;
            return GetUpdatedAuditables(queryable, DateTime.TryParse(updateDateTime, out update) ? update : (DateTime?)null);
        }
        public static IQueryable<TEntity> GetUpdatedAuditables<TEntity>(
            this IQueryable<TEntity> queryable, DateTime updateDateTime)
            where TEntity : class, IEntity, ICreateTrackableEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return GetUpdatedAuditables(queryable, (DateTime?)updateDateTime);
        }
        public static IQueryable<TEntity> GetUpdatedAuditables<TEntity>(
            this IQueryable<TEntity> queryable, DateTime? updateDateTime)
            where TEntity : class, IEntity, ICreateTrackableEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return updateDateTime.HasValue
                ? queryable
                    .Where(x => x.CreatedDateTime >= updateDateTime.Value || x.UpdateDateTime >= updateDateTime.Value)
                : queryable;
        }
        #endregion
        #region GetUpdatedUpdateTrackable
        public static IQueryable<TEntity> GetUpdatedUpdateTrackable<TEntity>(
            this IQueryable<TEntity> queryable, string updateDateTime)
            where TEntity : class, IEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            DateTime update;
            return GetUpdatedUpdateTrackable(queryable, DateTime.TryParse(updateDateTime, out update) ? update : (DateTime?)null);
        }
        public static IQueryable<TEntity> GetUpdatedUpdateTrackable<TEntity>(
            this IQueryable<TEntity> queryable, DateTime updateDateTime)
            where TEntity : class, IEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return GetUpdatedUpdateTrackable(queryable, (DateTime?)updateDateTime);
        }
        public static IQueryable<TEntity> GetUpdatedUpdateTrackable<TEntity>(
            this IQueryable<TEntity> queryable, DateTime? updateDateTime)
            where TEntity : class, IEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return updateDateTime.HasValue
                ? queryable
                    .Where(x => x.UpdateDateTime >= updateDateTime.Value)
                : queryable;
        }
        #endregion
        #region GetUpdatedCreateTrackable
        public static IQueryable<TEntity> GetUpdatedCreateTrackable<TEntity>(
            this IQueryable<TEntity> queryable, string updateDateTime)
            where TEntity : class, IEntity, ICreateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            DateTime update;
            return GetUpdatedCreateTrackable(queryable, DateTime.TryParse(updateDateTime, out update) ? update : (DateTime?)null);
        }
        public static IQueryable<TEntity> GetUpdatedCreateTrackable<TEntity>(
            this IQueryable<TEntity> queryable, DateTime updateDateTime)
            where TEntity : class, IEntity, ICreateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return GetUpdatedCreateTrackable(queryable, (DateTime?)updateDateTime);
        }
        public static IQueryable<TEntity> GetUpdatedCreateTrackable<TEntity>(
            this IQueryable<TEntity> queryable, DateTime? updateDateTime)
            where TEntity : class, IEntity, ICreateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return updateDateTime.HasValue
                ? queryable
                    .Where(x => x.CreatedDateTime >= updateDateTime.Value)
                : queryable;
        }
        #endregion
        #region GetUpdatedDeleteTrackable
        public static IQueryable<TEntity> GetUpdatedDeleteTrackable<TEntity>(
            this IQueryable<TEntity> queryable, string deleteDateTime)
            where TEntity : class, IEntity, IDeleteTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            DateTime update;
            return GetUpdatedDeleteTrackable(queryable, DateTime.TryParse(deleteDateTime, out update) ? update : (DateTime?)null);
        }
        public static IQueryable<TEntity> GetUpdatedDeleteTrackable<TEntity>(
            this IQueryable<TEntity> queryable, DateTime deleteDateTime)
            where TEntity : class, IEntity, IDeleteTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return GetUpdatedDeleteTrackable(queryable, (DateTime?)deleteDateTime);
        }
        public static IQueryable<TEntity> GetUpdatedDeleteTrackable<TEntity>(
            this IQueryable<TEntity> queryable, DateTime? deleteDateTime)
            where TEntity : class, IEntity, IDeleteTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return deleteDateTime.HasValue
                ? queryable
                    .Where(x => x.DeleteDateTime >= deleteDateTime.Value)
                : queryable;
        }
        #endregion

        public static IQueryable<TEntity> GetUpdatedEntities<TEntity>(
            this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity, IUpdateTrackableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return queryable.Where(x => x.UpdateDateTime.HasValue);
        }
        #endregion

        #region Expirables
        public static IQueryable<TEntity> GetExpiredEntities<TEntity>(
            this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity, IExpirableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
           return queryable.Where(x => x.ExpirationDateTime >= DateTime.Now);
        }
        public static IQueryable<TEntity> GetNotExpiredEntities<TEntity>(
            this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity, IExpirableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return queryable.Where(x => x.ExpirationDateTime < DateTime.Now);
        }
        #endregion

        #region Hideables
        public static IQueryable<TEntity> GetHiddenEntities<TEntity>(
            this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity, IHideableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return queryable.Where(x => !x.Visible);
        }
        public static IQueryable<TEntity> GetVisibleEntities<TEntity>(
            this IQueryable<TEntity> queryable)
            where TEntity : class, IEntity, IHideableEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            return queryable.Where(x => x.Visible);
        }
        #endregion

        //#region DbCoordinates
        //public static IQueryable<TEntity> GetSortedCoordinates<TEntity>(
        //    this IQueryable<TEntity> queryable, IGeoCoordinate coordinate, SortingOrder sortOrder)
        //    where TEntity : class, IEntity, IGeoCoordinate
        //{
        //    if (queryable == null) throw new ArgumentNullException(nameof(queryable));
        //    return GetSortedCoordinates(queryable, coordinate.Latitude, coordinate.Longitude, sortOrder);
        //}
        //public static IQueryable<TEntity> GetSortedCoordinates<TEntity>(
        //    this IQueryable<TEntity> queryable, double latitude, double longitude, SortingOrder sortOrder)
        //    where TEntity : IEntity, IGeoCoordinate
        //{
        //    if (queryable == null) throw new ArgumentNullException(nameof(queryable));
        //    Expression<Func<TEntity, bool>> order = x => true;

        //    return sortOrder == SortingOrder.Ascending
        //        ? queryable.OrderBy(order)
        //        : queryable.OrderByDescending(order);
        //}
        //#endregion

        #region Pageable
        public static IQueryable<TEntity> GetPage<TEntity>(
            this IOrderedQueryable<TEntity> queryable, long pageSize, long? page)
            where TEntity : class, IEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));

            if (!page.HasValue) return queryable;

            var cPage = page.Value;

            return queryable
                .Skip((int) (cPage*pageSize))
                .Take((int) pageSize);
        }
        public static IQueryable<TEntity> GetPage<TEntity, TProperty>(
            this IQueryable<TEntity> queryable,
            SortingOrder order, Expression<Func<TEntity, TProperty>> sortByProperty,
            long pageSize, long? page)
            where TEntity : class, IEntity
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));

            if (!page.HasValue) return queryable;

            var cPage = page.Value;

            return (order == SortingOrder.Descending
                ? queryable.OrderByDescending(sortByProperty)
                : queryable.OrderBy(sortByProperty))

                .Skip((int)(cPage * pageSize))
                .Take((int)pageSize);
        }
        #endregion
    }
}