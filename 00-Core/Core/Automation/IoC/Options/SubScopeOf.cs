using System;

namespace AMKsGear.Core.Automation.IoC.Options
{
    /// <summary>
    /// This makes object's scope to be sub-scope of another type.
    /// Used when you want to mark a ICrud to dispose before IRawWrapper.
    /// </summary>
    public class SubScopeOf : TypeResolverOption
    {
        public Type ParentScope { get; }
        
        public SubScopeOf(Type parentScope)
        {
            ParentScope = parentScope;
        }
    }
}