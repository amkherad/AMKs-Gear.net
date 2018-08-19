using System;
using AMKsGear.Architecture.Automation.Dependency;
using AMKsGear.Architecture.Linq.Expressions;
using AMKsGear.Core.Linq.Expressions;

namespace AMKsGear.Core.Automation.Dependency
{
    public class DependencyContainer : IDependencyResolver
    {
        public DependencyContainerContext Context { get; }


        public DependencyContainer(IExpressionCompiler expressionCompiler, DependencyContainerContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public DependencyContainer()
            : this(
                InternalExpressionCompiler.Instance,
                new DependencyContainerContext()
            )
        {
        }


        /// <inheritdoc />
        public virtual bool TryGet(Type type, out object instance)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool TryGet<T>(out T instance)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool TryGet(Type type, object context, out object instance)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool TryGet<T>(object context, out T instance)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool TryGet(Type type, object context, out object instance, params object[] options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public virtual bool TryGet<T>(object context, out T instance, params object[] options)
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc />
        public virtual object Get(Type type)
        {
            return TryGet(type, out var instance)
                ? instance
                : throw _tryGetFailedException(type);
        }

        /// <inheritdoc />
        public virtual T Get<T>()
        {
            return TryGet<T>(out var instance)
                ? instance
                : throw _tryGetFailedException(typeof(T));
        }

        /// <inheritdoc />
        public virtual object Get(Type type, object context)
        {
            return TryGet(type, context, out var instance)
                ? instance
                : throw _tryGetFailedException(type);
        }

        /// <inheritdoc />
        public virtual T Get<T>(object context)
        {
            return TryGet<T>(context, out var instance)
                ? instance
                : throw _tryGetFailedException(typeof(T));
        }

        /// <inheritdoc />
        public virtual object Get(Type type, object context, params object[] options)
        {
            return TryGet(type, context, out var instance, options)
                ? instance
                : throw _tryGetFailedException(type);
        }

        /// <inheritdoc />
        public virtual T Get<T>(object context, params object[] options)
        {
            return TryGet<T>(context, out var instance, options)
                ? instance
                : throw _tryGetFailedException(typeof(T));
        }


        /// <inheritdoc />
        public virtual object GetOrDefault(Type type)
        {
            return TryGet(type, out var instance)
                ? instance
                : default;
        }

        /// <inheritdoc />
        public virtual T GetOrDefault<T>()
        {
            return TryGet<T>(out var instance)
                ? instance
                : default;
        }

        /// <inheritdoc />
        public virtual object GetOrDefault(Type type, object context)
        {
            return TryGet(type, context, out var instance)
                ? instance
                : default;
        }

        /// <inheritdoc />
        public virtual T GetOrDefault<T>(object context)
        {
            return TryGet<T>(context, out var instance)
                ? instance
                : default;
        }

        /// <inheritdoc />
        public virtual object GetOrDefault(Type type, object context, params object[] options)
        {
            return TryGet(type, context, out var instance, options)
                ? instance
                : default;
        }

        /// <inheritdoc />
        public virtual T GetOrDefault<T>(object context, params object[] options)
        {
            return TryGet<T>(context, out var instance, options)
                ? instance
                : default;
        }

        /// <inheritdoc />
        public virtual IDependencyScope CreateScope()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
        }
        

        private DependencyContainerException _tryGetFailedException(Type type)
        {
            return new DependencyContainerException();
        }
    }
}