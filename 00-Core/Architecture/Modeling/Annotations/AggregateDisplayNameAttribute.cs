using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    public class AggregateDisplayNameAttribute : Attribute
    {
        public string DisplayName { get; protected set; }
        
        public AggregateDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}