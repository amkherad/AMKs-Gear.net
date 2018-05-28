using System;
using System.Collections.Generic;

namespace AMKsGear.Architecture.Annotations
{
    public class ThrowsAttribute : Attribute
    {
        private readonly List<Type> _throws = new List<Type>();

        public ThrowsAttribute()
        {

        }
        public ThrowsAttribute(IEnumerable<Type> types)
        {
            if (types != null)
                _throws.AddRange(types);
        }
        public ThrowsAttribute(params Type[] types)
        {
            if (types != null)
                _throws.AddRange(types);
        }

        public IEnumerable<Type> Throws => _throws;
    }
}