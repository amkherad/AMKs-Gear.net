using System;

namespace AMKsGear.Architecture.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class KeepAssemblyAliveAttribute : Attribute
    {
        public Type Type { get; }

        public KeepAssemblyAliveAttribute(Type type)
        {
            Type = type;
        }
    }
}