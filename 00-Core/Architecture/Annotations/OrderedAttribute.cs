using System;

namespace AMKsGear.Architecture.Annotations
{
    public class OrderedAttribute : Attribute
    {
        public int Order { get; set; }

        public OrderedAttribute() { }
        public OrderedAttribute(int order)
        {
            Order = order;
        }
    }
}