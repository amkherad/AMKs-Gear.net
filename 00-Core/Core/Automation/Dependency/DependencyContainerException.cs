using System;
using AMKsGear.Architecture.Annotations;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Automation.Dependency
{
    public class DependencyContainerException : Exception
    {
        [LocalizationRequired]
        private static readonly string FrameworkMessageDefaultMessage = "A dependency exception has been thrown.";
        
        
        
        public DependencyContainerException()
            : base(FrameworkMessageDefaultMessage.LocalizeFrameworkMessage())
        {
        }

        public DependencyContainerException(string message)
            : base(message)
        {
        }

        public DependencyContainerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}