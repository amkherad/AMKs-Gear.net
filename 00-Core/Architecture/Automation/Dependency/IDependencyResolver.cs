using System;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Automation.Dependency
{
    /// <summary>
    /// Provides a basic dependency resolver (IoC) functionality.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// Gets an object from the container.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Get([NotNull] Type type);
        
        /// <summary>
        /// Gets an object from the container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>();
        
        /// <summary>
        /// Gets an object from the container with respect to the context.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        object Get([NotNull] Type type, [NotNull] object context);
        
        /// <summary>
        /// Gets an object from the container with respect to the context.
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>([NotNull] object context);
        
        /// <summary>
        /// Gets an object from the container with respect to the context and additional options.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        object Get([NotNull] Type type, [NotNull] object context, params object[] options);
        
        /// <summary>
        /// Gets an object from the container with respect to the context and additional options.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>([NotNull] object context, params object[] options);


        /// <summary>
        /// Tries to get an object from the container.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryGet([NotNull] Type type, out object instance);

        /// <summary>
        /// Tries to get an object from the container.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool TryGet<T>(out T instance);
        
        /// <summary>
        /// Tries to get an object from the container with respect to the context.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryGet([NotNull] Type type, [NotNull] object context, out object instance);
        
        /// <summary>
        /// Tries to get an object from the container with respect to the context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool TryGet<T>([NotNull] object context, out T instance);
        
        /// <summary>
        /// Tries to get an object from the container with respect to the context and additional options.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        bool TryGet([NotNull] Type type, [NotNull] object context, out object instance, params object[] options);
        
        /// <summary>
        /// Tries to get an object from the container with respect to the context and additional options.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool TryGet<T>([NotNull] object context, out T instance, params object[] options);

        
        /// <summary>
        /// Tries to get an object from the container or default value if the object doesn't exist.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetOrDefault([NotNull] Type type);
        
        /// <summary>
        /// Tries to get an object from the container or default value if the object doesn't exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOrDefault<T>();
        
        /// <summary>
        /// Tries to get an object from the container or default value if the object doesn't exist with respect to the context.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        object GetOrDefault([NotNull] Type type, [NotNull] object context);
        
        /// <summary>
        /// Tries to get an object from the container or default value if the object doesn't exist with respect to the context.
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOrDefault<T>([NotNull] object context);
        
        /// <summary>
        /// Tries to get an object from the container or default value if the object doesn't exist with respect to the context and additional options.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        object GetOrDefault([NotNull] Type type, [NotNull] object context, params object[] options);
        
        /// <summary>
        /// Tries to get an object from the container or default value if the object doesn't exist with respect to the context and additional options.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetOrDefault<T>([NotNull] object context, params object[] options);

        
        /// <summary>
        /// Creates a sub-scope from the current context.
        /// </summary>
        /// <returns></returns>
        IDependencyScope CreateScope();
    }
}