using AMKsGear.Architecture.Localization;

namespace AMKsGear.Architecture.AssemblyScope.AssemblyLocalization
{
    public interface IActionResultLocalizationModel : ILocalizationModel
    {
        string Success { get; }
        string Succeeded { get; }
        string Successful { get; }
        string Failure { get; }
        string Fail { get; }
        string Failed { get; }
    }
    public class DefaultActionResultLocalizationModel : DefaultEnglishLocalization, IActionResultLocalizationModel
    {
        public string Success => "Success";
        public string Succeeded => "Succeeded";
        public string Successful => "Successful";
        public string Failure => "Failure";
        public string Fail => "Fail";
        public string Failed => "Failed";
    }
}