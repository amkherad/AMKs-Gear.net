using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture;

namespace AMKsGear.Core.Collections
{
    /// <summary>
    /// Some extended extension on <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> as an array; if enumerable isn't an array, it will call <c>Enumerable.ToArray()</c>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="TElement"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TElement[] AsArray<TElement>(this IEnumerable<TElement> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            if (enumerable is TElement[] array)
            {
                return array;
            }

            return enumerable.ToArray();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> as a list; if enumerable isn't of list type, it will call <c>Enumerable.ToList()</c>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="TElement"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<TElement> AsList<TElement>(this IEnumerable<TElement> enumerable)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));

            if (enumerable is List<TElement> array)
            {
                return array;
            }

            return enumerable.ToList();
        }

        /// <summary>
        /// Returns an <see cref="IDictionary{TKey,TValue}"/> as a dictionary; if dictionary isn't of <see cref="Dictionary{TKey,TValue}"/> type, it will call <c>new Dictionary&lt;TKey, TValue&gt;(dictionary)</c>.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey,TValue> AsDictionary<TKey,TValue>(this IDictionary<TKey,TValue> dictionary)
        {
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            if (dictionary is Dictionary<TKey,TValue> dict)
            {
                return dict;
            }

            return new Dictionary<TKey, TValue>(dictionary);
        }


        /// <summary>
        /// Merges an <see cref="IEnumerable{T}"/> with a single item.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="appendix"></param>
        /// <param name="order"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Merges two <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="appendix"></param>
        /// <param name="order"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
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
                    foreach (var element in appendix)
                        yield return element;
                yield break;
            }

            if (appendix != null)
                foreach (var element in appendix)
                    yield return element;
            foreach (var element in enumerable) yield return element;
        }

        /// <summary>
        /// Yield returns an item before the enumerable.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TEnumerable> Merge<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            TEnumerable before, TEnumerable after)
        {
            yield return before;
            foreach (var element in enumerable) yield return element;
            yield return after;
        }

        /// <summary>
        /// Yield return an item after the enumerable.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TEnumerable> Merge<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            IEnumerable<TEnumerable> before, IEnumerable<TEnumerable> after)
        {
            foreach (var element in before) yield return element;
            foreach (var element in enumerable) yield return element;
            foreach (var element in after) yield return element;
        }


        /// <summary>
        /// Distinct
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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


        /// <summary>
        /// Applies an action on all of the enumerable items
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach(
            this IEnumerable enumerable,
            Action<object> action)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var element in enumerable)
                action(element);
        }

        /// <summary>
        /// Applies an action on all of the enumerable items
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ForEach<TEnumerable>(
            this IEnumerable<TEnumerable> enumerable,
            Action<TEnumerable> action)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (action == null) throw new ArgumentNullException(nameof(action));
            foreach (var element in enumerable)
                action(element);
        }


        /// <summary>
        /// Returns the index of an item in an enumerable.
        /// </summary>
        /// <remarks>
        /// NOTE: The index is not guaranteed if the source collection doe's not have any indexing mechanism. (It will simply count the items to target item)
        /// </remarks>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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

        /// <summary>
        /// Returns the index of an item in an enumerable.
        /// </summary>
        /// <remarks>
        /// NOTE: The index is not guaranteed if the source collection doe's not have any indexing mechanism. (It will simply count the items to target item)
        /// </remarks>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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


        /// <summary>
        /// Provides ordering of an enumerable using <see cref="SortingOrder"/> enum.
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="selector"></param>
        /// <param name="order"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<TEntity> OrderBy<TEntity, TKey>(
            this IEnumerable<TEntity> enumerable,
            Func<TEntity, TKey> selector,
            SortingOrder order)
        {
            return order == SortingOrder.Descending
                ? enumerable.OrderByDescending(selector)
                : enumerable.OrderBy(selector);
        }
    }
}