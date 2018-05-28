using System;
using System.Linq;
using System.Linq.Expressions;
using AMKsGear.Architecture;

namespace AMKsGear.Core.Linq
{
    public static class QueryableExtensions
    {
        #region OrderByEx
        public static IOrderedQueryable<TEntity> OrderByEx<TEntity, TKey>(
            this IQueryable<TEntity> queryable,
            Expression<Func<TEntity, TKey>> selector,
            SortingOrder order)
        {
            return order == SortingOrder.Descending
                ? queryable.OrderByDescending(selector)
                : queryable.OrderBy(selector);
        }
        #endregion

        //public static Task<List<TElement>> ToListAsync<TElement>(this IQueryable<TElement> queryable)
        //{ return Task.Run(() => queryable.ToList()); }
        //public static Task<TElement[]> ToArrayAsync<TElement>(this IQueryable<TElement> queryable)
        //{ return Task.Run(() => queryable.ToArray()); }
    }
}