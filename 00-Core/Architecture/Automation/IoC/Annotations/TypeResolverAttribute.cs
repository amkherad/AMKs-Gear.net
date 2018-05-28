using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Architecture.Automation.IoC.Annotations
{
    public class TypeResolverAttribute : OrderedAttribute
    {
        public TypeResolverAttribute() { }
        public TypeResolverAttribute(int order)
            : base(order)
        { }
    }
}