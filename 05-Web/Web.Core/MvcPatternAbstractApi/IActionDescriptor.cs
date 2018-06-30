using AMKsGear.Architecture.Modeling;
using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Web.Core.MvcPatternAbstractApi
{
    public interface IActionDescriptor : IMemberInfo, IWrapper
    {
        string ActionName { get; }

        IParameterDescriptor[] GetParameters();
    }
}