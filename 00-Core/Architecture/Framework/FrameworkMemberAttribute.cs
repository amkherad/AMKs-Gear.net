using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AMKsGear.Architecture.Framework
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    [ComVisible(true)]
    internal class FrameworkMemberAttribute : Attribute
    {
        public string AssemblyId { get; }

        public FrameworkMemberAttribute(string assemblyId)
        {
            AssemblyId = assemblyId;
        }
    }

    public static class FrameworkAssemblyExtensions
    {
        public static string GetFrameworkAssemblyId(this Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(FrameworkMemberAttribute)).ToList();
            if (attributes.Count == 0) throw new InvalidOperationException("Only available on assemblies with FrameworkMemberAttribute.");
            var titleAttribute = (FrameworkMemberAttribute)attributes[0];
            return titleAttribute.AssemblyId;
        }
    }
}