using System;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.IoC.Builder
{
    public class SingletonBuilder : IFluentConfigurator
    {
        protected bool Closed = false;
        
        internal readonly ISingletonTypeResolverContainer Container;
        public string StrongName { get; protected set; }

        public SingletonBuilder(ISingletonTypeResolverContainer container)
        {
            Container = container;
        }

        public SingletonBuilder WithStrongName(string name)
        {
            if (Closed) throw new InvalidOperationException();
            StrongName = name;
            return this;
        }
        
        public SingletonBuilder<TFor> For<TFor>()
        {
            if (Closed) throw new InvalidOperationException();
            var singleton = new SingletonBuilder<TFor>(Container)
            {
                ForType = typeof (TFor)
            };
            return singleton;
        }
    }
    
    public class SingletonBuilder<TFor> : SingletonBuilder, IFluentConfiguratorSealing
    {
        public Type ForType { get; protected internal set; }

        protected object Instance;
        protected ILazyValue LazyValue;
        protected Type Type;

        public SingletonBuilder(ISingletonTypeResolverContainer container)
            : base(container)
        {
        }

        public SingletonBuilder<TFor> With(object instance)
        {
            if (Closed) throw new InvalidOperationException();

            Instance = instance;

            return this;
        }
        public SingletonBuilder<TFor> With(ILazyValue lazyValue)
        {
            if (Closed) throw new InvalidOperationException();

            LazyValue = lazyValue;

            return this;
        }
        public SingletonBuilder<TFor> To<TType>()
        {
            if (Closed) throw new InvalidOperationException();

            Type = typeof(TType);

            return this;
        }
        public SingletonBuilder<TFor> To(Type type)
        {
            if (Closed) throw new InvalidOperationException();
            
            Type = type;

            return this;
        }
        
        public void Seal()
        {
            if (Instance != null)
            {
                Container.RegisterType(ForType, Instance);
            }
            else if (Type != null)
            {
                Container.RegisterSingleton(ForType, Type);
            }
            else if (LazyValue != null)
            {
                Container.RegisterType(ForType, LazyValue);
            }
            Closed = true;
        }
        public void CheckAndSeal()
        {
            if (Instance != null)
            {
                if (Type != null && LazyValue != null)
                {
                    throw new InvalidOperationException();
                }
                Container.RegisterType(ForType, Instance);
            }
            else if (Type != null)
            {
                if (Instance != null && LazyValue != null)
                {
                    throw new InvalidOperationException();
                }
                Container.RegisterSingleton(ForType, Type);
            }
            else if (LazyValue != null)
            {
                if (Type != null && Instance != null)
                {
                    throw new InvalidOperationException();
                }
                Container.RegisterType(ForType, LazyValue);
            }
            Closed = true;
        }
    }
}
