using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.Automation.IoC
{
    public interface ITypeResolverLocalizationModel : ILocalizationModel
    {
        string TypeDescriptionNotFound { get; }
        string UnknownTypeForEngine { get; }
        string PropertyNotFound { get; }
    }
    
    public class DefaultTypeResolverLocalizationModel : DefaultEnglishLocalization, ITypeResolverLocalizationModel
    {
        public string TypeDescriptionNotFound => "No type description was found to resolve type '{0}'.";
        public string UnknownTypeForEngine => "Unknown type '{0}' provided for engine.";
        public string PropertyNotFound => "Property '{0}' not found.";
    }
}