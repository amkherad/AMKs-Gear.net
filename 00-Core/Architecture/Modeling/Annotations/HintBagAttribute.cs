using System;

namespace AMKsGear.Architecture.Modeling.Annotations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class HintBagAttribute : Attribute
    {
        public string Name { get; }
        public string Value { get; }

        public HintBagAttribute() { }
        public HintBagAttribute(string name, string value)
        {
            Value = value;
            Name = name;
        }
    }
}