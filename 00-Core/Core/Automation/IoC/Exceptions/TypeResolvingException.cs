using System;

namespace AMKsGear.Core.Automation.IoC.Exceptions
{
    public class TypeResolvingException : InvalidOperationException
    {
        public TypeResolvingException() : base("An error occurred when trying to resolve a type.") { }
        public TypeResolvingException(string message) : base(message) { }
        public TypeResolvingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
