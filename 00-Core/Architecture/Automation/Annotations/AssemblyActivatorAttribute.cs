using System;
using System.Reflection;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Automation.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyActivatorAttribute : OrderedAttribute
    {
        public Type AssemblyActivatorType { get; }

        public AssemblyActivatorAttribute(Type assemblyActivatorType)
        {
            if (assemblyActivatorType == null) throw new ArgumentNullException(nameof(assemblyActivatorType));

            if (!typeof (IAssemblyActivator).GetTypeInfo().IsAssignableFrom(assemblyActivatorType.GetTypeInfo()))
                throw new InvalidOperationException("AssemblyActivator requires a type of IAssemblyActivator.");

            AssemblyActivatorType = assemblyActivatorType;
        }
    }
}