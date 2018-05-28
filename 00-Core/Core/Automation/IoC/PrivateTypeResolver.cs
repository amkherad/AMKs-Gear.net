using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.IoC
{
    public class PrivateTypeResolver : ITypeResolverContainer
    {
        public object GetUnderlyingContext() => null;
        
        public object Resolve(Type type)
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type, object context, IEnumerable<object> args)
        {
            throw new NotImplementedException();
        }

        public bool CanResolve(Type type, object context, IEnumerable<object> args)
        {
            throw new NotImplementedException();
        }

        public object GetService(Type serviceType) => Resolve(serviceType);
        public void RegisterType(Type type, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(Type fromType, Type toType, params object[] options)
        {
            throw new NotImplementedException();
        }
        
        public void RegisterType(Type type, object instance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(Type type, ILazyValue lazyInstance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(Type type, Func<object> factory, bool cacheInstance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(Type type, Func<Type, object> factory, bool cacheInstance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterApplier(Type type, object applier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (applier == null) throw new ArgumentNullException(nameof(applier));

            var typeResolverApplier = applier as ITypeResolverApplier;
            if (typeResolverApplier == null) throw new InvalidOperationException();


        }
    }
}