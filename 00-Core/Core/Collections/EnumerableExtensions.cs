using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture;

namespace AMKsGear.Core.Collections
{
    public static class EnumerableExtensions
    {
        #region Utils
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TElement[] AsArray<TElement>(this IEnumerable<TElement> enumerable)
        {
            if(enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            var array = enumerable as TElement[];
            if (array != null)
                return array;
            return enumerable.ToArray();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<TElement> AsList<TElement>(this IEnumerable<TElement> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            var array = enumerable as List<TElement>;
            if (array != null)
                return array;
            return enumerable.ToList();
        }
        #endregion

        #region Merge
        public static IEnumerable<TEnumerable> Merge<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            TEnumerable appendix, RelativeOrder order = RelativeOrder.After)
        {
            if (enumerable == null)
            {
                yield return appendix;
                yield break;
            }
            if (order == RelativeOrder.Before)
            {
                foreach (var element in enumerable) yield return element;
                yield return appendix;
                yield break;
            }

            yield return appendix;
            foreach (var element in enumerable)
                yield return element;
        }
        public static IEnumerable<TEnumerable> Merge<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            IEnumerable<TEnumerable> appendix, RelativeOrder order = RelativeOrder.After)
        {
            if (enumerable == null)
            {
                if (appendix == null) yield break;
                foreach (var element in appendix) yield return element;
                yield break;
            }
            if (order == RelativeOrder.Before)
            {
                foreach (var element in enumerable) yield return element;
                if (appendix != null)
                    foreach (var element in appendix) yield return element;
                yield break;
            }
            if (appendix != null)
                foreach (var element in appendix) yield return element;
            foreach (var element in enumerable) yield return element;
        }
        public static IEnumerable<TEnumerable> Merge<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            TEnumerable before, TEnumerable after)
        {
            yield return before;
            foreach (var element in enumerable) yield return element;
            yield return after;
        }
        public static IEnumerable<TEnumerable> Merge<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            IEnumerable<TEnumerable> before, IEnumerable<TEnumerable> after)
        {
            foreach (var element in before) yield return element;
            foreach (var element in enumerable) yield return element;
            foreach (var element in after) yield return element;
        }
        #endregion

        #region Distinct
        public static IEnumerable<TEnumerable> Distinct<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable, Func<TEnumerable, object> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            List<Tuple<TEnumerable, object>> retVal = new List<Tuple<TEnumerable, object>>();
            if (predicate == null) return new TEnumerable[0];

            foreach (var item in enumerable)
            {
                var obj = predicate(item);
                if (!retVal.Any(x =>
                {
                    if (!ReferenceEquals(x.Item2, null)) return x.Item2.Equals(item);
                    if (ReferenceEquals(obj, null)) return true;

                    return x.Item2?.Equals(item) ?? false;
                }))
                    retVal.Add(new Tuple<TEnumerable, object>(item, obj));
            }

            return retVal.Select(x => x.Item1);
        }
        #endregion

        #region ForEach
        public static void ForEach(
            this IEnumerable enumerable,
            Action<object> action)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var element in enumerable)
                action(element);
        }
        public static void ForEach<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            Action<TEnumerable> action)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var element in enumerable)
                action(element);
        }
        #endregion

        #region SequentialIndexOf
        public static int SequentialIndexOf<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable, Func<TEnumerable, bool> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null) return -1;
            int index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                    return index;
                index++;
            }
            return -1;
        }
        public static long SequentialLongIndexOf<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable, Func<TEnumerable, bool> predicate)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null) return -1;
            long index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                    return index;
                index++;
            }
            return -1;
        }
        #endregion

        #region OrderByEx
        public static IOrderedEnumerable<TEntity> OrderByEx<TEntity, TKey>(
            this IEnumerable<TEntity> enumerable,
            Func<TEntity, TKey> selector,
            SortingOrder order)
        {
            return order == SortingOrder.Descending
                ? enumerable.OrderByDescending(selector)
                : enumerable.OrderBy(selector);
        }
        #endregion
    }
}