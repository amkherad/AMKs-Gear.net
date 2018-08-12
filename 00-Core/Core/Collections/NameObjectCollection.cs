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
    }
}