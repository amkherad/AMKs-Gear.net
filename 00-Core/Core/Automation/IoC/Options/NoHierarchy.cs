namespace AMKsGear.Core.Automation.IoC.Options
{
    /// <summary>
    /// Disables hierarchy resolving in the current context.
    /// This means if two types require sme type, two separated instance will made.
    /// </summary>
    public class NoHierarchy : TypeResolverOption
    {

    }
}