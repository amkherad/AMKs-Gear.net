using System;
using System.Threading;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Automation.IoC
{
    public interface ILazyObjectProvider : ILazyValue
    {
        bool IsValueCreated { get; }
    }

    public class Lazy<T> : Lazy, ILazyObjectProvider, ILazyValue<T>, ILazyValue
//System.Lazy<T>, 
    {
        public Lazy()
            : base(typeof(T))
        { }

        public new T GetValue() => (T)base.GetValue();

        //public Lazy() : base() { }
        //public Lazy(bool isThreadSafe) : base(isThreadSafe) { }
        //public Lazy(Func<T> valueFactory) : base(valueFactory) { }
        //public Lazy(LazyThreadSafetyMode mode) : base(mode) { }
        //public Lazy(Func<T> valueFactory, bool isThreadSafe) : base(valueFactory, isThreadSafe) { }
        //public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode) : base(valueFactory, mode) { }
        //
        //object ILazyValue.GetValue() => Value;
        //public T GetValue() => Value;
    }
    public class Lazy : ILazyObjectProvider, ILazyValue
    {
        public Type Type { get; }
        private object _value;
        private bool _valueCreated;

        public Lazy(Type type)
        {
            Type = type;
        }

        public object GetValue()
        {
            LazyInitializer.EnsureInitialized(ref _value, () =>
            {
                var result = Activator.CreateInstance(Type); //Using TypeResolver cause loop.
                _valueCreated = true;
                return result;
            });
            return _value;
        }
        public bool IsValueCreated => _valueCreated;
    }

    public class TypeResolverLazy<T> : TypeResolverLazy, ILazyObjectProvider, ILazyValue<T>, ILazyValue
    {
        public TypeResolverLazy()
            : base(typeof(T))
        { }

        public new T GetValue() => (T)base.GetValue();
    }
    public class TypeResolverLazy : ILazyObjectProvider, ILazyValue
    {
        public Type Type { get; }
        private object _value;
        private bool _valueCreated;

        public TypeResolverLazy(Type type)
        {
            Type = type;
        }

        public object GetValue()
        {
            LazyInitializer.EnsureInitialized(ref _value, () =>
            {
                var result = TypeResolver.CreateInstance(Type);
                _valueCreated = true;
                return result;
            });
            return _value;
        }
        public bool IsValueCreated => _valueCreated;
    }
}