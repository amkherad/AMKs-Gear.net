using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AMKsGear.Core.Automation.IoC
{
    public class DefaultTypeResolver : TypeResolverContainer
    {
        //public object GetService(Type serviceType) => ;
        //public object Resolve(Type type, params object[] args) => Activator.CreateInstance(type, args);

        //public bool CanResolve(Type type, params object[] args)
        //    => type?.GetTypeInfo().DeclaredConstructors.Any(x => x.GetParameters().Length == 0) ?? false;

        public override object Resolve(Type type, object context, IEnumerable<object> args)
            => base.Resolve(type, context, args) ?? Activator.CreateInstance(type, args);

        public override bool CanResolve(Type type, object context, IEnumerable<object> args)
        {
            var result = base.CanResolve(type, context, args);
            if (!result && type != null)
            {
                result = type.GetTypeInfo().DeclaredConstructors.Any(x => x.GetParameters().Length == 0);
            }
            return result;
        }
    }
}