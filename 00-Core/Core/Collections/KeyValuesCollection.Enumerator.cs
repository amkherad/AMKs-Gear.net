using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public partial class KeyValuesCollection<TKey, TValue>
    {
        protected class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private readonly KeyValuesCollection<TKey, TValue> _parent;
            private int _currentIndex = 0;

            private int _currentSubIndex = -1;
            //private TKey _currentKey;


            public Enumerator(KeyValuesCollection<TKey, TValue> collection)
            {
                _parent = collection;
            }

            public void Dispose()
            {
                //_parent = null;
                _currentIndex = 0;
                _currentSubIndex = 0;
            }

            public bool MoveNext()
            {
                if (_parent.Count == 0 || _parent.Count <= _currentIndex)
                {
                    return false;
                }

                ++_currentSubIndex;

                var col = _parent.GetEntryByIndex(_currentIndex);
                while (col.Count == 0 || col.Count <= _currentSubIndex)
                {
                    _currentSubIndex = 0;
                    ++_currentIndex;
                    if (_parent.Count <= _currentIndex)
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Reset()
            {
                _currentIndex = 0;
                _currentSubIndex = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    var entry = _parent.GetEntryByIndex(_currentIndex);
                    return new KeyValuePair<TKey, TValue>(entry.Key, entry[_currentSubIndex]);
                }
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    var entry = _parent.GetEntryByIndex(_currentIndex);
                    return new KeyValuePair<TKey, TValue>(entry.Key, entry[_currentSubIndex]);
                }
            }
        }
    }
}