using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.IoC
{
    public class CompiledTypeResolverContainer : ITypeResolverContainer, INamedMappingTypeResolverContainer
    {
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public object GetUnderlyingContext()
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

        public void RegisterApplier(Type type, object applier)
        {
            throw new NotImplementedException();
        }

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

        public void RegisterType(string name, Type type, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(string name, object instance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(string name, ILazyValue lazyInstance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(string name, Func<object> factory, params object[] options)
        {
            throw new NotImplementedException();
        }

        public void RegisterType(string name, Func<Type, object> factory, params object[] options)
        {
            throw new NotImplementedException();
        }
    }
}