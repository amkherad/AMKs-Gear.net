using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public partial class KeyValuesCollection<TKey, TValue>
    {
        protected class Entry : List<TValue>
        {
            public TKey Key { get; }

            public Entry(TKey key)
            {
                Key = key;
            }

            public Entry(TKey key, IEnumerable<TValue> values)
                : base(values)
            {
                Key = key;
            }
        }
    }
}