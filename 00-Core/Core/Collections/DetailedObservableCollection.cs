using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
//using AMKsGear.Architecture.Framework.Legacy;

namespace AMKsGear.Core.Collections
{
    public abstract class DetailedObservableCollection<TElement> : ICloneable, IList<TElement>, ICollection<TElement>, IEnumerable<TElement>, IList, ICollection, IEnumerable, IObservableCollection<TElement>
    {
        private IDetailedObservableCollectionObserver<TElement> _observer;
        private readonly List<TElement> _members = new List<TElement>();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected DetailedObservableCollection() { _observer = null; }
        protected DetailedObservableCollection(IDetailedObservableCollectionObserver<TElement> observer)
        {
            _observer = observer;
        }

        protected IDetailedObservableCollectionObserver<TElement> Observer
        {
            get { return _observer; }
            set { _observer = value; }
        }

        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs eventArgs) => CollectionChanged?.Invoke(this, eventArgs);

        #region Collection Members
        #region Mutable Collection Members
        public virtual void Add(TElement item)
        {
            if (_observer != null)
                if (!_observer.ValidateAddItem(this, item))
                    throw new InvalidOperationException("Adding this element to collection is invalid.");
            _members.Add(item);
            _observer?.Add(this, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item));
        }
        public virtual void AddRange(IEnumerable<TElement> items)
        {
            var elements = items;
            if (_observer != null)
                if (!_observer.ValidateAddItems(this, items, out elements))
                    throw new InvalidOperationException("Adding these elements to collection is invalid.");
            if (elements == null) throw new InvalidOperationException("ValidateAddItems returned a null enumerable list.");
            _members.AddRange(elements);
            _observer?.AddRange(this, elements);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, items));
        }
        public void Clear()
        {
            if (_observer != null)
                if (!_observer.ValidateClearItems(this))
                    throw new InvalidOperationException("Clearing elements from the collection have failed.");
            var items = _members.ToArray();
            _members.Clear();
            _observer?.Clear(this, items);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Reset, items));
        }

        public void Insert(int index, TElement item)
        {
            if (_observer != null)
                if (!_observer.ValidateAddItem(this, item))
                    throw new InvalidOperationException("Adding this element to the collection is invalid.");
            _members.Insert(index, item);
            _observer?.Insert(this, index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Add, item, index));
        }

        public bool Remove(TElement item)
        {
            if (_observer != null)
                if (!_observer.ValidateRemoveItem(this, item))
                    throw new InvalidOperationException("Removing element from the collection have failed.");
            bool val = _members.Remove(item);
            if (val)
                _observer?.Remove(this, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, item));
            return val;
        }

        public void RemoveAt(int index)
        {
            var item = _members[index];
            if (_observer != null)
                if (!_observer.ValidateRemoveItem(this, item))
                    throw new InvalidOperationException("Removing element from the collection have failed.");
            _members.RemoveAt(index);
            _observer?.Remove(this, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Remove, item, index));
        }
        #endregion
        #region Immutable Collection Members
        public void Insert(int index, object value) { Insert(index, (TElement)value); }
        public void Remove(object value) { Remove((TElement)value); }
        int IList.Add(object value)
        {
            var count = _members.Count;
            Add((TElement)value);
            return count;
        }

        object IList.this[int index]
        {
            get { return _members[index]; }
            set { throw new InvalidOperationException(); }
        }
        public TElement this[int index]
        {
            get { return _members[index]; }
            set { throw new InvalidOperationException(); }
        }

        public object Clone() => new List<TElement>(_members);
        public IEnumerator<TElement> GetEnumerator() => _members.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _members.GetEnumerator();
        public bool Contains(object value) => _members.Contains((TElement)value);
        public bool Contains(TElement item) => _members.Contains(item);
        public int IndexOf(object value) => _members.IndexOf((TElement)value);
        public int IndexOf(TElement item) => _members.IndexOf(item);
        bool ICollection<TElement>.IsReadOnly => IsReadOnly;
        public virtual bool IsReadOnly => false;
        public virtual bool IsFixedSize => false;

        public void CopyTo(TElement[] array, int arrayIndex) => _members.CopyTo(array, arrayIndex);
        public void CopyTo(Array array, int index) => ((ICollection) _members).CopyTo(array, index);

        int ICollection<TElement>.Count => _members.Count;
        int ICollection.Count => _members.Count;
        public int Count => _members.Count;
        public object SyncRoot => ((ICollection)_members).SyncRoot;

        public bool IsSynchronized => false;
        #endregion
        #endregion
    }

    public interface IDetailedObservableCollectionObserver<TElement>
    {
        void Add(DetailedObservableCollection<TElement> sender, TElement element);
        void AddRange(DetailedObservableCollection<TElement> sender, IEnumerable<TElement> elements);
        void Insert(DetailedObservableCollection<TElement> sender, int index, TElement element);
        void Remove(DetailedObservableCollection<TElement> sender, TElement element);
        void Clear(DetailedObservableCollection<TElement> sender, IEnumerable<TElement> elements);
        bool ValidateAddItem(DetailedObservableCollection<TElement> sender, TElement element);
        bool ValidateAddItems(DetailedObservableCollection<TElement> sender, IEnumerable<TElement> elements, out IEnumerable<TElement> enumeratedElements);
        bool ValidateRemoveItem(DetailedObservableCollection<TElement> sender, TElement element);
        bool ValidateClearItems(DetailedObservableCollection<TElement> sender);
    }
}
