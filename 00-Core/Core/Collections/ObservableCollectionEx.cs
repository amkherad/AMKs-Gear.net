using System.Collections.Generic;
using System.Collections.Specialized;

namespace AMKsGear.Core.Collections
{
    public class ObservableCollectionEx<T> : System.Collections.ObjectModel.ObservableCollection<T>, IObservableCollection<T>
    {
        public ObservableCollectionEx() : base() { }
        public ObservableCollectionEx(T first, IEnumerable<T> collection) : base(collection) { if (first != null) base.InsertItem(0, first); }
        public ObservableCollectionEx(IEnumerable<T> collection) : base(collection) { }
        public ObservableCollectionEx(T first, List<T> list) : base(list) { base.InsertItem(0, first); }
        public ObservableCollectionEx(List<T> list) : base(list) { }

        public new virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
        }
    }
}