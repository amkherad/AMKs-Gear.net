using System;
using AMKsGear.Architecture.Annotations;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Automation.Mapper
{
    public class MapperException : Exception
    {
        [LocalizationRequired]
        private static readonly string FrameworkMessageDefaultMessage = "A mapper exception has been thrown.";
        
        
        
        public MapperException()
            : base(FrameworkMessageDefaultMessage.LocalizeFrameworkMessage())
        {
        }

        public MapperException(string message)
            : base(message)
        {
        }

        public MapperException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}