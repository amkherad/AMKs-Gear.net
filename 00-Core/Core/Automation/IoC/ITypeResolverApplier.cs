using System.Collections.Generic;
using AMKsGear.Architecture.Automation.IoC;
using AMKsGear.Core.Automation.IoC.Options;

namespace AMKsGear.Core.Automation.IoC
{
    public interface ITypeResolverApplier
    {
        void ApplyBindings(
            object instance,
            ITypeResolver resolver,
            TypeResolverTypeMapping mapping,
            TypeResolverTypeMappingContext context,
            List<TypeResolverOption> options);
    }
}