using System;
using AMKsGear.Architecture.Automation.IoC;

namespace AMKsGear.Core.Automation.IoC.Builder
{
    public static class TypeResolverRegistrarExtensions
    {
        public static SingletonBuilder RegisterSingleton(this ISingletonTypeResolverContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var singleton = new SingletonBuilder(container);
            return singleton;
        }
        public static SingletonBuilder RegisterTransient(this ISingletonTypeResolverContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var singleton = new SingletonBuilder(container);
            return singleton;
        }
        public static SingletonBuilder RegisterScoped(this ISingletonTypeResolverContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var singleton = new SingletonBuilder(container);
            return singleton;
        }
    }
}