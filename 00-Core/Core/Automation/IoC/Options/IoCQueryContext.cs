namespace AMKsGear.Core.Automation.IoC.Options
{
    public class IoCQueryContext : TypeResolverOption
    {
        public TypeResolverTypeMappingContextWrapper Context { get; private set; }

        public void SetContext(TypeResolverTypeMappingContext context)
            => Context = new TypeResolverTypeMappingContextWrapper(context);
        public void SetContext(TypeResolverTypeMappingContextWrapper context)
            => Context = context;
    }
    internal class _Internal_IoCQueryContext : TypeResolverOption
    {
        public TypeResolverTypeMappingContext Context { get; private set; }

        public void SetContext(TypeResolverTypeMappingContext context)
            => Context = context;
    }
}