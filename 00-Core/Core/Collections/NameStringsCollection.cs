using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
    public class NameStringsCollection : NameValuesCollection<string>
    {
        public NameStringsCollection()
        {
        }
        public NameStringsCollection(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        public NameStringsCollection(int capacity)
            : base(capacity)
        {
        }

        public NameStringsCollection(int capacity, IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
        }

        public NameStringsCollection(IEnumerable<KeyValuePair<string, string>> values)
            : base(values)
        {
        }

        public NameStringsCollection(IDictionary<string, string> values)
            : base(values)
        {
        }
    }
}