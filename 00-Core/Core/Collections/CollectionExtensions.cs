using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AMKsGear.Core.Collections
{
    public static class CollectionExtensions
    {
        #region SyncCollectionWith

        #region SyncCollectionWith Auto

        public static void SyncCollectionWith<T>(this ICollection<T> collection,
            ICollection<T> source,
            Func<T, T, bool> comparer,
            bool distinctAdd = false)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            var deleteds = collection.Where(attach => source.All(x => !comparer(attach, x))).ToList();
            foreach (var remove in deleteds) collection.Remove(remove);
            var newOnes = new List<T>();
            if (distinctAdd)
            {
                foreach (var attach in source)
                    if (collection.All(x => !comparer(x, attach)) &&
                        newOnes.All(x => !comparer(x, attach)))
                        newOnes.Add(attach);
            }
            else
            {
                foreach (var attach in source)
                    if (collection.All(x => !comparer(x, attach)))
                        newOnes.Add(attach);
            }

            foreach (var attach in newOnes)
                collection.Add(attach);
        }

        public static void SyncCollectionWith<T1, T2>(this ICollection<T1> collection,
            ICollection<T2> source,
            Func<T1, T2, bool> comparer,
            Func<T2, T1> converter,
            bool distinctAdd = false)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            if (converter == null) throw new ArgumentNullException(nameof(converter));

            var deleteds = collection.Where(attach => source.All(x => !comparer(attach, x))).ToList();
            foreach (var remove in deleteds) collection.Remove(remove);
            var newOnes = new List<T1>();
            if (distinctAdd)
            {
                foreach (var attach in source)
                    if (collection.All(x => !comparer(x, attach)) &&
                        newOnes.All(x => !comparer(x, attach)))
                        newOnes.Add(converter(attach));
            }
            else
            {
                foreach (var attach in source)
                    if (collection.All(x => !comparer(x, attach)))
                        newOnes.Add(converter(attach));
            }

            foreach (var attach in newOnes)
                collection.Add(attach);
        }

        #endregion

        #region SyncCollectionWith EX

        public static void SyncCollectionWith<T>(this ICollection<T> collection,
            ICollection<T> source,
            Func<T, T, bool> comparer,
            Action<ICollection<T>, T> adder,
            Action<ICollection<T>, T> remover)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            var deleteds = collection.Where(attach => source.All(x => !comparer(attach, x))).ToList();
            foreach (var remove in deleteds) remover(collection, remove); //collection.Remove(remove);
            foreach (var attach in source)
                if (collection.All(x => !comparer(x, attach)))
                    adder(collection, attach); //collection.Add(attach);
        }

        #endregion

        #endregion


        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var element in elements)
                collection.Add(element);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (var element in elements)
                collection.Add(element);
        }

        
//        public static void RemoveAll<T>(this ICollection<T> collection, IEnumerable<T> elements)
//        {
//            foreach (var i in elements)
//                collection.Remove(i);
//        }


        public static void RemoveAll<T>(this List<T> collection, IEnumerable<T> elements)
        {
            var comparer = EqualityComparer<T>.Default;
            collection.RemoveAll(i => elements.Any(e => comparer.Equals(i, e)));
        }

        public static void RemoveAll<T>(this List<T> collection, IEnumerable<T> elements, IEqualityComparer<T> comparer)
        {
            collection.RemoveAll(i => elements.Any(e => comparer.Equals(i, e)));
        }
    }
}