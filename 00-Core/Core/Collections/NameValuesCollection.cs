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
}