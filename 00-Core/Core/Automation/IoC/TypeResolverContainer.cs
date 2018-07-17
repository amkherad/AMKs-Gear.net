using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;
using AMKsGear.Core.Collections;

namespace AMKsGear.Core.Automation.IoC
{
    public class TypeResolverContainer :
        ITypeResolverContainer,
        INamedMappingTypeResolverContainer,
        ITypeResolverContainerMetadataExporter
    {
        protected readonly TypeResolverTypeMapping Mappings;

        public TypeResolverContainer() { Mappings = new TypeResolverTypeMapping(); }

        public virtual bool ResolveDefaults { get { return Mappings.ResolveDefaults; } set { Mappings.ResolveDefaults = value; } }
        public virtual TypeMappingCacheMode CacheMode { get { return Mappings.CacheMode; } set { Mappings.CacheMode = value; } }

        public virtual object GetUnderlyingContext() => Mappings;
        
        public virtual object Resolve(Type type, object context, IEnumerable<object> args)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return Mappings.CreateInstance(this, type, args?.AsArray());
        }
        public virtual bool CanResolve(Type type, object context, IEnumerable<object> args)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return Mappings.CanResolve(this, type, args?.AsArray());
        }
        
        public virtual object GetService(Type serviceType) => Resolve(serviceType, null, null);
        
        public virtual void RegisterType(Type type, params object[] options)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            Mappings.RegisterType(type, options);
        }
        public virtual void RegisterType(Type fromType, Type toType, params object[] options)
        {
            if (fromType == null) throw new ArgumentNullException(nameof(fromType));
            if (toType == null) throw new ArgumentNullException(nameof(toType));
            Mappings.RegisterType(fromType, toType, options);
        }
        public virtual void RegisterType(Type type, Func<object> factory, bool cacheInstance, params object[] options)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            Mappings.RegisterType(type, factory, cacheInstance, options);
        }
        public virtual void RegisterType(Type type, Func<Type, object> factory, bool cacheInstance, params object[] options)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            Mappings.RegisterType(type, factory, cacheInstance, options);
        }

        public virtual void RegisterType(Type type, object instance, params object[] options)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (instance == null) throw new ArgumentNullException(nameof(instance));
            Mappings.RegisterType(type, instance, options);
        }
        public virtual void RegisterType(Type type, ILazyValue lazyInstance, params object[] options)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (lazyInstance == null) throw new ArgumentNullException(nameof(lazyInstance));
            Mappings.RegisterType(type, lazyInstance, options);
        }

        public virtual bool Exists(Type type) => Mappings.TypeExists(type);

        #region Named
        public virtual bool Exists(string name) => Mappings.NameExists(name);

        public virtual void RegisterType(string name, Type type, params object[] options)
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterType(string name, object instance, params object[] options)
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterType(string name, ILazyValue lazyInstance, params object[] options)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (Exists(name)) throw new InvalidOperationException("Name already exists.");
            if (lazyInstance == null) throw new ArgumentNullException(nameof(lazyInstance));
            var result = Mappings.RegisterType(null, lazyInstance, options, addToList: false);
            //result.StrongName = name;
            //result.BindingStrongName = name;
            result.BindingNames.Add(name);
            Mappings.RegisterContext(result);
        }

        public virtual void RegisterType(string name, Func<object> factory, params object[] options)
        {
            throw new NotImplementedException();
        }

        public virtual void RegisterType(string name, Func<Type, object> factory, params object[] options)
        {
            throw new NotImplementedException();
        }
        #endregion
        
        public void RegisterApplier(Type type, object applier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (applier == null) throw new ArgumentNullException(nameof(applier));

            var typeResolverApplier = applier as ITypeResolverApplier;
            if (typeResolverApplier == null)
                throw new InvalidOperationException(
                    Localization.Format<ITypeResolverLocalization, DefaultTypeResolverLocalization>(
                        x => x.UnknownTypeForEngine, type.Name)
                    );
            
            Mappings.RegisterApplier(type, typeResolverApplier);
        }
    }
}