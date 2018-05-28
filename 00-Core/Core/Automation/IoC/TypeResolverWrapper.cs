using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Core.Automation.IoC
{
    public class TypeResolverWrapper : ITypeResolver
    {
        private readonly ITypeResolver _resolver;
        private readonly bool _accessUnderlyingContext;

        public TypeResolverWrapper(ITypeResolver typeResolver)
        {
            _resolver = typeResolver;
            _accessUnderlyingContext = true;
        }
        public TypeResolverWrapper(ITypeResolver typeResolver, bool accessUnderlyingContext)
        {
            _resolver = typeResolver;
            _accessUnderlyingContext = accessUnderlyingContext;
        }

        public object GetUnderlyingContext() => _accessUnderlyingContext ? _resolver : null;

        public object Resolve(Type type, object context, IEnumerable<object> args) => _resolver.Resolve(type, context, args);
        public bool CanResolve(Type type, object context, IEnumerable<object> args) => _resolver.CanResolve(type, context, args);
    }
}