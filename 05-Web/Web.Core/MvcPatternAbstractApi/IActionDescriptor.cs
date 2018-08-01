using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IActionDescriptor : IAdapter
    {
        string Name { get; }
        
        string ActionName { get; }

        IParameterDescriptor[] GetParameters();
    }
}