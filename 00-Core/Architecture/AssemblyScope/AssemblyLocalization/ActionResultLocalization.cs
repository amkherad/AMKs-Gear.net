using AMKsGear.Architecture.LocalizationFramework;

namespace AMKsGear.Architecture.AssemblyScope.AssemblyLocalization
{
    public interface IActionResultLocalization : ILocalization
    {
        string Success { get; }
        string Succeeded { get; }
        string Successful { get; }
        string Failure { get; }
        string Fail { get; }
        string Failed { get; }
    }
    public class DefaultActionResultLocalization : IActionResultLocalization
    {
        public string Success => "Success";
        public string Succeeded => "Succeeded";
        public string Successful => "Successful";
        public string Failure => "Failure";
        public string Fail => "Fail";
        public string Failed => "Failed";
    }
}