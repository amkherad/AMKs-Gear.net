using AMKsGear.Architecture.AssemblyScope.AssemblyLocalization;
using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.TraceTools
{
    public interface ITestingLocalizationModel : ILocalizationModel, IActionResultLocalizationModel
    {
        string AssertionDoneWithResultOf { get; }
        string TestDoneWithResultOf { get; }
    }

    public class DefaultTestingLocalizationModel : DefaultActionResultLocalizationModel, ITestingLocalizationModel
    {
        public string AssertionDoneWithResultOf { get; } = "Assertion '{0}' done with the result of: {1}";
        public string TestDoneWithResultOf { get; } = "Test '{0}' has been {1}";
    }
}