using System;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.IoC.Builder
{
    public class TransientBuilder : IFluentConfigurator
    {
        protected bool Closed = false;

        internal readonly ITypeResolverContainer Container;
        public string StrongName { get; protected set; }

        public TransientBuilder(ITypeResolverContainer container)
        {
            Container = container;
        }

        public TransientBuilder WithStrongName(string name)
        {
            if (Closed) throw new InvalidOperationException();
            StrongName = name;
            return this;
        }

        public TransientBuilder<TFor> For<TFor>()
        {
            if (Closed) throw new InvalidOperationException();
            var singleton = new TransientBuilder<TFor>(Container)
            {
                //ForType = typeof(TFor)
            };
            return singleton;
        }
    }

    public class TransientBuilder<TFor> : TransientBuilder, IFluentConfiguratorSealing
    {
        public TransientBuilder(ITypeResolverContainer container)
            : base(container)
        {
        }

        public void Seal()
        {
            throw new NotImplementedException();
        }

        public void CheckAndSeal()
        {
            throw new NotImplementedException();
        }
    }
}