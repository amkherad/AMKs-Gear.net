using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class NameObjectCollection : NameValuesCollection<object>
    {
        public NameObjectCollection()
        {
        }
        public NameObjectCollection(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        public NameObjectCollection(int capacity)
            : base(capacity)
        {
        }

        public NameObjectCollection(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
        }

        public NameObjectCollection(IEnumerable<KeyValuePair<string, object>> values)
            : base(values)
        {
        }

        public NameObjectCollection(IDictionary<string, object> values)
            : base(values)
        {
        }
    }
}