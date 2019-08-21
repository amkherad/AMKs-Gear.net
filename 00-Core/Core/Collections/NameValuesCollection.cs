using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class NameValuesCollection<TValue> : KeyValuesCollection<string, TValue>
    {
        public NameValuesCollection()
        {
        }

        public NameValuesCollection(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        public NameValuesCollection(int capacity)
            : base(capacity)
        {
        }

        public NameValuesCollection(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
        }

        public NameValuesCollection(IEnumerable<KeyValuePair<string, TValue>> values)
            : base(values)
        {
        }

        public NameValuesCollection(IDictionary<string, TValue> values)
            : base(values)
        {
        }
    }
}