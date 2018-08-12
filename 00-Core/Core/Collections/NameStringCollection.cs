using System.Collections.Generic;

namespace AMKsGear.Core.Collections
{
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