using System;
using System.Collections;
using System.Collections.Generic;

namespace AMKsGear.Core.Trace.Tools
{
    public class TraceInfoRecord
    {
        public string Message { get; }
        public Exception Exception { get; }

        public TraceInfoRecord(string message)
        {
            Message = message;
        }
    }

    public class TraceInfo : ICollection<TraceInfoRecord>
    {
        private readonly List<TraceInfoRecord> _collection;

        public TraceInfo()
        {
            _collection = new List<TraceInfoRecord>();
        }
        public TraceInfo(IEnumerable<TraceInfoRecord> collection)
        {
            if (collection == null)
            {
                _collection = new List<TraceInfoRecord>();
            }
            else
            {
                foreach(var record in collection)
                    if (record == null) throw new InvalidOperationException();

                _collection = new List<TraceInfoRecord>(collection);
            }
        }
        public TraceInfo(int capacity)
        {
            _collection = new List<TraceInfoRecord>(capacity);
        }

        public IEnumerator<TraceInfoRecord> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

        public void Add(TraceInfoRecord item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _collection.Add(item);
        }

        public void Clear() => _collection.Clear();
        public bool Contains(TraceInfoRecord item) => _collection.Contains(item);
        public void CopyTo(TraceInfoRecord[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);
        public bool Remove(TraceInfoRecord item) => _collection.Remove(item);

        public int Count => _collection.Count;
        public bool IsReadOnly => ((ICollection<TraceInfoRecord>) _collection).IsReadOnly;
    }
}