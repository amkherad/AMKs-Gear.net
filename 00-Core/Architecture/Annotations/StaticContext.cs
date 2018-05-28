using System;
using AMKsGear.Architecture.Automation;

namespace AMKsGear.Architecture.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = true)]
    public class StaticContextAttribute : Attribute
    {
        public Type Type { get; protected set; }

        public StaticContextAttribute(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (type != typeof (IActionWrapper)) throw new InvalidOperationException("Parameter 'type' must be of type IActionWrapper.");
            Type = type;

            Invoke();
        }

        public void Invoke()
        {
            
        }
    }
}