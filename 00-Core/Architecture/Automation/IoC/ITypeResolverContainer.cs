using System;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Architecture.Automation.IoC
{
    public interface ITypeMappingTypeResolverContainer : IServiceProvider, ITypeResolver, IWrapper
    {
        void RegisterType(Type type, params object[] options);
        void RegisterType(Type fromType, Type toType, params object[] options);
    }
    public interface ISingletonTypeResolverContainer : IServiceProvider, ITypeResolver, IWrapper
    {
        //void RegisterSingleton(Type from, Type to, params object[] options);
        void RegisterType(Type type, object instance, params object[] options);
        void RegisterType(Type type, ILazyValue lazyInstance, params object[] options);
    }
    public interface IFactoryTypeResolverContainer : IServiceProvider, ITypeResolver, IWrapper
    {
        void RegisterType(Type type, Func<object> factory, bool cacheInstance, params object[] options);
        void RegisterType(Type type, Func<Type, object> factory, bool cacheInstance, params object[] options);
    }
    public interface INamedMappingTypeResolverContainer : IServiceProvider, ITypeResolver, IWrapper
    {
        void RegisterType(string name, Type type, params object[] options);
        void RegisterType(string name, object instance, params object[] options);
        void RegisterType(string name, ILazyValue lazyInstance, params object[] options);
        void RegisterType(string name, Func<object> factory, params object[] options);
        void RegisterType(string name, Func<Type, object> factory, params object[] options);
    }
    public interface ITypeResolverAppliableContainer : IServiceProvider, ITypeResolver, IWrapper
    {
        void RegisterApplier(Type type, object applier);
    }
    public interface ITypeResolverContainer :
        ITypeMappingTypeResolverContainer,
        ISingletonTypeResolverContainer,
        IFactoryTypeResolverContainer,
        ITypeResolverAppliableContainer
    { }
}