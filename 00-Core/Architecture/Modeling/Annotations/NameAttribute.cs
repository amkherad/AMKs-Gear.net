using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    public class NameAttribute : Attribute
    {
        public string Name { get; }

        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}