using AMKsGear.Architecture.Data.Serialization.Annotations;
using AMKsGear.Core.Automation.IoC;
using AMKsGear.Web.Core.Helpers;

namespace AMKsGear.Web.Core.ClientLibraryHelpers
{
    public class ClientLibraryOptions : JsOptions
    {
        public static T Default<T>() => TypeResolver.CreateInstance<T>();

        [SerializationExclude]
        public IHtmlTargetSelector TargetSelector { get; set; }
    }
}