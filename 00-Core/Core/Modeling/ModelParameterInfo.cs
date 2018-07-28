using System.Reflection;

namespace AMKsGear.Core.Modeling
{
    /// <summary>
    /// Provides abstract access to property info.
    /// </summary>
    public class ModelParameterInfo
    {
        public ParameterInfo ParameterInfo { get; }

        public ModelParameterInfo( /*Type type, TypeInfo typeInfo,*/ ParameterInfo parameterInfo)
        {
            ParameterInfo = parameterInfo;
        }
    }
}