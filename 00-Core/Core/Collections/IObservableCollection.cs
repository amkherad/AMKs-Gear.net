using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AMKsGear.Core.Collections
{
    public interface IObservableCollection<T> : IList<T>, ICollection<T>, IReadOnlyList<T>, IReadOnlyCollection<T>, IEnumerable<T>, IObservableCollection
    { }

    public interface IObservableCollection : IList, ICollection, IEnumerable, INotifyCollectionChanged
    {
        //void OnCollectionChanged(NotifyCollectionChangedEventArgs e);
    }
}