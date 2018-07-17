using System;
using AMKsGear.Architecture.Automation.LifetimeManagers;

namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// Provides scoped context for mapper.
    /// </summary>
    public class MapperScope : IScopedBlock
    {
        /// <summary>
        /// Gives access to parent scope.
        /// </summary>
        public MapperScope ParentScope { get; protected set; }

        /// <summary>
        /// The <see cref="MapperContext"/>
        /// </summary>
        public MapperContext Context { get; protected set; }


        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="MapperScope"/> using an empty context.
        /// </summary>
        public MapperScope()
        {
            Context = new MapperContext();
        }

        /// <summary>
        /// Creates a new instance of <see cref="MapperScope"/> using give <see cref="MapperContext"/>
        /// </summary>
        /// <param name="context">The context to use in this scope.</param>
        public MapperScope(MapperContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates a new instance of <see cref="MapperScope"/> using given scope as parent.
        /// </summary>
        /// <param name="parentScope">The parent scope.</param>
        public MapperScope(MapperScope parentScope)
        {
            ParentScope = parentScope;
        }

        /// <summary>
        /// Creates a new instance of <see cref="MapperScope"/> using given parent and context.
        /// </summary>
        /// <param name="parentScope">The parent scope.</param>
        /// <param name="context">The context to use in this scope.</param>
        public MapperScope(MapperScope parentScope, MapperContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            ParentScope = parentScope;
        }

        #endregion
        
        
        public void Dispose()
        {
        }


        public MapperScope CreateSubScope()
        {
            return ParentScope;
        }
    }
}