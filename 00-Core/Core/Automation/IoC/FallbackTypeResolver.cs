using System;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Automation.IoC
{
    public class FallbackTypeResolver : ITypeResolver, IServiceProvider
    {
        public object GetUnderlyingContext() => null;
        private readonly IList<ITypeResolver> _resolvers;

        public FallbackTypeResolver() { _resolvers = new List<ITypeResolver>(); }
        public FallbackTypeResolver(IEnumerable<ITypeResolver> typeResolvers)
        {
            _resolvers = new List<ITypeResolver>();
            foreach (var tr in typeResolvers)
                _resolvers.Add(tr);
        }

        public void AppendTypeResolver(ITypeResolver typeResolver) => _resolvers.Add(typeResolver);
        public void PrependTypeResolver(ITypeResolver typeResolver) => _resolvers.Insert(0, typeResolver);

        public object GetService(Type serviceType) => Resolve(serviceType, null, null);
        public object Resolve(Type type, object context, IEnumerable<object> args)
        {
            foreach (var tr in _resolvers)
            {
                var resolved = tr.Resolve(type, context, args);
                if (resolved != null)
                    return resolved;
            }
            return null;
        }
        
        public bool CanResolve(Type type, object context, IEnumerable<object> args) => _resolvers.Any(tr => tr.CanResolve(type, context, args));
    }
}