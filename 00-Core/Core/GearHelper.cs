using System;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Framework;

namespace AMKsGear.Core
{
    internal static class GearHelper
    {
        public static IEnumerable<T> OrderHelper<T, TKey>(this IEnumerable<T> source,
            Func<T, TKey> orderExpression, SortingOrder order)
        {
            var result = (order == SortingOrder.Descending
                ? source.OrderByDescending(orderExpression)
                : source.OrderBy(orderExpression)).ToList();

            var superPriorities = result.Where(x => x is IFxSuperPriority).ToList();
            result.RemoveAll(x => x is IFxSuperPriority);
            result.InsertRange(0, superPriorities);

            return result;
        }
        public static IEnumerable<T> PriorityHelper<T>(this IEnumerable<T> source)
        {
            var result = source.ToList();

            var superPriorities = result.Where(x => x is IFxSuperPriority).ToList();
            result.RemoveAll(x => x is IFxSuperPriority);
            result.InsertRange(0, superPriorities);

            return result;
        }
    }
}