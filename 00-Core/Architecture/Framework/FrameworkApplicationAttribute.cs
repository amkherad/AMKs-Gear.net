using System;
using System.Runtime.InteropServices;

namespace AMKsGear.Architecture.Framework
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    [ComVisible(true)]
    public class FrameworkApplicationAttribute : Attribute
    {
        public string ApplicationId { get; }

        public FrameworkApplicationAttribute()
        {
            ApplicationId = $"This app is using {FrameworkInfo.Name}";
        }
        public FrameworkApplicationAttribute(string assemblyId)
        {
            ApplicationId = $"{assemblyId} is using {FrameworkInfo.Name}";
        }
    }
}