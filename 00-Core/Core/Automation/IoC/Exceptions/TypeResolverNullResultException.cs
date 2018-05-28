using System;

namespace AMKsGear.Core.Automation.IoC.Exceptions
{
    public class TypeResolverNullResultException : TypeResolvingException
    {
        public TypeResolverNullResultException() : base("Type resolver returned null.") { }
        public TypeResolverNullResultException(string message) : base(message) { }
        public TypeResolverNullResultException(string message, Exception innerException) : base(message, innerException) { }
    }
}
