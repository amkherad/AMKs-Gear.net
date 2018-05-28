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
    }

    public class NameObjectCollection : NameValuesCollection<object>
    {
        public NameObjectCollection()
        {
        }
        public NameObjectCollection(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }
    }

    public class NameStringCollection : NameValuesCollection<string>
    {
        public NameStringCollection()
        {
        }
        public NameStringCollection(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }
    }
}