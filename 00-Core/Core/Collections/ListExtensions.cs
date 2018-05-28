using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public static class ListExtensions
    {
        public static bool AddDistinct(this ICollection collection, object item)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (item == null) return false;
            var list = collection as IList;
            if (list != null)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    return true;
                }
            }
            else
            {
                throw new NotSupportedException();
                //var comparer = EqualityComparer<object>.Default;
                //foreach(var row in collection)
                //    if (comparer.Equals(item, row))
                //        return false;
                //collection.
            }
            return false;
        }
        public static bool AddDistinct<TElement>(this ICollection<TElement> collection, TElement item)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (item == null) return false;
            var list = collection as IList<TElement>;
            if (list != null)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    return true;
                }
            }
            else
            {
                var comparer = EqualityComparer<object>.Default;
                foreach (var row in collection)
                    if (comparer.Equals(item, row))
                        return false;
                collection.Add(item);
                return true;
            }
            return false;
        }
    }
}